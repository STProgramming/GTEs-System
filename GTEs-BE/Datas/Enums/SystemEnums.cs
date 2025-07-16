namespace GTEs_BE.Datas.Enums
{
    public enum HabitType
    {
        Commute,
        Leisure,
        Shopping,
        Gym,
        Custom
    }

    public enum TripMode
    {
        Fastest,
        Scenic,
        Economic
    }

    public enum RaceChipMode
    {
        Off,
        Eco,
        Normal,
        Sport
    }

    public enum WeatherCondition
    {
        Clear,
        Clouds,
        Rain,
        Snow,
        Thunderstorm,
        Drizzle,
        Mist,
        Fog
    }

    public enum StopType
    {
        Tourist,
        Fuel,
        Food,
        Rest,
        Custom
    }

    public enum NotificationTopic
    {
        InternalServerError,
        Sos,
        Update,
        DashCam,
        Disconnected,
        Habit
    }

    public enum NotificationGravity
    {
        None,
        Normal,
        Priority,
        Danger
    }
}
