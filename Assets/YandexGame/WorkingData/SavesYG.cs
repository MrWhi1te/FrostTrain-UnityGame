
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        //
        public int WagonCol; // Количество активных вагонов
        public int TextureTrain; // Выбранный вид поезда
        public bool[] Texture = new bool[4]; // 1-Цветной вид поезда / 2-Золотой вид поезда / 3-Фиолетовый вид поезда

        public int Money; // Количество денег
        public int Diamond; // Роскошь
        public int Score; // Очки
        public int Coal; // Количество угля
        public int CoalMax; // Максимум угля для хранения
        public int Worker; // Количество свободных рабочих
        public int AllWorker; // Количество всего рабочих в поезде
        public int Warm; // Количество тепла
        public int WarmMax; // Максимум тепла для хранения
        public int Food; // Количество еды
        public int FoodMax; // Максимум еды для хранения
        public int Water; // Количество воды
        public int WaterMax; // Максимум воды для хранения

        public int TemperatureOnStreet; // Температура за бортом
        public int NextStationTime; // Таймер до следующей станции
        public int NextStationTimeCount; //
        public int LevelLoco; // уровень локомотива. Уровень зависит от вида локомотива. ПОкупается на станции
        public int ActiveTimerLoco; // Активный таймер локомотива

        public int LevelEngine = 1; // Уровень двигателя локомотива
        public int LevelCoalStorage = 1; // Уровень тендера локомотива
        public int LevelChassis = 1; // Уровень шасси локомотива

        public int passWagoneCount;
        public int passCount;

        public int CargoTransportCount; // Количество вагонов
        public int RewardCargoTransport; // Награда за доставку

        public int CargoSpecTransportCount; // Количество вагонов

        public int CargoSpec1TransportCount; // Количество вагонов

        public int City; // Текущий город

        public bool[] Trainer = new bool[2]; //
        public bool CoalHelp; // Подсказка при первом появлении угля
        public bool helpPass; //
        public bool helpBarrier;
        public bool helpRepair;
        
        public int TaskCount; // Счетчик задания

        public bool[] WagoneActive = new bool[50]; // Активация скрипта вагона
        public string[] Name = new string[50]; // Название вагона (Назначение)
        public int[] LevelWagone = new int[50]; // Уровень вагона
        public int[] TimerActiveProduction = new int[50]; // Текующее время для таймера производства
        public int[] WorkerInWagone = new int[50]; // Количество активных работников и проживающих в пасс в вагоне (Кол. материалов в грузвом)
        public int[] TemperatureWagone = new int[50]; // Температура в вагоне

        public bool Saver;
        public bool Thanks;
        //
        public bool AirShipActive; //
        public int TimerAirShip = 900; //
        //
        public int TimeInGameStatistic; //
        public int CoalPlusStatistic; //
        public int FoodPlusStatistic; //
        public int WaterPlusStatistic; //
        public int WarmPlusStatistic; //
        public int MoneyPlusStatistic; //
        public int DistancePlusStatistic; //

        //
        public bool[] doneTask = new bool[14];
        public bool[] statusPass = new bool[42];
    }
}
