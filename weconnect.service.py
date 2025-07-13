#!/home/stcor/get-env/bin/python3
# -*- coding: utf-8 -*-

from flask import Flask, jsonify
from weconnect import weconnect
import warnings
import os
import json

warnings.filterwarnings("ignore")

def make_json_safe(obj):
    if isinstance(obj, dict):
        return {k: make_json_safe(v) for k, v in obj.items()}
    elif isinstance(obj, list):
        return [make_json_safe(v) for v in obj]
    elif hasattr(obj, 'value'):
        return make_json_safe(obj.value)
    elif hasattr(obj, 'asDict'):
        return make_json_safe(obj.asDict())
    elif isinstance(obj, (str, int, float, bool)) or obj is None:
        return obj
    else:
        return str(obj)  # fallback in stringa

email = os.getenv("VW_USERNAME")
password = os.getenv("VW_PASSWORD")

connection = weconnect.WeConnect(
        username=email,
        password=password,
        loginOnInit=True
)

app = Flask(__name__)

"""
@app.route('/api/status', methods=['GET'])
def status():
    try:
        connection.update()
        vehicle = next(iter(connection.vehicles.values()))
        safe_data = make_json_safe(vehicle)
        return jsonify(safe_data)
    except Exception as e:
        return jsonify({'error': str(e)}), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', debug=False)

"""
@app.route('/api/slim-status', methods=['GET'])
def slim_status():
    try:
        connection.update()
        vehicle = next(iter(connection.vehicles.values()))
        v = vehicle.domains

        result = {
            "batterySOC_pct": v['charging'].batteryStatus.currentSOC_pct.value,
            "chargingState": v['charging'].chargingStatus.chargingState.value,
            "plugConnected": v['charging'].plugStatus.plugConnectionState.value,
            "locked": v['access'].accessStatus.doorLockStatus.value,
            "doors": {k: v['access'].accessStatus.doors[k].openState.value for k in v['access'].accessStatus.doors},
            "windows": {k: v['access'].accessStatus.windows[k].openState.value for k in v['access'].accessStatus.windows},
            "climatisation": v['climatisation'].climatisationStatus.climatisationState.value,
            "targetTemperature_C": v['climatisation'].climatisationSettings.targetTemperature_C.value,
            "gps": {
                "latitude": v['parking'].parkingPosition.latitude.value,
                "longitude": v['parking'].parkingPosition.longitude.value
            },
            "range_km": v['fuelStatus'].rangeStatus.totalRange_km.value,
            "chargeMode": v['charging'].chargingStatus.chargeMode.value,
        }

        return jsonify(result)
    except Exception as e:
        return jsonify({"error": str(e)}), 500