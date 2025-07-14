from flask import Flask, request, jsonify
from weconnect import weconnect
import warnings
import datetime
import json
import os
import time
import socket

warnings.filterwarnings("ignore")

app = Flask(__name__)

# Configurazione base
SESSION_FILE = 'session.json'
SCHEDULE_FILE = 'climate_schedule.json'
PORT = 8080  # Porta fissa per uso continuo e affidabile
HOST = '0.0.0.0'  # Accessibile da qualsiasi IP della rete

authenticated = False
connection = None
user_vin = None
user_spinp = None
last_run_time = None

# === Funzioni di utilità ===
def save_session():
    if connection:
        connection.saveSession(SESSION_FILE)

def load_session():
    global connection
    connection = weconnect.WeConnect(loginOnInit=False, sessionFilePath=SESSION_FILE)
    connection.loadSession(SESSION_FILE)

def authenticate(email, password, spin):
    global connection, authenticated, user_vin, user_spinp
    try:
        connection = weconnect.WeConnect(user=email, password=password, loginOnInit=True)
        user_vin = next(iter(connection.vehicles)).vin
        user_spinp = spin
        save_session()
        authenticated = True
        return True, "Autenticazione riuscita"
    except Exception as e:
        return False, str(e)

# === API Flask ===
@app.route('/login', methods=['POST'])
def login():
    data = request.json
    email = data.get('email')
    password = data.get('password')
    spin = data.get('spin')
    success, msg = authenticate(email, password, spin)
    return jsonify({'success': success, 'message': msg})

@app.route('/status', methods=['GET'])
def status():
    if not authenticated:
        return jsonify({'error': 'Non autenticato'}), 401
    vehicle = connection.vehicles[user_vin]
    status_data = {
        'battery': vehicle.domains['charging'].batteryStatus.value if 'charging' in vehicle.domains else 'N/A',
        'position': str(vehicle.domains['parking'].parkingPosition.value) if 'parking' in vehicle.domains else 'N/A',
        'climatisation': str(vehicle.domains['climatisation'].climatisationStatus.value) if 'climatisation' in vehicle.domains else 'N/A'
    }
    return jsonify(status_data)

@app.route('/climate_on', methods=['POST'])
def climate_on():
    if not authenticated:
        return jsonify({'error': 'Non autenticato'}), 401
    data = request.json
    temp = data.get('temperature', 20.0)
    vehicle = connection.vehicles[user_vin]
    vehicle.domains['climatisation'].setTemperature(temp)
    vehicle.domains['climatisation'].startClimatisation(spin=user_spinp)
    return jsonify({'success': True, 'message': f'Climatizzatore attivato a {temp}°C'})

@app.route('/climate_off', methods=['POST'])
def climate_off():
    if not authenticated:
        return jsonify({'error': 'Non autenticato'}), 401
    vehicle = connection.vehicles[user_vin]
    vehicle.domains['climatisation'].stopClimatisation(spin=user_spinp)
    return jsonify({'success': True, 'message': 'Climatizzatore spento'})

@app.route('/lock', methods=['POST'])
def lock():
    if not authenticated:
        return jsonify({'error': 'Non autenticato'}), 401
    vehicle = connection.vehicles[user_vin]
    vehicle.domains['access'].lock(spin=user_spinp)
    return jsonify({'success': True, 'message': 'Veicolo bloccato'})

@app.route('/unlock', methods=['POST'])
def unlock():
    if not authenticated:
        return jsonify({'error': 'Non autenticato'}), 401
    vehicle = connection.vehicles[user_vin]
    vehicle.domains['access'].unlock(spin=user_spinp)
    return jsonify({'success': True, 'message': 'Veicolo sbloccato'})

@app.route('/start_charge', methods=['POST'])
def start_charge():
    if not authenticated:
        return jsonify({'error': 'Non autenticato'}), 401
    vehicle = connection.vehicles[user_vin]
    vehicle.domains['charging'].start(spin=user_spinp)
    return jsonify({'success': True, 'message': 'Ricarica avviata'})

@app.route('/stop_charge', methods=['POST'])
def stop_charge():
    if not authenticated:
        return jsonify({'error': 'Non autenticato'}), 401
    vehicle = connection.vehicles[user_vin]
    vehicle.domains['charging'].stop(spin=user_spinp)
    return jsonify({'success': True, 'message': 'Ricarica fermata'})

@app.route('/flash', methods=['POST'])
def flash():
    if not authenticated:
        return jsonify({'error': 'Non autenticato'}), 401
    vehicle = connection.vehicles[user_vin]
    vehicle.domains['vehicleAccess'].flash()
    return jsonify({'success': True, 'message': 'Fari lampeggianti attivati'})

@app.route('/honk', methods=['POST'])
def honk():
    if not authenticated:
        return jsonify({'error': 'Non autenticato'}), 401
    vehicle = connection.vehicles[user_vin]
    vehicle.domains['vehicleAccess'].honk()
    return jsonify({'success': True, 'message': 'Clacson attivato'})

@app.route('/schedule_climate', methods=['POST'])
def schedule_climate():
    data = request.json
    orario = data.get('orario')  # formato HH:MM
    temperatura = data.get('temperatura', 20.0)
    with open(SCHEDULE_FILE, 'w') as f:
        json.dump({'orario': orario, 'temperatura': temperatura}, f)
    return jsonify({'success': True, 'message': f'Clima schedulato per {orario} a {temperatura}°C'})

# === Scheduler semplice, richiamabile manualmente ===
@app.route('/run_scheduler_once', methods=['POST'])
def run_scheduler_once():
    global last_run_time
    try:
        if os.path.exists(SCHEDULE_FILE) and authenticated:
            with open(SCHEDULE_FILE) as f:
                sched = json.load(f)
            now_str = datetime.datetime.now().strftime('%H:%M')
            if now_str == sched['orario'] and last_run_time != now_str:
                vehicle = connection.vehicles[user_vin]
                vehicle.domains['climatisation'].setTemperature(sched['temperatura'])
                vehicle.domains['climatisation'].startClimatisation(spin=user_spinp)
                last_run_time = now_str
                return jsonify({'success': True, 'message': 'Clima avviato come da schedulazione'})
        return jsonify({'success': True, 'message': 'Nessuna azione eseguita'})
    except Exception as e:
        return jsonify({'success': False, 'error': str(e)})

if __name__ == '__main__':
    try:
        load_session()
        authenticated = True
        user_vin = next(iter(connection.vehicles)).vin
    except:
        authenticated = False

    try:
        print(f"Avvio server su {HOST}:{PORT}...")
        app.run(host=HOST, port=PORT)
    except OSError as e:
        print(f"Errore durante l'avvio del server: {e}")