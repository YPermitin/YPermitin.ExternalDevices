using System.Runtime.CompilerServices;
using Tmds.DBus;

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace YPermitin.ExternalDevices.ManagementService.NetworkManager
{
    [DBusInterface("org.freedesktop.DBus.ObjectManager")]
    interface IObjectManager : IDBusObject
    {
        Task<IDictionary<ObjectPath, IDictionary<string, IDictionary<string, object>>>> GetManagedObjectsAsync();
        Task<IDisposable> WatchInterfacesAddedAsync(Action<(ObjectPath objectPath, IDictionary<string, IDictionary<string, object>> interfacesAndProperties)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchInterfacesRemovedAsync(Action<(ObjectPath objectPath, string[] interfaces)> handler, Action<Exception> onError = null);
    }

    #region ModemManager

    [DBusInterface("org.freedesktop.ModemManager1")]
    interface IModemManager1 : IDBusObject
    {
        Task ScanDevicesAsync();
        Task SetLoggingAsync(string Level);
        Task ReportKernelEventAsync(IDictionary<string, object> Properties);
        Task InhibitDeviceAsync(string Uid, bool Inhibit);
        Task<T> GetAsync<T>(string prop);
        Task<ModemManager1Properties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class ModemManager1Properties
    {
        private string _version = default(string);
        public string Version
        {
            get
            {
                return _version;
            }

            set
            {
                _version = (value);
            }
        }
    }

    static class ModemManager1Extensions
    {
        public static Task<string> GetVersionAsync(this IModemManager1 o) => o.GetAsync<string>("Version");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Modem.Location")]
    interface ILocation : IDBusObject
    {
        Task SetupAsync(uint Sources, bool SignalLocation);
        Task<IDictionary<uint, object>> GetLocationAsync();
        Task SetSuplServerAsync(string Supl);
        Task InjectAssistanceDataAsync(byte[] Data);
        Task SetGpsRefreshRateAsync(uint Rate);
        Task<T> GetAsync<T>(string prop);
        Task<LocationProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class LocationProperties
    {
        private uint _capabilities = default(uint);
        public uint Capabilities
        {
            get
            {
                return _capabilities;
            }

            set
            {
                _capabilities = (value);
            }
        }

        private uint _supportedAssistanceData = default(uint);
        public uint SupportedAssistanceData
        {
            get
            {
                return _supportedAssistanceData;
            }

            set
            {
                _supportedAssistanceData = (value);
            }
        }

        private uint _enabled = default(uint);
        public uint Enabled
        {
            get
            {
                return _enabled;
            }

            set
            {
                _enabled = (value);
            }
        }

        private bool _signalsLocation = default(bool);
        public bool SignalsLocation
        {
            get
            {
                return _signalsLocation;
            }

            set
            {
                _signalsLocation = (value);
            }
        }

        private IDictionary<uint, object> _location = default(IDictionary<uint, object>);
        public IDictionary<uint, object> Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = (value);
            }
        }

        private string _suplServer = default(string);
        public string SuplServer
        {
            get
            {
                return _suplServer;
            }

            set
            {
                _suplServer = (value);
            }
        }

        private string[] _assistanceDataServers = default(string[]);
        public string[] AssistanceDataServers
        {
            get
            {
                return _assistanceDataServers;
            }

            set
            {
                _assistanceDataServers = (value);
            }
        }

        private uint _gpsRefreshRate = default(uint);
        public uint GpsRefreshRate
        {
            get
            {
                return _gpsRefreshRate;
            }

            set
            {
                _gpsRefreshRate = (value);
            }
        }
    }

    static class LocationExtensions
    {
        public static Task<uint> GetCapabilitiesAsync(this ILocation o) => o.GetAsync<uint>("Capabilities");
        public static Task<uint> GetSupportedAssistanceDataAsync(this ILocation o) => o.GetAsync<uint>("SupportedAssistanceData");
        public static Task<uint> GetEnabledAsync(this ILocation o) => o.GetAsync<uint>("Enabled");
        public static Task<bool> GetSignalsLocationAsync(this ILocation o) => o.GetAsync<bool>("SignalsLocation");
        public static Task<IDictionary<uint, object>> GetLocationAsync(this ILocation o) => o.GetAsync<IDictionary<uint, object>>("Location");
        public static Task<string> GetSuplServerAsync(this ILocation o) => o.GetAsync<string>("SuplServer");
        public static Task<string[]> GetAssistanceDataServersAsync(this ILocation o) => o.GetAsync<string[]>("AssistanceDataServers");
        public static Task<uint> GetGpsRefreshRateAsync(this ILocation o) => o.GetAsync<uint>("GpsRefreshRate");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Modem.Signal")]
    interface ISignal : IDBusObject
    {
        Task SetupAsync(uint Rate);
        Task SetupThresholdsAsync(IDictionary<string, object> Settings);
        Task<T> GetAsync<T>(string prop);
        Task<SignalProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class SignalProperties
    {
        private uint _rate = default(uint);
        public uint Rate
        {
            get
            {
                return _rate;
            }

            set
            {
                _rate = (value);
            }
        }

        private uint _rssiThreshold = default(uint);
        public uint RssiThreshold
        {
            get
            {
                return _rssiThreshold;
            }

            set
            {
                _rssiThreshold = (value);
            }
        }

        private bool _errorRateThreshold = default(bool);
        public bool ErrorRateThreshold
        {
            get
            {
                return _errorRateThreshold;
            }

            set
            {
                _errorRateThreshold = (value);
            }
        }

        private IDictionary<string, object> _cdma = default(IDictionary<string, object>);
        public IDictionary<string, object> Cdma
        {
            get
            {
                return _cdma;
            }

            set
            {
                _cdma = (value);
            }
        }

        private IDictionary<string, object> _evdo = default(IDictionary<string, object>);
        public IDictionary<string, object> Evdo
        {
            get
            {
                return _evdo;
            }

            set
            {
                _evdo = (value);
            }
        }

        private IDictionary<string, object> _gsm = default(IDictionary<string, object>);
        public IDictionary<string, object> Gsm
        {
            get
            {
                return _gsm;
            }

            set
            {
                _gsm = (value);
            }
        }

        private IDictionary<string, object> _umts = default(IDictionary<string, object>);
        public IDictionary<string, object> Umts
        {
            get
            {
                return _umts;
            }

            set
            {
                _umts = (value);
            }
        }

        private IDictionary<string, object> _lte = default(IDictionary<string, object>);
        public IDictionary<string, object> Lte
        {
            get
            {
                return _lte;
            }

            set
            {
                _lte = (value);
            }
        }

        private IDictionary<string, object> _nr5g = default(IDictionary<string, object>);
        public IDictionary<string, object> Nr5g
        {
            get
            {
                return _nr5g;
            }

            set
            {
                _nr5g = (value);
            }
        }
    }

    static class SignalExtensions
    {
        public static Task<uint> GetRateAsync(this ISignal o) => o.GetAsync<uint>("Rate");
        public static Task<uint> GetRssiThresholdAsync(this ISignal o) => o.GetAsync<uint>("RssiThreshold");
        public static Task<bool> GetErrorRateThresholdAsync(this ISignal o) => o.GetAsync<bool>("ErrorRateThreshold");
        public static Task<IDictionary<string, object>> GetCdmaAsync(this ISignal o) => o.GetAsync<IDictionary<string, object>>("Cdma");
        public static Task<IDictionary<string, object>> GetEvdoAsync(this ISignal o) => o.GetAsync<IDictionary<string, object>>("Evdo");
        public static Task<IDictionary<string, object>> GetGsmAsync(this ISignal o) => o.GetAsync<IDictionary<string, object>>("Gsm");
        public static Task<IDictionary<string, object>> GetUmtsAsync(this ISignal o) => o.GetAsync<IDictionary<string, object>>("Umts");
        public static Task<IDictionary<string, object>> GetLteAsync(this ISignal o) => o.GetAsync<IDictionary<string, object>>("Lte");
        public static Task<IDictionary<string, object>> GetNr5gAsync(this ISignal o) => o.GetAsync<IDictionary<string, object>>("Nr5g");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Modem.Modem3gpp.Ussd")]
    interface IUssd : IDBusObject
    {
        Task<string> InitiateAsync(string Command);
        Task<string> RespondAsync(string Response);
        Task CancelAsync();
        Task<T> GetAsync<T>(string prop);
        Task<UssdProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class UssdProperties
    {
        private uint _state = default(uint);
        public uint State
        {
            get
            {
                return _state;
            }

            set
            {
                _state = (value);
            }
        }

        private string _networkNotification = default(string);
        public string NetworkNotification
        {
            get
            {
                return _networkNotification;
            }

            set
            {
                _networkNotification = (value);
            }
        }

        private string _networkRequest = default(string);
        public string NetworkRequest
        {
            get
            {
                return _networkRequest;
            }

            set
            {
                _networkRequest = (value);
            }
        }
    }

    static class UssdExtensions
    {
        public static Task<uint> GetStateAsync(this IUssd o) => o.GetAsync<uint>("State");
        public static Task<string> GetNetworkNotificationAsync(this IUssd o) => o.GetAsync<string>("NetworkNotification");
        public static Task<string> GetNetworkRequestAsync(this IUssd o) => o.GetAsync<string>("NetworkRequest");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Modem.Messaging")]
    interface IMessaging : IDBusObject
    {
        Task<ObjectPath[]> ListAsync();
        Task DeleteAsync(ObjectPath Path);
        Task<ObjectPath> CreateAsync(IDictionary<string, object> Properties);
        Task<IDisposable> WatchAddedAsync(Action<(ObjectPath path, bool received)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchDeletedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<MessagingProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class MessagingProperties
    {
        private ObjectPath[] _messages = default(ObjectPath[]);
        public ObjectPath[] Messages
        {
            get
            {
                return _messages;
            }

            set
            {
                _messages = (value);
            }
        }

        private uint[] _supportedStorages = default(uint[]);
        public uint[] SupportedStorages
        {
            get
            {
                return _supportedStorages;
            }

            set
            {
                _supportedStorages = (value);
            }
        }

        private uint _defaultStorage = default(uint);
        public uint DefaultStorage
        {
            get
            {
                return _defaultStorage;
            }

            set
            {
                _defaultStorage = (value);
            }
        }
    }

    static class MessagingExtensions
    {
        public static Task<ObjectPath[]> GetMessagesAsync(this IMessaging o) => o.GetAsync<ObjectPath[]>("Messages");
        public static Task<uint[]> GetSupportedStoragesAsync(this IMessaging o) => o.GetAsync<uint[]>("SupportedStorages");
        public static Task<uint> GetDefaultStorageAsync(this IMessaging o) => o.GetAsync<uint>("DefaultStorage");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Modem")]
    interface IModem : IDBusObject
    {
        Task EnableAsync(bool Enable);
        Task<ObjectPath[]> ListBearersAsync();
        Task<ObjectPath> CreateBearerAsync(IDictionary<string, object> Properties);
        Task DeleteBearerAsync(ObjectPath Bearer);
        Task ResetAsync();
        Task FactoryResetAsync(string Code);
        Task SetPowerStateAsync(uint State);
        Task SetCurrentCapabilitiesAsync(uint Capabilities);
        Task SetCurrentModesAsync((uint, uint) Modes);
        Task SetCurrentBandsAsync(uint[] Bands);
        Task SetPrimarySimSlotAsync(uint SimSlot);
        Task<IDictionary<string, object>[]> GetCellInfoAsync();
        Task<string> CommandAsync(string Cmd, uint Timeout);
        Task<IDisposable> WatchStateChangedAsync(Action<(int old, int @new, uint reason)> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<ModemProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class ModemProperties
    {
        private ObjectPath _sim = default(ObjectPath);
        public ObjectPath Sim
        {
            get
            {
                return _sim;
            }

            set
            {
                _sim = (value);
            }
        }

        private ObjectPath[] _simSlots = default(ObjectPath[]);
        public ObjectPath[] SimSlots
        {
            get
            {
                return _simSlots;
            }

            set
            {
                _simSlots = (value);
            }
        }

        private uint _primarySimSlot = default(uint);
        public uint PrimarySimSlot
        {
            get
            {
                return _primarySimSlot;
            }

            set
            {
                _primarySimSlot = (value);
            }
        }

        private ObjectPath[] _bearers = default(ObjectPath[]);
        public ObjectPath[] Bearers
        {
            get
            {
                return _bearers;
            }

            set
            {
                _bearers = (value);
            }
        }

        private uint[] _supportedCapabilities = default(uint[]);
        public uint[] SupportedCapabilities
        {
            get
            {
                return _supportedCapabilities;
            }

            set
            {
                _supportedCapabilities = (value);
            }
        }

        private uint _currentCapabilities = default(uint);
        public uint CurrentCapabilities
        {
            get
            {
                return _currentCapabilities;
            }

            set
            {
                _currentCapabilities = (value);
            }
        }

        private uint _maxBearers = default(uint);
        public uint MaxBearers
        {
            get
            {
                return _maxBearers;
            }

            set
            {
                _maxBearers = (value);
            }
        }

        private uint _maxActiveBearers = default(uint);
        public uint MaxActiveBearers
        {
            get
            {
                return _maxActiveBearers;
            }

            set
            {
                _maxActiveBearers = (value);
            }
        }

        private uint _maxActiveMultiplexedBearers = default(uint);
        public uint MaxActiveMultiplexedBearers
        {
            get
            {
                return _maxActiveMultiplexedBearers;
            }

            set
            {
                _maxActiveMultiplexedBearers = (value);
            }
        }

        private string _manufacturer = default(string);
        public string Manufacturer
        {
            get
            {
                return _manufacturer;
            }

            set
            {
                _manufacturer = (value);
            }
        }

        private string _model = default(string);
        public string Model
        {
            get
            {
                return _model;
            }

            set
            {
                _model = (value);
            }
        }

        private string _revision = default(string);
        public string Revision
        {
            get
            {
                return _revision;
            }

            set
            {
                _revision = (value);
            }
        }

        private string _carrierConfiguration = default(string);
        public string CarrierConfiguration
        {
            get
            {
                return _carrierConfiguration;
            }

            set
            {
                _carrierConfiguration = (value);
            }
        }

        private string _carrierConfigurationRevision = default(string);
        public string CarrierConfigurationRevision
        {
            get
            {
                return _carrierConfigurationRevision;
            }

            set
            {
                _carrierConfigurationRevision = (value);
            }
        }

        private string _hardwareRevision = default(string);
        public string HardwareRevision
        {
            get
            {
                return _hardwareRevision;
            }

            set
            {
                _hardwareRevision = (value);
            }
        }

        private string _deviceIdentifier = default(string);
        public string DeviceIdentifier
        {
            get
            {
                return _deviceIdentifier;
            }

            set
            {
                _deviceIdentifier = (value);
            }
        }

        private string _device = default(string);
        public string Device
        {
            get
            {
                return _device;
            }

            set
            {
                _device = (value);
            }
        }

        private string[] _drivers = default(string[]);
        public string[] Drivers
        {
            get
            {
                return _drivers;
            }

            set
            {
                _drivers = (value);
            }
        }

        private string _plugin = default(string);
        public string Plugin
        {
            get
            {
                return _plugin;
            }

            set
            {
                _plugin = (value);
            }
        }

        private string _primaryPort = default(string);
        public string PrimaryPort
        {
            get
            {
                return _primaryPort;
            }

            set
            {
                _primaryPort = (value);
            }
        }

        private (string, uint)[] _ports = default((string, uint)[]);
        public (string, uint)[] Ports
        {
            get
            {
                return _ports;
            }

            set
            {
                _ports = (value);
            }
        }

        private string _equipmentIdentifier = default(string);
        public string EquipmentIdentifier
        {
            get
            {
                return _equipmentIdentifier;
            }

            set
            {
                _equipmentIdentifier = (value);
            }
        }

        private uint _unlockRequired = default(uint);
        public uint UnlockRequired
        {
            get
            {
                return _unlockRequired;
            }

            set
            {
                _unlockRequired = (value);
            }
        }

        private IDictionary<uint, uint> _unlockRetries = default(IDictionary<uint, uint>);
        public IDictionary<uint, uint> UnlockRetries
        {
            get
            {
                return _unlockRetries;
            }

            set
            {
                _unlockRetries = (value);
            }
        }

        private int _state = default(int);
        public int State
        {
            get
            {
                return _state;
            }

            set
            {
                _state = (value);
            }
        }

        private uint _stateFailedReason = default(uint);
        public uint StateFailedReason
        {
            get
            {
                return _stateFailedReason;
            }

            set
            {
                _stateFailedReason = (value);
            }
        }

        private uint _accessTechnologies = default(uint);
        public uint AccessTechnologies
        {
            get
            {
                return _accessTechnologies;
            }

            set
            {
                _accessTechnologies = (value);
            }
        }

        private (uint, bool) _signalQuality = default((uint, bool));
        public (uint, bool) SignalQuality
        {
            get
            {
                return _signalQuality;
            }

            set
            {
                _signalQuality = (value);
            }
        }

        private string[] _ownNumbers = default(string[]);
        public string[] OwnNumbers
        {
            get
            {
                return _ownNumbers;
            }

            set
            {
                _ownNumbers = (value);
            }
        }

        private uint _powerState = default(uint);
        public uint PowerState
        {
            get
            {
                return _powerState;
            }

            set
            {
                _powerState = (value);
            }
        }

        private (uint, uint)[] _supportedModes = default((uint, uint)[]);
        public (uint, uint)[] SupportedModes
        {
            get
            {
                return _supportedModes;
            }

            set
            {
                _supportedModes = (value);
            }
        }

        private (uint, uint) _currentModes = default((uint, uint));
        public (uint, uint) CurrentModes
        {
            get
            {
                return _currentModes;
            }

            set
            {
                _currentModes = (value);
            }
        }

        private uint[] _supportedBands = default(uint[]);
        public uint[] SupportedBands
        {
            get
            {
                return _supportedBands;
            }

            set
            {
                _supportedBands = (value);
            }
        }

        private uint[] _currentBands = default(uint[]);
        public uint[] CurrentBands
        {
            get
            {
                return _currentBands;
            }

            set
            {
                _currentBands = (value);
            }
        }

        private uint _supportedIpFamilies = default(uint);
        public uint SupportedIpFamilies
        {
            get
            {
                return _supportedIpFamilies;
            }

            set
            {
                _supportedIpFamilies = (value);
            }
        }
    }

    static class ModemExtensions
    {
        public static Task<ObjectPath> GetSimAsync(this IModem o) => o.GetAsync<ObjectPath>("Sim");
        public static Task<ObjectPath[]> GetSimSlotsAsync(this IModem o) => o.GetAsync<ObjectPath[]>("SimSlots");
        public static Task<uint> GetPrimarySimSlotAsync(this IModem o) => o.GetAsync<uint>("PrimarySimSlot");
        public static Task<ObjectPath[]> GetBearersAsync(this IModem o) => o.GetAsync<ObjectPath[]>("Bearers");
        public static Task<uint[]> GetSupportedCapabilitiesAsync(this IModem o) => o.GetAsync<uint[]>("SupportedCapabilities");
        public static Task<uint> GetCurrentCapabilitiesAsync(this IModem o) => o.GetAsync<uint>("CurrentCapabilities");
        public static Task<uint> GetMaxBearersAsync(this IModem o) => o.GetAsync<uint>("MaxBearers");
        public static Task<uint> GetMaxActiveBearersAsync(this IModem o) => o.GetAsync<uint>("MaxActiveBearers");
        public static Task<uint> GetMaxActiveMultiplexedBearersAsync(this IModem o) => o.GetAsync<uint>("MaxActiveMultiplexedBearers");
        public static Task<string> GetManufacturerAsync(this IModem o) => o.GetAsync<string>("Manufacturer");
        public static Task<string> GetModelAsync(this IModem o) => o.GetAsync<string>("Model");
        public static Task<string> GetRevisionAsync(this IModem o) => o.GetAsync<string>("Revision");
        public static Task<string> GetCarrierConfigurationAsync(this IModem o) => o.GetAsync<string>("CarrierConfiguration");
        public static Task<string> GetCarrierConfigurationRevisionAsync(this IModem o) => o.GetAsync<string>("CarrierConfigurationRevision");
        public static Task<string> GetHardwareRevisionAsync(this IModem o) => o.GetAsync<string>("HardwareRevision");
        public static Task<string> GetDeviceIdentifierAsync(this IModem o) => o.GetAsync<string>("DeviceIdentifier");
        public static Task<string> GetDeviceAsync(this IModem o) => o.GetAsync<string>("Device");
        public static Task<string[]> GetDriversAsync(this IModem o) => o.GetAsync<string[]>("Drivers");
        public static Task<string> GetPluginAsync(this IModem o) => o.GetAsync<string>("Plugin");
        public static Task<string> GetPrimaryPortAsync(this IModem o) => o.GetAsync<string>("PrimaryPort");
        public static Task<(string, uint)[]> GetPortsAsync(this IModem o) => o.GetAsync<(string, uint)[]>("Ports");
        public static Task<string> GetEquipmentIdentifierAsync(this IModem o) => o.GetAsync<string>("EquipmentIdentifier");
        public static Task<uint> GetUnlockRequiredAsync(this IModem o) => o.GetAsync<uint>("UnlockRequired");
        public static Task<IDictionary<uint, uint>> GetUnlockRetriesAsync(this IModem o) => o.GetAsync<IDictionary<uint, uint>>("UnlockRetries");
        public static Task<int> GetStateAsync(this IModem o) => o.GetAsync<int>("State");
        public static Task<uint> GetStateFailedReasonAsync(this IModem o) => o.GetAsync<uint>("StateFailedReason");
        public static Task<uint> GetAccessTechnologiesAsync(this IModem o) => o.GetAsync<uint>("AccessTechnologies");
        public static Task<(uint, bool)> GetSignalQualityAsync(this IModem o) => o.GetAsync<(uint, bool)>("SignalQuality");
        public static Task<string[]> GetOwnNumbersAsync(this IModem o) => o.GetAsync<string[]>("OwnNumbers");
        public static Task<uint> GetPowerStateAsync(this IModem o) => o.GetAsync<uint>("PowerState");
        public static Task<(uint, uint)[]> GetSupportedModesAsync(this IModem o) => o.GetAsync<(uint, uint)[]>("SupportedModes");
        public static Task<(uint, uint)> GetCurrentModesAsync(this IModem o) => o.GetAsync<(uint, uint)>("CurrentModes");
        public static Task<uint[]> GetSupportedBandsAsync(this IModem o) => o.GetAsync<uint[]>("SupportedBands");
        public static Task<uint[]> GetCurrentBandsAsync(this IModem o) => o.GetAsync<uint[]>("CurrentBands");
        public static Task<uint> GetSupportedIpFamiliesAsync(this IModem o) => o.GetAsync<uint>("SupportedIpFamilies");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Modem.Time")]
    interface ITime : IDBusObject
    {
        Task<string> GetNetworkTimeAsync();
        Task<IDisposable> WatchNetworkTimeChangedAsync(Action<string> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<TimeProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class TimeProperties
    {
        private IDictionary<string, object> _networkTimezone = default(IDictionary<string, object>);
        public IDictionary<string, object> NetworkTimezone
        {
            get
            {
                return _networkTimezone;
            }

            set
            {
                _networkTimezone = (value);
            }
        }
    }

    static class TimeExtensions
    {
        public static Task<IDictionary<string, object>> GetNetworkTimezoneAsync(this ITime o) => o.GetAsync<IDictionary<string, object>>("NetworkTimezone");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Modem.Firmware")]
    interface IFirmware : IDBusObject
    {
        Task<(string selected, IDictionary<string, object>[] installed)> ListAsync();
        Task SelectAsync(string Uniqueid);
        Task<T> GetAsync<T>(string prop);
        Task<FirmwareProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class FirmwareProperties
    {
        private (uint, IDictionary<string, object>) _updateSettings = default((uint, IDictionary<string, object>));
        public (uint, IDictionary<string, object>) UpdateSettings
        {
            get
            {
                return _updateSettings;
            }

            set
            {
                _updateSettings = (value);
            }
        }
    }

    static class FirmwareExtensions
    {
        public static Task<(uint, IDictionary<string, object>)> GetUpdateSettingsAsync(this IFirmware o) => o.GetAsync<(uint, IDictionary<string, object>)>("UpdateSettings");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Modem.Modem3gpp.ProfileManager")]
    interface IProfileManager : IDBusObject
    {
        Task<IDictionary<string, object>[]> ListAsync();
        Task<IDictionary<string, object>> SetAsync(IDictionary<string, object> RequestedProperties);
        Task DeleteAsync(IDictionary<string, object> Properties);
        Task<IDisposable> WatchUpdatedAsync(Action handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<ProfileManagerProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class ProfileManagerProperties
    {
        private string _indexField = default(string);
        public string IndexField
        {
            get
            {
                return _indexField;
            }

            set
            {
                _indexField = (value);
            }
        }
    }

    static class ProfileManagerExtensions
    {
        public static Task<string> GetIndexFieldAsync(this IProfileManager o) => o.GetAsync<string>("IndexField");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Modem.Sar")]
    interface ISar : IDBusObject
    {
        Task EnableAsync(bool Enable);
        Task SetPowerLevelAsync(uint Level);
        Task<T> GetAsync<T>(string prop);
        Task<SarProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class SarProperties
    {
        private bool _state = default(bool);
        public bool State
        {
            get
            {
                return _state;
            }

            set
            {
                _state = (value);
            }
        }

        private uint _powerLevel = default(uint);
        public uint PowerLevel
        {
            get
            {
                return _powerLevel;
            }

            set
            {
                _powerLevel = (value);
            }
        }
    }

    static class SarExtensions
    {
        public static Task<bool> GetStateAsync(this ISar o) => o.GetAsync<bool>("State");
        public static Task<uint> GetPowerLevelAsync(this ISar o) => o.GetAsync<uint>("PowerLevel");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Modem.Simple")]
    interface ISimple : IDBusObject
    {
        Task<ObjectPath> ConnectAsync(IDictionary<string, object> Properties);
        Task DisconnectAsync(ObjectPath Bearer);
        Task<IDictionary<string, object>> GetStatusAsync();
    }

    [DBusInterface("org.freedesktop.ModemManager1.Modem.Modem3gpp")]
    interface IModem3gpp : IDBusObject
    {
        Task RegisterAsync(string OperatorId);
        Task<IDictionary<string, object>[]> ScanAsync();
        Task SetEpsUeModeOperationAsync(uint Mode);
        Task SetInitialEpsBearerSettingsAsync(IDictionary<string, object> Settings);
        Task SetNr5gRegistrationSettingsAsync(IDictionary<string, object> Properties);
        Task DisableFacilityLockAsync((uint, string) Properties);
        Task SetPacketServiceStateAsync(uint State);
        Task<T> GetAsync<T>(string prop);
        Task<Modem3gppProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class Modem3gppProperties
    {
        private string _imei = default(string);
        public string Imei
        {
            get
            {
                return _imei;
            }

            set
            {
                _imei = (value);
            }
        }

        private uint _registrationState = default(uint);
        public uint RegistrationState
        {
            get
            {
                return _registrationState;
            }

            set
            {
                _registrationState = (value);
            }
        }

        private string _operatorCode = default(string);
        public string OperatorCode
        {
            get
            {
                return _operatorCode;
            }

            set
            {
                _operatorCode = (value);
            }
        }

        private string _operatorName = default(string);
        public string OperatorName
        {
            get
            {
                return _operatorName;
            }

            set
            {
                _operatorName = (value);
            }
        }

        private uint _enabledFacilityLocks = default(uint);
        public uint EnabledFacilityLocks
        {
            get
            {
                return _enabledFacilityLocks;
            }

            set
            {
                _enabledFacilityLocks = (value);
            }
        }

        private uint _subscriptionState = default(uint);
        public uint SubscriptionState
        {
            get
            {
                return _subscriptionState;
            }

            set
            {
                _subscriptionState = (value);
            }
        }

        private uint _epsUeModeOperation = default(uint);
        public uint EpsUeModeOperation
        {
            get
            {
                return _epsUeModeOperation;
            }

            set
            {
                _epsUeModeOperation = (value);
            }
        }

        private (uint, bool, byte[])[] _pco = default((uint, bool, byte[])[]);
        public (uint, bool, byte[])[] Pco
        {
            get
            {
                return _pco;
            }

            set
            {
                _pco = (value);
            }
        }

        private ObjectPath _initialEpsBearer = default(ObjectPath);
        public ObjectPath InitialEpsBearer
        {
            get
            {
                return _initialEpsBearer;
            }

            set
            {
                _initialEpsBearer = (value);
            }
        }

        private IDictionary<string, object> _initialEpsBearerSettings = default(IDictionary<string, object>);
        public IDictionary<string, object> InitialEpsBearerSettings
        {
            get
            {
                return _initialEpsBearerSettings;
            }

            set
            {
                _initialEpsBearerSettings = (value);
            }
        }

        private uint _packetServiceState = default(uint);
        public uint PacketServiceState
        {
            get
            {
                return _packetServiceState;
            }

            set
            {
                _packetServiceState = (value);
            }
        }

        private IDictionary<string, object> _nr5gRegistrationSettings = default(IDictionary<string, object>);
        public IDictionary<string, object> Nr5gRegistrationSettings
        {
            get
            {
                return _nr5gRegistrationSettings;
            }

            set
            {
                _nr5gRegistrationSettings = (value);
            }
        }
    }

    static class Modem3gppExtensions
    {
        public static Task<string> GetImeiAsync(this IModem3gpp o) => o.GetAsync<string>("Imei");
        public static Task<uint> GetRegistrationStateAsync(this IModem3gpp o) => o.GetAsync<uint>("RegistrationState");
        public static Task<string> GetOperatorCodeAsync(this IModem3gpp o) => o.GetAsync<string>("OperatorCode");
        public static Task<string> GetOperatorNameAsync(this IModem3gpp o) => o.GetAsync<string>("OperatorName");
        public static Task<uint> GetEnabledFacilityLocksAsync(this IModem3gpp o) => o.GetAsync<uint>("EnabledFacilityLocks");
        public static Task<uint> GetSubscriptionStateAsync(this IModem3gpp o) => o.GetAsync<uint>("SubscriptionState");
        public static Task<uint> GetEpsUeModeOperationAsync(this IModem3gpp o) => o.GetAsync<uint>("EpsUeModeOperation");
        public static Task<(uint, bool, byte[])[]> GetPcoAsync(this IModem3gpp o) => o.GetAsync<(uint, bool, byte[])[]>("Pco");
        public static Task<ObjectPath> GetInitialEpsBearerAsync(this IModem3gpp o) => o.GetAsync<ObjectPath>("InitialEpsBearer");
        public static Task<IDictionary<string, object>> GetInitialEpsBearerSettingsAsync(this IModem3gpp o) => o.GetAsync<IDictionary<string, object>>("InitialEpsBearerSettings");
        public static Task<uint> GetPacketServiceStateAsync(this IModem3gpp o) => o.GetAsync<uint>("PacketServiceState");
        public static Task<IDictionary<string, object>> GetNr5gRegistrationSettingsAsync(this IModem3gpp o) => o.GetAsync<IDictionary<string, object>>("Nr5gRegistrationSettings");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Modem.Voice")]
    interface IVoice : IDBusObject
    {
        Task<ObjectPath[]> ListCallsAsync();
        Task DeleteCallAsync(ObjectPath Path);
        Task<ObjectPath> CreateCallAsync(IDictionary<string, object> Properties);
        Task HoldAndAcceptAsync();
        Task HangupAndAcceptAsync();
        Task HangupAllAsync();
        Task TransferAsync();
        Task CallWaitingSetupAsync(bool Enable);
        Task<bool> CallWaitingQueryAsync();
        Task<IDisposable> WatchCallAddedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchCallDeletedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<VoiceProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class VoiceProperties
    {
        private ObjectPath[] _calls = default(ObjectPath[]);
        public ObjectPath[] Calls
        {
            get
            {
                return _calls;
            }

            set
            {
                _calls = (value);
            }
        }

        private bool _emergencyOnly = default(bool);
        public bool EmergencyOnly
        {
            get
            {
                return _emergencyOnly;
            }

            set
            {
                _emergencyOnly = (value);
            }
        }
    }

    static class VoiceExtensions
    {
        public static Task<ObjectPath[]> GetCallsAsync(this IVoice o) => o.GetAsync<ObjectPath[]>("Calls");
        public static Task<bool> GetEmergencyOnlyAsync(this IVoice o) => o.GetAsync<bool>("EmergencyOnly");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Sim")]
    interface ISim : IDBusObject
    {
        Task SendPinAsync(string Pin);
        Task SendPukAsync(string Puk, string Pin);
        Task EnablePinAsync(string Pin, bool Enabled);
        Task ChangePinAsync(string OldPin, string NewPin);
        Task SetPreferredNetworksAsync((string, uint)[] PreferredNetworks);
        Task<T> GetAsync<T>(string prop);
        Task<SimProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class SimProperties
    {
        private bool _active = default(bool);
        public bool Active
        {
            get
            {
                return _active;
            }

            set
            {
                _active = (value);
            }
        }

        private string _simIdentifier = default(string);
        public string SimIdentifier
        {
            get
            {
                return _simIdentifier;
            }

            set
            {
                _simIdentifier = (value);
            }
        }

        private string _imsi = default(string);
        public string Imsi
        {
            get
            {
                return _imsi;
            }

            set
            {
                _imsi = (value);
            }
        }

        private string _eid = default(string);
        public string Eid
        {
            get
            {
                return _eid;
            }

            set
            {
                _eid = (value);
            }
        }

        private string _operatorIdentifier = default(string);
        public string OperatorIdentifier
        {
            get
            {
                return _operatorIdentifier;
            }

            set
            {
                _operatorIdentifier = (value);
            }
        }

        private string _operatorName = default(string);
        public string OperatorName
        {
            get
            {
                return _operatorName;
            }

            set
            {
                _operatorName = (value);
            }
        }

        private string[] _emergencyNumbers = default(string[]);
        public string[] EmergencyNumbers
        {
            get
            {
                return _emergencyNumbers;
            }

            set
            {
                _emergencyNumbers = (value);
            }
        }

        private (string, uint)[] _preferredNetworks = default((string, uint)[]);
        public (string, uint)[] PreferredNetworks
        {
            get
            {
                return _preferredNetworks;
            }

            set
            {
                _preferredNetworks = (value);
            }
        }

        private byte[] _gid1 = default(byte[]);
        public byte[] Gid1
        {
            get
            {
                return _gid1;
            }

            set
            {
                _gid1 = (value);
            }
        }

        private byte[] _gid2 = default(byte[]);
        public byte[] Gid2
        {
            get
            {
                return _gid2;
            }

            set
            {
                _gid2 = (value);
            }
        }

        private uint _simType = default(uint);
        public uint SimType
        {
            get
            {
                return _simType;
            }

            set
            {
                _simType = (value);
            }
        }

        private uint _esimStatus = default(uint);
        public uint EsimStatus
        {
            get
            {
                return _esimStatus;
            }

            set
            {
                _esimStatus = (value);
            }
        }

        private uint _removability = default(uint);
        public uint Removability
        {
            get
            {
                return _removability;
            }

            set
            {
                _removability = (value);
            }
        }
    }

    static class SimExtensions
    {
        public static Task<bool> GetActiveAsync(this ISim o) => o.GetAsync<bool>("Active");
        public static Task<string> GetSimIdentifierAsync(this ISim o) => o.GetAsync<string>("SimIdentifier");
        public static Task<string> GetImsiAsync(this ISim o) => o.GetAsync<string>("Imsi");
        public static Task<string> GetEidAsync(this ISim o) => o.GetAsync<string>("Eid");
        public static Task<string> GetOperatorIdentifierAsync(this ISim o) => o.GetAsync<string>("OperatorIdentifier");
        public static Task<string> GetOperatorNameAsync(this ISim o) => o.GetAsync<string>("OperatorName");
        public static Task<string[]> GetEmergencyNumbersAsync(this ISim o) => o.GetAsync<string[]>("EmergencyNumbers");
        public static Task<(string, uint)[]> GetPreferredNetworksAsync(this ISim o) => o.GetAsync<(string, uint)[]>("PreferredNetworks");
        public static Task<byte[]> GetGid1Async(this ISim o) => o.GetAsync<byte[]>("Gid1");
        public static Task<byte[]> GetGid2Async(this ISim o) => o.GetAsync<byte[]>("Gid2");
        public static Task<uint> GetSimTypeAsync(this ISim o) => o.GetAsync<uint>("SimType");
        public static Task<uint> GetEsimStatusAsync(this ISim o) => o.GetAsync<uint>("EsimStatus");
        public static Task<uint> GetRemovabilityAsync(this ISim o) => o.GetAsync<uint>("Removability");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Sms")]
    interface ISms : IDBusObject
    {
        Task SendAsync();
        Task StoreAsync(uint Storage);
        Task<T> GetAsync<T>(string prop);
        Task<SmsProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class SmsProperties
    {
        private uint _state = default(uint);
        public uint State
        {
            get
            {
                return _state;
            }

            set
            {
                _state = (value);
            }
        }

        private uint _pduType = default(uint);
        public uint PduType
        {
            get
            {
                return _pduType;
            }

            set
            {
                _pduType = (value);
            }
        }

        private string _number = default(string);
        public string Number
        {
            get
            {
                return _number;
            }

            set
            {
                _number = (value);
            }
        }

        private string _text = default(string);
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = (value);
            }
        }

        private byte[] _data = default(byte[]);
        public byte[] Data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = (value);
            }
        }

        private string _sMSC = default(string);
        public string SMSC
        {
            get
            {
                return _sMSC;
            }

            set
            {
                _sMSC = (value);
            }
        }

        private (uint, object) _validity = default((uint, object));
        public (uint, object) Validity
        {
            get
            {
                return _validity;
            }

            set
            {
                _validity = (value);
            }
        }

        private int _class = default(int);
        public int Class
        {
            get
            {
                return _class;
            }

            set
            {
                _class = (value);
            }
        }

        private uint _teleserviceId = default(uint);
        public uint TeleserviceId
        {
            get
            {
                return _teleserviceId;
            }

            set
            {
                _teleserviceId = (value);
            }
        }

        private uint _serviceCategory = default(uint);
        public uint ServiceCategory
        {
            get
            {
                return _serviceCategory;
            }

            set
            {
                _serviceCategory = (value);
            }
        }

        private bool _deliveryReportRequest = default(bool);
        public bool DeliveryReportRequest
        {
            get
            {
                return _deliveryReportRequest;
            }

            set
            {
                _deliveryReportRequest = (value);
            }
        }

        private uint _messageReference = default(uint);
        public uint MessageReference
        {
            get
            {
                return _messageReference;
            }

            set
            {
                _messageReference = (value);
            }
        }

        private string _timestamp = default(string);
        public string Timestamp
        {
            get
            {
                return _timestamp;
            }

            set
            {
                _timestamp = (value);
            }
        }

        private string _dischargeTimestamp = default(string);
        public string DischargeTimestamp
        {
            get
            {
                return _dischargeTimestamp;
            }

            set
            {
                _dischargeTimestamp = (value);
            }
        }

        private uint _deliveryState = default(uint);
        public uint DeliveryState
        {
            get
            {
                return _deliveryState;
            }

            set
            {
                _deliveryState = (value);
            }
        }

        private uint _storage = default(uint);
        public uint Storage
        {
            get
            {
                return _storage;
            }

            set
            {
                _storage = (value);
            }
        }
    }

    static class SmsExtensions
    {
        public static Task<uint> GetStateAsync(this ISms o) => o.GetAsync<uint>("State");
        public static Task<uint> GetPduTypeAsync(this ISms o) => o.GetAsync<uint>("PduType");
        public static Task<string> GetNumberAsync(this ISms o) => o.GetAsync<string>("Number");
        public static Task<string> GetTextAsync(this ISms o) => o.GetAsync<string>("Text");
        public static Task<byte[]> GetDataAsync(this ISms o) => o.GetAsync<byte[]>("Data");
        public static Task<string> GetSMSCAsync(this ISms o) => o.GetAsync<string>("SMSC");
        public static Task<(uint, object)> GetValidityAsync(this ISms o) => o.GetAsync<(uint, object)>("Validity");
        public static Task<int> GetClassAsync(this ISms o) => o.GetAsync<int>("Class");
        public static Task<uint> GetTeleserviceIdAsync(this ISms o) => o.GetAsync<uint>("TeleserviceId");
        public static Task<uint> GetServiceCategoryAsync(this ISms o) => o.GetAsync<uint>("ServiceCategory");
        public static Task<bool> GetDeliveryReportRequestAsync(this ISms o) => o.GetAsync<bool>("DeliveryReportRequest");
        public static Task<uint> GetMessageReferenceAsync(this ISms o) => o.GetAsync<uint>("MessageReference");
        public static Task<string> GetTimestampAsync(this ISms o) => o.GetAsync<string>("Timestamp");
        public static Task<string> GetDischargeTimestampAsync(this ISms o) => o.GetAsync<string>("DischargeTimestamp");
        public static Task<uint> GetDeliveryStateAsync(this ISms o) => o.GetAsync<uint>("DeliveryState");
        public static Task<uint> GetStorageAsync(this ISms o) => o.GetAsync<uint>("Storage");
    }

    [DBusInterface("org.freedesktop.ModemManager1.Bearer")]
    interface IBearer : IDBusObject
    {
        Task ConnectAsync();
        Task DisconnectAsync();
        Task<T> GetAsync<T>(string prop);
        Task<BearerProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class BearerProperties
    {
        private string _interface = default(string);
        public string Interface
        {
            get
            {
                return _interface;
            }

            set
            {
                _interface = (value);
            }
        }

        private bool _connected = default(bool);
        public bool Connected
        {
            get
            {
                return _connected;
            }

            set
            {
                _connected = (value);
            }
        }

        private (string, string) _connectionError = default((string, string));
        public (string, string) ConnectionError
        {
            get
            {
                return _connectionError;
            }

            set
            {
                _connectionError = (value);
            }
        }

        private bool _suspended = default(bool);
        public bool Suspended
        {
            get
            {
                return _suspended;
            }

            set
            {
                _suspended = (value);
            }
        }

        private bool _multiplexed = default(bool);
        public bool Multiplexed
        {
            get
            {
                return _multiplexed;
            }

            set
            {
                _multiplexed = (value);
            }
        }

        private IDictionary<string, object> _ip4Config = default(IDictionary<string, object>);
        public IDictionary<string, object> Ip4Config
        {
            get
            {
                return _ip4Config;
            }

            set
            {
                _ip4Config = (value);
            }
        }

        private IDictionary<string, object> _ip6Config = default(IDictionary<string, object>);
        public IDictionary<string, object> Ip6Config
        {
            get
            {
                return _ip6Config;
            }

            set
            {
                _ip6Config = (value);
            }
        }

        private IDictionary<string, object> _stats = default(IDictionary<string, object>);
        public IDictionary<string, object> Stats
        {
            get
            {
                return _stats;
            }

            set
            {
                _stats = (value);
            }
        }

        private bool _reloadStatsSupported = default(bool);
        public bool ReloadStatsSupported
        {
            get
            {
                return _reloadStatsSupported;
            }

            set
            {
                _reloadStatsSupported = (value);
            }
        }

        private uint _ipTimeout = default(uint);
        public uint IpTimeout
        {
            get
            {
                return _ipTimeout;
            }

            set
            {
                _ipTimeout = (value);
            }
        }

        private uint _bearerType = default(uint);
        public uint BearerType
        {
            get
            {
                return _bearerType;
            }

            set
            {
                _bearerType = (value);
            }
        }

        private int _profileId = default(int);
        public int ProfileId
        {
            get
            {
                return _profileId;
            }

            set
            {
                _profileId = (value);
            }
        }

        private IDictionary<string, object> _properties = default(IDictionary<string, object>);
        public IDictionary<string, object> Properties
        {
            get
            {
                return _properties;
            }

            set
            {
                _properties = (value);
            }
        }
    }

    static class BearerExtensions
    {
        public static Task<string> GetInterfaceAsync(this IBearer o) => o.GetAsync<string>("Interface");
        public static Task<bool> GetConnectedAsync(this IBearer o) => o.GetAsync<bool>("Connected");
        public static Task<(string, string)> GetConnectionErrorAsync(this IBearer o) => o.GetAsync<(string, string)>("ConnectionError");
        public static Task<bool> GetSuspendedAsync(this IBearer o) => o.GetAsync<bool>("Suspended");
        public static Task<bool> GetMultiplexedAsync(this IBearer o) => o.GetAsync<bool>("Multiplexed");
        public static Task<IDictionary<string, object>> GetIp4ConfigAsync(this IBearer o) => o.GetAsync<IDictionary<string, object>>("Ip4Config");
        public static Task<IDictionary<string, object>> GetIp6ConfigAsync(this IBearer o) => o.GetAsync<IDictionary<string, object>>("Ip6Config");
        public static Task<IDictionary<string, object>> GetStatsAsync(this IBearer o) => o.GetAsync<IDictionary<string, object>>("Stats");
        public static Task<bool> GetReloadStatsSupportedAsync(this IBearer o) => o.GetAsync<bool>("ReloadStatsSupported");
        public static Task<uint> GetIpTimeoutAsync(this IBearer o) => o.GetAsync<uint>("IpTimeout");
        public static Task<uint> GetBearerTypeAsync(this IBearer o) => o.GetAsync<uint>("BearerType");
        public static Task<int> GetProfileIdAsync(this IBearer o) => o.GetAsync<int>("ProfileId");
        public static Task<IDictionary<string, object>> GetPropertiesAsync(this IBearer o) => o.GetAsync<IDictionary<string, object>>("Properties");
    }

    #endregion

    #region NetworkManager

    [DBusInterface("org.freedesktop.NetworkManager")]
    interface INetworkManager : IDBusObject
    {
        Task ReloadAsync(uint Flags);
        Task<ObjectPath[]> GetDevicesAsync();
        Task<ObjectPath[]> GetAllDevicesAsync();
        Task<ObjectPath> GetDeviceByIpIfaceAsync(string Iface);
        Task<ObjectPath> ActivateConnectionAsync(ObjectPath Connection, ObjectPath Device, ObjectPath SpecificObject);
        Task<(ObjectPath path, ObjectPath activeConnection)> AddAndActivateConnectionAsync(IDictionary<string, IDictionary<string, object>> Connection, ObjectPath Device, ObjectPath SpecificObject);
        Task<(ObjectPath path, ObjectPath activeConnection, IDictionary<string, object> result)> AddAndActivateConnection2Async(IDictionary<string, IDictionary<string, object>> Connection, ObjectPath Device, ObjectPath SpecificObject, IDictionary<string, object> Options);
        Task DeactivateConnectionAsync(ObjectPath ActiveConnection);
        Task SleepAsync(bool Sleep);
        Task EnableAsync(bool Enable);
        Task<IDictionary<string, string>> GetPermissionsAsync();
        Task SetLoggingAsync(string Level, string Domains);
        Task<(string level, string domains)> GetLoggingAsync();
        Task<uint> CheckConnectivityAsync();
        Task<uint> stateAsync();
        Task<ObjectPath> CheckpointCreateAsync(ObjectPath[] Devices, uint RollbackTimeout, uint Flags);
        Task CheckpointDestroyAsync(ObjectPath Checkpoint);
        Task<IDictionary<string, uint>> CheckpointRollbackAsync(ObjectPath Checkpoint);
        Task CheckpointAdjustRollbackTimeoutAsync(ObjectPath Checkpoint, uint AddTimeout);
        Task<IDisposable> WatchCheckPermissionsAsync(Action handler, Action<Exception> onError = null);
        Task<IDisposable> WatchStateChangedAsync(Action<uint> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchDeviceAddedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchDeviceRemovedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<NetworkManagerProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class NetworkManagerProperties
    {
        private ObjectPath[] _Devices = default(ObjectPath[]);
        public ObjectPath[] Devices
        {
            get
            {
                return _Devices;
            }

            set
            {
                _Devices = (value);
            }
        }

        private ObjectPath[] _AllDevices = default(ObjectPath[]);
        public ObjectPath[] AllDevices
        {
            get
            {
                return _AllDevices;
            }

            set
            {
                _AllDevices = (value);
            }
        }

        private ObjectPath[] _Checkpoints = default(ObjectPath[]);
        public ObjectPath[] Checkpoints
        {
            get
            {
                return _Checkpoints;
            }

            set
            {
                _Checkpoints = (value);
            }
        }

        private bool _NetworkingEnabled = default(bool);
        public bool NetworkingEnabled
        {
            get
            {
                return _NetworkingEnabled;
            }

            set
            {
                _NetworkingEnabled = (value);
            }
        }

        private bool _WirelessEnabled = default(bool);
        public bool WirelessEnabled
        {
            get
            {
                return _WirelessEnabled;
            }

            set
            {
                _WirelessEnabled = (value);
            }
        }

        private bool _WirelessHardwareEnabled = default(bool);
        public bool WirelessHardwareEnabled
        {
            get
            {
                return _WirelessHardwareEnabled;
            }

            set
            {
                _WirelessHardwareEnabled = (value);
            }
        }

        private bool _WwanEnabled = default(bool);
        public bool WwanEnabled
        {
            get
            {
                return _WwanEnabled;
            }

            set
            {
                _WwanEnabled = (value);
            }
        }

        private bool _WwanHardwareEnabled = default(bool);
        public bool WwanHardwareEnabled
        {
            get
            {
                return _WwanHardwareEnabled;
            }

            set
            {
                _WwanHardwareEnabled = (value);
            }
        }

        private bool _WimaxEnabled = default(bool);
        public bool WimaxEnabled
        {
            get
            {
                return _WimaxEnabled;
            }

            set
            {
                _WimaxEnabled = (value);
            }
        }

        private bool _WimaxHardwareEnabled = default(bool);
        public bool WimaxHardwareEnabled
        {
            get
            {
                return _WimaxHardwareEnabled;
            }

            set
            {
                _WimaxHardwareEnabled = (value);
            }
        }

        private ObjectPath[] _ActiveConnections = default(ObjectPath[]);
        public ObjectPath[] ActiveConnections
        {
            get
            {
                return _ActiveConnections;
            }

            set
            {
                _ActiveConnections = (value);
            }
        }

        private ObjectPath _PrimaryConnection = default(ObjectPath);
        public ObjectPath PrimaryConnection
        {
            get
            {
                return _PrimaryConnection;
            }

            set
            {
                _PrimaryConnection = (value);
            }
        }

        private string _PrimaryConnectionType = default(string);
        public string PrimaryConnectionType
        {
            get
            {
                return _PrimaryConnectionType;
            }

            set
            {
                _PrimaryConnectionType = (value);
            }
        }

        private uint _Metered = default(uint);
        public uint Metered
        {
            get
            {
                return _Metered;
            }

            set
            {
                _Metered = (value);
            }
        }

        private ObjectPath _ActivatingConnection = default(ObjectPath);
        public ObjectPath ActivatingConnection
        {
            get
            {
                return _ActivatingConnection;
            }

            set
            {
                _ActivatingConnection = (value);
            }
        }

        private bool _Startup = default(bool);
        public bool Startup
        {
            get
            {
                return _Startup;
            }

            set
            {
                _Startup = (value);
            }
        }

        private string _Version = default(string);
        public string Version
        {
            get
            {
                return _Version;
            }

            set
            {
                _Version = (value);
            }
        }

        private uint[] _Capabilities = default(uint[]);
        public uint[] Capabilities
        {
            get
            {
                return _Capabilities;
            }

            set
            {
                _Capabilities = (value);
            }
        }

        private uint _State = default(uint);
        public uint State
        {
            get
            {
                return _State;
            }

            set
            {
                _State = (value);
            }
        }

        private uint _Connectivity = default(uint);
        public uint Connectivity
        {
            get
            {
                return _Connectivity;
            }

            set
            {
                _Connectivity = (value);
            }
        }

        private bool _ConnectivityCheckAvailable = default(bool);
        public bool ConnectivityCheckAvailable
        {
            get
            {
                return _ConnectivityCheckAvailable;
            }

            set
            {
                _ConnectivityCheckAvailable = (value);
            }
        }

        private bool _ConnectivityCheckEnabled = default(bool);
        public bool ConnectivityCheckEnabled
        {
            get
            {
                return _ConnectivityCheckEnabled;
            }

            set
            {
                _ConnectivityCheckEnabled = (value);
            }
        }

        private string _ConnectivityCheckUri = default(string);
        public string ConnectivityCheckUri
        {
            get
            {
                return _ConnectivityCheckUri;
            }

            set
            {
                _ConnectivityCheckUri = (value);
            }
        }

        private IDictionary<string, object> _GlobalDnsConfiguration = default(IDictionary<string, object>);
        public IDictionary<string, object> GlobalDnsConfiguration
        {
            get
            {
                return _GlobalDnsConfiguration;
            }

            set
            {
                _GlobalDnsConfiguration = (value);
            }
        }
    }

    static class NetworkManagerExtensions
    {
        public static Task<ObjectPath[]> GetDevicesAsync(this INetworkManager o) => o.GetAsync<ObjectPath[]>("Devices");
        public static Task<ObjectPath[]> GetAllDevicesAsync(this INetworkManager o) => o.GetAsync<ObjectPath[]>("AllDevices");
        public static Task<ObjectPath[]> GetCheckpointsAsync(this INetworkManager o) => o.GetAsync<ObjectPath[]>("Checkpoints");
        public static Task<bool> GetNetworkingEnabledAsync(this INetworkManager o) => o.GetAsync<bool>("NetworkingEnabled");
        public static Task<bool> GetWirelessEnabledAsync(this INetworkManager o) => o.GetAsync<bool>("WirelessEnabled");
        public static Task<bool> GetWirelessHardwareEnabledAsync(this INetworkManager o) => o.GetAsync<bool>("WirelessHardwareEnabled");
        public static Task<bool> GetWwanEnabledAsync(this INetworkManager o) => o.GetAsync<bool>("WwanEnabled");
        public static Task<bool> GetWwanHardwareEnabledAsync(this INetworkManager o) => o.GetAsync<bool>("WwanHardwareEnabled");
        public static Task<bool> GetWimaxEnabledAsync(this INetworkManager o) => o.GetAsync<bool>("WimaxEnabled");
        public static Task<bool> GetWimaxHardwareEnabledAsync(this INetworkManager o) => o.GetAsync<bool>("WimaxHardwareEnabled");
        public static Task<ObjectPath[]> GetActiveConnectionsAsync(this INetworkManager o) => o.GetAsync<ObjectPath[]>("ActiveConnections");
        public static Task<ObjectPath> GetPrimaryConnectionAsync(this INetworkManager o) => o.GetAsync<ObjectPath>("PrimaryConnection");
        public static Task<string> GetPrimaryConnectionTypeAsync(this INetworkManager o) => o.GetAsync<string>("PrimaryConnectionType");
        public static Task<uint> GetMeteredAsync(this INetworkManager o) => o.GetAsync<uint>("Metered");
        public static Task<ObjectPath> GetActivatingConnectionAsync(this INetworkManager o) => o.GetAsync<ObjectPath>("ActivatingConnection");
        public static Task<bool> GetStartupAsync(this INetworkManager o) => o.GetAsync<bool>("Startup");
        public static Task<string> GetVersionAsync(this INetworkManager o) => o.GetAsync<string>("Version");
        public static Task<uint[]> GetCapabilitiesAsync(this INetworkManager o) => o.GetAsync<uint[]>("Capabilities");
        public static Task<uint> GetStateAsync(this INetworkManager o) => o.GetAsync<uint>("State");
        public static Task<uint> GetConnectivityAsync(this INetworkManager o) => o.GetAsync<uint>("Connectivity");
        public static Task<bool> GetConnectivityCheckAvailableAsync(this INetworkManager o) => o.GetAsync<bool>("ConnectivityCheckAvailable");
        public static Task<bool> GetConnectivityCheckEnabledAsync(this INetworkManager o) => o.GetAsync<bool>("ConnectivityCheckEnabled");
        public static Task<string> GetConnectivityCheckUriAsync(this INetworkManager o) => o.GetAsync<string>("ConnectivityCheckUri");
        public static Task<IDictionary<string, object>> GetGlobalDnsConfigurationAsync(this INetworkManager o) => o.GetAsync<IDictionary<string, object>>("GlobalDnsConfiguration");
        public static Task SetWirelessEnabledAsync(this INetworkManager o, bool val) => o.SetAsync("WirelessEnabled", val);
        public static Task SetWwanEnabledAsync(this INetworkManager o, bool val) => o.SetAsync("WwanEnabled", val);
        public static Task SetWimaxEnabledAsync(this INetworkManager o, bool val) => o.SetAsync("WimaxEnabled", val);
        public static Task SetConnectivityCheckEnabledAsync(this INetworkManager o, bool val) => o.SetAsync("ConnectivityCheckEnabled", val);
        public static Task SetGlobalDnsConfigurationAsync(this INetworkManager o, IDictionary<string, object> val) => o.SetAsync("GlobalDnsConfiguration", val);
    }

    [DBusInterface("org.freedesktop.NetworkManager.IP4Config")]
    interface IIP4Config : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<IP4ConfigProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class IP4ConfigProperties
    {
        private uint[][] _Addresses = default(uint[][]);
        public uint[][] Addresses
        {
            get
            {
                return _Addresses;
            }

            set
            {
                _Addresses = (value);
            }
        }

        private IDictionary<string, object>[] _AddressData = default(IDictionary<string, object>[]);
        public IDictionary<string, object>[] AddressData
        {
            get
            {
                return _AddressData;
            }

            set
            {
                _AddressData = (value);
            }
        }

        private string _Gateway = default(string);
        public string Gateway
        {
            get
            {
                return _Gateway;
            }

            set
            {
                _Gateway = (value);
            }
        }

        private uint[][] _Routes = default(uint[][]);
        public uint[][] Routes
        {
            get
            {
                return _Routes;
            }

            set
            {
                _Routes = (value);
            }
        }

        private IDictionary<string, object>[] _RouteData = default(IDictionary<string, object>[]);
        public IDictionary<string, object>[] RouteData
        {
            get
            {
                return _RouteData;
            }

            set
            {
                _RouteData = (value);
            }
        }

        private IDictionary<string, object>[] _NameserverData = default(IDictionary<string, object>[]);
        public IDictionary<string, object>[] NameserverData
        {
            get
            {
                return _NameserverData;
            }

            set
            {
                _NameserverData = (value);
            }
        }

        private uint[] _Nameservers = default(uint[]);
        public uint[] Nameservers
        {
            get
            {
                return _Nameservers;
            }

            set
            {
                _Nameservers = (value);
            }
        }

        private string[] _Domains = default(string[]);
        public string[] Domains
        {
            get
            {
                return _Domains;
            }

            set
            {
                _Domains = (value);
            }
        }

        private string[] _Searches = default(string[]);
        public string[] Searches
        {
            get
            {
                return _Searches;
            }

            set
            {
                _Searches = (value);
            }
        }

        private string[] _DnsOptions = default(string[]);
        public string[] DnsOptions
        {
            get
            {
                return _DnsOptions;
            }

            set
            {
                _DnsOptions = (value);
            }
        }

        private int _DnsPriority = default(int);
        public int DnsPriority
        {
            get
            {
                return _DnsPriority;
            }

            set
            {
                _DnsPriority = (value);
            }
        }

        private string[] _WinsServerData = default(string[]);
        public string[] WinsServerData
        {
            get
            {
                return _WinsServerData;
            }

            set
            {
                _WinsServerData = (value);
            }
        }

        private uint[] _WinsServers = default(uint[]);
        public uint[] WinsServers
        {
            get
            {
                return _WinsServers;
            }

            set
            {
                _WinsServers = (value);
            }
        }
    }

    static class IP4ConfigExtensions
    {
        public static Task<uint[][]> GetAddressesAsync(this IIP4Config o) => o.GetAsync<uint[][]>("Addresses");
        public static Task<IDictionary<string, object>[]> GetAddressDataAsync(this IIP4Config o) => o.GetAsync<IDictionary<string, object>[]>("AddressData");
        public static Task<string> GetGatewayAsync(this IIP4Config o) => o.GetAsync<string>("Gateway");
        public static Task<uint[][]> GetRoutesAsync(this IIP4Config o) => o.GetAsync<uint[][]>("Routes");
        public static Task<IDictionary<string, object>[]> GetRouteDataAsync(this IIP4Config o) => o.GetAsync<IDictionary<string, object>[]>("RouteData");
        public static Task<IDictionary<string, object>[]> GetNameserverDataAsync(this IIP4Config o) => o.GetAsync<IDictionary<string, object>[]>("NameserverData");
        public static Task<uint[]> GetNameserversAsync(this IIP4Config o) => o.GetAsync<uint[]>("Nameservers");
        public static Task<string[]> GetDomainsAsync(this IIP4Config o) => o.GetAsync<string[]>("Domains");
        public static Task<string[]> GetSearchesAsync(this IIP4Config o) => o.GetAsync<string[]>("Searches");
        public static Task<string[]> GetDnsOptionsAsync(this IIP4Config o) => o.GetAsync<string[]>("DnsOptions");
        public static Task<int> GetDnsPriorityAsync(this IIP4Config o) => o.GetAsync<int>("DnsPriority");
        public static Task<string[]> GetWinsServerDataAsync(this IIP4Config o) => o.GetAsync<string[]>("WinsServerData");
        public static Task<uint[]> GetWinsServersAsync(this IIP4Config o) => o.GetAsync<uint[]>("WinsServers");
    }

    [DBusInterface("org.freedesktop.NetworkManager.Connection.Active")]
    interface IActive : IDBusObject
    {
        Task<IDisposable> WatchStateChangedAsync(Action<(uint state, uint reason)> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<ActiveProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class ActiveProperties
    {
        private ObjectPath _Connection = default(ObjectPath);
        public ObjectPath Connection
        {
            get
            {
                return _Connection;
            }

            set
            {
                _Connection = (value);
            }
        }

        private ObjectPath _SpecificObject = default(ObjectPath);
        public ObjectPath SpecificObject
        {
            get
            {
                return _SpecificObject;
            }

            set
            {
                _SpecificObject = (value);
            }
        }

        private string _Id = default(string);
        public string Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = (value);
            }
        }

        private string _Uuid = default(string);
        public string Uuid
        {
            get
            {
                return _Uuid;
            }

            set
            {
                _Uuid = (value);
            }
        }

        private string _Type = default(string);
        public string Type
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = (value);
            }
        }

        private ObjectPath[] _Devices = default(ObjectPath[]);
        public ObjectPath[] Devices
        {
            get
            {
                return _Devices;
            }

            set
            {
                _Devices = (value);
            }
        }

        private uint _State = default(uint);
        public uint State
        {
            get
            {
                return _State;
            }

            set
            {
                _State = (value);
            }
        }

        private uint _StateFlags = default(uint);
        public uint StateFlags
        {
            get
            {
                return _StateFlags;
            }

            set
            {
                _StateFlags = (value);
            }
        }

        private bool _Default = default(bool);
        public bool Default
        {
            get
            {
                return _Default;
            }

            set
            {
                _Default = (value);
            }
        }

        private ObjectPath _Ip4Config = default(ObjectPath);
        public ObjectPath Ip4Config
        {
            get
            {
                return _Ip4Config;
            }

            set
            {
                _Ip4Config = (value);
            }
        }

        private ObjectPath _Dhcp4Config = default(ObjectPath);
        public ObjectPath Dhcp4Config
        {
            get
            {
                return _Dhcp4Config;
            }

            set
            {
                _Dhcp4Config = (value);
            }
        }

        private bool _Default6 = default(bool);
        public bool Default6
        {
            get
            {
                return _Default6;
            }

            set
            {
                _Default6 = (value);
            }
        }

        private ObjectPath _Ip6Config = default(ObjectPath);
        public ObjectPath Ip6Config
        {
            get
            {
                return _Ip6Config;
            }

            set
            {
                _Ip6Config = (value);
            }
        }

        private ObjectPath _Dhcp6Config = default(ObjectPath);
        public ObjectPath Dhcp6Config
        {
            get
            {
                return _Dhcp6Config;
            }

            set
            {
                _Dhcp6Config = (value);
            }
        }

        private bool _Vpn = default(bool);
        public bool Vpn
        {
            get
            {
                return _Vpn;
            }

            set
            {
                _Vpn = (value);
            }
        }

        private ObjectPath _Master = default(ObjectPath);
        public ObjectPath Master
        {
            get
            {
                return _Master;
            }

            set
            {
                _Master = (value);
            }
        }
    }

    static class ActiveExtensions
    {
        public static Task<ObjectPath> GetConnectionAsync(this IActive o) => o.GetAsync<ObjectPath>("Connection");
        public static Task<ObjectPath> GetSpecificObjectAsync(this IActive o) => o.GetAsync<ObjectPath>("SpecificObject");
        public static Task<string> GetIdAsync(this IActive o) => o.GetAsync<string>("Id");
        public static Task<string> GetUuidAsync(this IActive o) => o.GetAsync<string>("Uuid");
        public static Task<string> GetTypeAsync(this IActive o) => o.GetAsync<string>("Type");
        public static Task<ObjectPath[]> GetDevicesAsync(this IActive o) => o.GetAsync<ObjectPath[]>("Devices");
        public static Task<uint> GetStateAsync(this IActive o) => o.GetAsync<uint>("State");
        public static Task<uint> GetStateFlagsAsync(this IActive o) => o.GetAsync<uint>("StateFlags");
        public static Task<bool> GetDefaultAsync(this IActive o) => o.GetAsync<bool>("Default");
        public static Task<ObjectPath> GetIp4ConfigAsync(this IActive o) => o.GetAsync<ObjectPath>("Ip4Config");
        public static Task<ObjectPath> GetDhcp4ConfigAsync(this IActive o) => o.GetAsync<ObjectPath>("Dhcp4Config");
        public static Task<bool> GetDefault6Async(this IActive o) => o.GetAsync<bool>("Default6");
        public static Task<ObjectPath> GetIp6ConfigAsync(this IActive o) => o.GetAsync<ObjectPath>("Ip6Config");
        public static Task<ObjectPath> GetDhcp6ConfigAsync(this IActive o) => o.GetAsync<ObjectPath>("Dhcp6Config");
        public static Task<bool> GetVpnAsync(this IActive o) => o.GetAsync<bool>("Vpn");
        public static Task<ObjectPath> GetMasterAsync(this IActive o) => o.GetAsync<ObjectPath>("Master");
    }

    [DBusInterface("org.freedesktop.NetworkManager.AgentManager")]
    interface IAgentManager : IDBusObject
    {
        Task RegisterAsync(string Identifier);
        Task RegisterWithCapabilitiesAsync(string Identifier, uint Capabilities);
        Task UnregisterAsync();
    }

    [DBusInterface("org.freedesktop.NetworkManager.Device.Statistics")]
    interface IStatistics : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<StatisticsProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class StatisticsProperties
    {
        private uint _RefreshRateMs = default(uint);
        public uint RefreshRateMs
        {
            get
            {
                return _RefreshRateMs;
            }

            set
            {
                _RefreshRateMs = (value);
            }
        }

        private ulong _TxBytes = default(ulong);
        public ulong TxBytes
        {
            get
            {
                return _TxBytes;
            }

            set
            {
                _TxBytes = (value);
            }
        }

        private ulong _RxBytes = default(ulong);
        public ulong RxBytes
        {
            get
            {
                return _RxBytes;
            }

            set
            {
                _RxBytes = (value);
            }
        }
    }

    static class StatisticsExtensions
    {
        public static Task<uint> GetRefreshRateMsAsync(this IStatistics o) => o.GetAsync<uint>("RefreshRateMs");
        public static Task<ulong> GetTxBytesAsync(this IStatistics o) => o.GetAsync<ulong>("TxBytes");
        public static Task<ulong> GetRxBytesAsync(this IStatistics o) => o.GetAsync<ulong>("RxBytes");
        public static Task SetRefreshRateMsAsync(this IStatistics o, uint val) => o.SetAsync("RefreshRateMs", val);
    }

    [DBusInterface("org.freedesktop.NetworkManager.Device.Bridge")]
    interface IBridge : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<BridgeProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class BridgeProperties
    {
        private string _HwAddress = default(string);
        public string HwAddress
        {
            get
            {
                return _HwAddress;
            }

            set
            {
                _HwAddress = (value);
            }
        }

        private bool _Carrier = default(bool);
        public bool Carrier
        {
            get
            {
                return _Carrier;
            }

            set
            {
                _Carrier = (value);
            }
        }

        private ObjectPath[] _Slaves = default(ObjectPath[]);
        public ObjectPath[] Slaves
        {
            get
            {
                return _Slaves;
            }

            set
            {
                _Slaves = (value);
            }
        }
    }

    static class BridgeExtensions
    {
        public static Task<string> GetHwAddressAsync(this IBridge o) => o.GetAsync<string>("HwAddress");
        public static Task<bool> GetCarrierAsync(this IBridge o) => o.GetAsync<bool>("Carrier");
        public static Task<ObjectPath[]> GetSlavesAsync(this IBridge o) => o.GetAsync<ObjectPath[]>("Slaves");
    }

    [DBusInterface("org.freedesktop.NetworkManager.Device")]
    interface IDevice : IDBusObject
    {
        Task ReapplyAsync(IDictionary<string, IDictionary<string, object>> Connection, ulong VersionId, uint Flags);
        Task<(IDictionary<string, IDictionary<string, object>> connection, ulong versionId)> GetAppliedConnectionAsync(uint Flags);
        Task DisconnectAsync();
        Task DeleteAsync();
        Task<IDisposable> WatchStateChangedAsync(Action<(uint newState, uint oldState, uint reason)> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<DeviceProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class DeviceProperties
    {
        private string _Udi = default(string);
        public string Udi
        {
            get
            {
                return _Udi;
            }

            set
            {
                _Udi = (value);
            }
        }

        private string _Path = default(string);
        public string Path
        {
            get
            {
                return _Path;
            }

            set
            {
                _Path = (value);
            }
        }

        private string _Interface = default(string);
        public string Interface
        {
            get
            {
                return _Interface;
            }

            set
            {
                _Interface = (value);
            }
        }

        private string _IpInterface = default(string);
        public string IpInterface
        {
            get
            {
                return _IpInterface;
            }

            set
            {
                _IpInterface = (value);
            }
        }

        private string _Driver = default(string);
        public string Driver
        {
            get
            {
                return _Driver;
            }

            set
            {
                _Driver = (value);
            }
        }

        private string _DriverVersion = default(string);
        public string DriverVersion
        {
            get
            {
                return _DriverVersion;
            }

            set
            {
                _DriverVersion = (value);
            }
        }

        private string _FirmwareVersion = default(string);
        public string FirmwareVersion
        {
            get
            {
                return _FirmwareVersion;
            }

            set
            {
                _FirmwareVersion = (value);
            }
        }

        private uint _Capabilities = default(uint);
        public uint Capabilities
        {
            get
            {
                return _Capabilities;
            }

            set
            {
                _Capabilities = (value);
            }
        }

        private uint _Ip4Address = default(uint);
        public uint Ip4Address
        {
            get
            {
                return _Ip4Address;
            }

            set
            {
                _Ip4Address = (value);
            }
        }

        private uint _State = default(uint);
        public uint State
        {
            get
            {
                return _State;
            }

            set
            {
                _State = (value);
            }
        }

        private (uint, uint) _StateReason = default((uint, uint));
        public (uint, uint) StateReason
        {
            get
            {
                return _StateReason;
            }

            set
            {
                _StateReason = (value);
            }
        }

        private ObjectPath _ActiveConnection = default(ObjectPath);
        public ObjectPath ActiveConnection
        {
            get
            {
                return _ActiveConnection;
            }

            set
            {
                _ActiveConnection = (value);
            }
        }

        private ObjectPath _Ip4Config = default(ObjectPath);
        public ObjectPath Ip4Config
        {
            get
            {
                return _Ip4Config;
            }

            set
            {
                _Ip4Config = (value);
            }
        }

        private ObjectPath _Dhcp4Config = default(ObjectPath);
        public ObjectPath Dhcp4Config
        {
            get
            {
                return _Dhcp4Config;
            }

            set
            {
                _Dhcp4Config = (value);
            }
        }

        private ObjectPath _Ip6Config = default(ObjectPath);
        public ObjectPath Ip6Config
        {
            get
            {
                return _Ip6Config;
            }

            set
            {
                _Ip6Config = (value);
            }
        }

        private ObjectPath _Dhcp6Config = default(ObjectPath);
        public ObjectPath Dhcp6Config
        {
            get
            {
                return _Dhcp6Config;
            }

            set
            {
                _Dhcp6Config = (value);
            }
        }

        private bool _Managed = default(bool);
        public bool Managed
        {
            get
            {
                return _Managed;
            }

            set
            {
                _Managed = (value);
            }
        }

        private bool _Autoconnect = default(bool);
        public bool Autoconnect
        {
            get
            {
                return _Autoconnect;
            }

            set
            {
                _Autoconnect = (value);
            }
        }

        private bool _FirmwareMissing = default(bool);
        public bool FirmwareMissing
        {
            get
            {
                return _FirmwareMissing;
            }

            set
            {
                _FirmwareMissing = (value);
            }
        }

        private bool _NmPluginMissing = default(bool);
        public bool NmPluginMissing
        {
            get
            {
                return _NmPluginMissing;
            }

            set
            {
                _NmPluginMissing = (value);
            }
        }

        private uint _DeviceType = default(uint);
        public uint DeviceType
        {
            get
            {
                return _DeviceType;
            }

            set
            {
                _DeviceType = (value);
            }
        }

        private ObjectPath[] _AvailableConnections = default(ObjectPath[]);
        public ObjectPath[] AvailableConnections
        {
            get
            {
                return _AvailableConnections;
            }

            set
            {
                _AvailableConnections = (value);
            }
        }

        private string _PhysicalPortId = default(string);
        public string PhysicalPortId
        {
            get
            {
                return _PhysicalPortId;
            }

            set
            {
                _PhysicalPortId = (value);
            }
        }

        private uint _Mtu = default(uint);
        public uint Mtu
        {
            get
            {
                return _Mtu;
            }

            set
            {
                _Mtu = (value);
            }
        }

        private uint _Metered = default(uint);
        public uint Metered
        {
            get
            {
                return _Metered;
            }

            set
            {
                _Metered = (value);
            }
        }

        private IDictionary<string, object>[] _LldpNeighbors = default(IDictionary<string, object>[]);
        public IDictionary<string, object>[] LldpNeighbors
        {
            get
            {
                return _LldpNeighbors;
            }

            set
            {
                _LldpNeighbors = (value);
            }
        }

        private bool _Real = default(bool);
        public bool Real
        {
            get
            {
                return _Real;
            }

            set
            {
                _Real = (value);
            }
        }

        private uint _Ip4Connectivity = default(uint);
        public uint Ip4Connectivity
        {
            get
            {
                return _Ip4Connectivity;
            }

            set
            {
                _Ip4Connectivity = (value);
            }
        }

        private uint _Ip6Connectivity = default(uint);
        public uint Ip6Connectivity
        {
            get
            {
                return _Ip6Connectivity;
            }

            set
            {
                _Ip6Connectivity = (value);
            }
        }

        private uint _InterfaceFlags = default(uint);
        public uint InterfaceFlags
        {
            get
            {
                return _InterfaceFlags;
            }

            set
            {
                _InterfaceFlags = (value);
            }
        }

        private string _HwAddress = default(string);
        public string HwAddress
        {
            get
            {
                return _HwAddress;
            }

            set
            {
                _HwAddress = (value);
            }
        }

        private ObjectPath[] _Ports = default(ObjectPath[]);
        public ObjectPath[] Ports
        {
            get
            {
                return _Ports;
            }

            set
            {
                _Ports = (value);
            }
        }
    }

    static class DeviceExtensions
    {
        public static Task<string> GetUdiAsync(this IDevice o) => o.GetAsync<string>("Udi");
        public static Task<string> GetPathAsync(this IDevice o) => o.GetAsync<string>("Path");
        public static Task<string> GetInterfaceAsync(this IDevice o) => o.GetAsync<string>("Interface");
        public static Task<string> GetIpInterfaceAsync(this IDevice o) => o.GetAsync<string>("IpInterface");
        public static Task<string> GetDriverAsync(this IDevice o) => o.GetAsync<string>("Driver");
        public static Task<string> GetDriverVersionAsync(this IDevice o) => o.GetAsync<string>("DriverVersion");
        public static Task<string> GetFirmwareVersionAsync(this IDevice o) => o.GetAsync<string>("FirmwareVersion");
        public static Task<uint> GetCapabilitiesAsync(this IDevice o) => o.GetAsync<uint>("Capabilities");
        public static Task<uint> GetIp4AddressAsync(this IDevice o) => o.GetAsync<uint>("Ip4Address");
        public static Task<uint> GetStateAsync(this IDevice o) => o.GetAsync<uint>("State");
        public static Task<(uint, uint)> GetStateReasonAsync(this IDevice o) => o.GetAsync<(uint, uint)>("StateReason");
        public static Task<ObjectPath> GetActiveConnectionAsync(this IDevice o) => o.GetAsync<ObjectPath>("ActiveConnection");
        public static Task<ObjectPath> GetIp4ConfigAsync(this IDevice o) => o.GetAsync<ObjectPath>("Ip4Config");
        public static Task<ObjectPath> GetDhcp4ConfigAsync(this IDevice o) => o.GetAsync<ObjectPath>("Dhcp4Config");
        public static Task<ObjectPath> GetIp6ConfigAsync(this IDevice o) => o.GetAsync<ObjectPath>("Ip6Config");
        public static Task<ObjectPath> GetDhcp6ConfigAsync(this IDevice o) => o.GetAsync<ObjectPath>("Dhcp6Config");
        public static Task<bool> GetManagedAsync(this IDevice o) => o.GetAsync<bool>("Managed");
        public static Task<bool> GetAutoconnectAsync(this IDevice o) => o.GetAsync<bool>("Autoconnect");
        public static Task<bool> GetFirmwareMissingAsync(this IDevice o) => o.GetAsync<bool>("FirmwareMissing");
        public static Task<bool> GetNmPluginMissingAsync(this IDevice o) => o.GetAsync<bool>("NmPluginMissing");
        public static Task<uint> GetDeviceTypeAsync(this IDevice o) => o.GetAsync<uint>("DeviceType");
        public static Task<ObjectPath[]> GetAvailableConnectionsAsync(this IDevice o) => o.GetAsync<ObjectPath[]>("AvailableConnections");
        public static Task<string> GetPhysicalPortIdAsync(this IDevice o) => o.GetAsync<string>("PhysicalPortId");
        public static Task<uint> GetMtuAsync(this IDevice o) => o.GetAsync<uint>("Mtu");
        public static Task<uint> GetMeteredAsync(this IDevice o) => o.GetAsync<uint>("Metered");
        public static Task<IDictionary<string, object>[]> GetLldpNeighborsAsync(this IDevice o) => o.GetAsync<IDictionary<string, object>[]>("LldpNeighbors");
        public static Task<bool> GetRealAsync(this IDevice o) => o.GetAsync<bool>("Real");
        public static Task<uint> GetIp4ConnectivityAsync(this IDevice o) => o.GetAsync<uint>("Ip4Connectivity");
        public static Task<uint> GetIp6ConnectivityAsync(this IDevice o) => o.GetAsync<uint>("Ip6Connectivity");
        public static Task<uint> GetInterfaceFlagsAsync(this IDevice o) => o.GetAsync<uint>("InterfaceFlags");
        public static Task<string> GetHwAddressAsync(this IDevice o) => o.GetAsync<string>("HwAddress");
        public static Task<ObjectPath[]> GetPortsAsync(this IDevice o) => o.GetAsync<ObjectPath[]>("Ports");
        public static Task SetManagedAsync(this IDevice o, bool val) => o.SetAsync("Managed", val);
        public static Task SetAutoconnectAsync(this IDevice o, bool val) => o.SetAsync("Autoconnect", val);
    }

    [DBusInterface("org.freedesktop.NetworkManager.Device.Wired")]
    interface IWired : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<WiredProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class WiredProperties
    {
        private string _HwAddress = default(string);
        public string HwAddress
        {
            get
            {
                return _HwAddress;
            }

            set
            {
                _HwAddress = (value);
            }
        }

        private string _PermHwAddress = default(string);
        public string PermHwAddress
        {
            get
            {
                return _PermHwAddress;
            }

            set
            {
                _PermHwAddress = (value);
            }
        }

        private uint _Speed = default(uint);
        public uint Speed
        {
            get
            {
                return _Speed;
            }

            set
            {
                _Speed = (value);
            }
        }

        private string[] _S390Subchannels = default(string[]);
        public string[] S390Subchannels
        {
            get
            {
                return _S390Subchannels;
            }

            set
            {
                _S390Subchannels = (value);
            }
        }

        private bool _Carrier = default(bool);
        public bool Carrier
        {
            get
            {
                return _Carrier;
            }

            set
            {
                _Carrier = (value);
            }
        }
    }

    static class WiredExtensions
    {
        public static Task<string> GetHwAddressAsync(this IWired o) => o.GetAsync<string>("HwAddress");
        public static Task<string> GetPermHwAddressAsync(this IWired o) => o.GetAsync<string>("PermHwAddress");
        public static Task<uint> GetSpeedAsync(this IWired o) => o.GetAsync<uint>("Speed");
        public static Task<string[]> GetS390SubchannelsAsync(this IWired o) => o.GetAsync<string[]>("S390Subchannels");
        public static Task<bool> GetCarrierAsync(this IWired o) => o.GetAsync<bool>("Carrier");
    }

    [DBusInterface("org.freedesktop.NetworkManager.Device.Generic")]
    interface IGeneric : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<GenericProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class GenericProperties
    {
        private string _HwAddress = default(string);
        public string HwAddress
        {
            get
            {
                return _HwAddress;
            }

            set
            {
                _HwAddress = (value);
            }
        }

        private string _TypeDescription = default(string);
        public string TypeDescription
        {
            get
            {
                return _TypeDescription;
            }

            set
            {
                _TypeDescription = (value);
            }
        }
    }

    static class GenericExtensions
    {
        public static Task<string> GetHwAddressAsync(this IGeneric o) => o.GetAsync<string>("HwAddress");
        public static Task<string> GetTypeDescriptionAsync(this IGeneric o) => o.GetAsync<string>("TypeDescription");
    }

    [DBusInterface("org.freedesktop.NetworkManager.Device.WifiP2P")]
    interface IWifiP2P : IDBusObject
    {
        Task StartFindAsync(IDictionary<string, object> Options);
        Task StopFindAsync();
        Task<IDisposable> WatchPeerAddedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchPeerRemovedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<WifiP2PProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class WifiP2PProperties
    {
        private string _HwAddress = default(string);
        public string HwAddress
        {
            get
            {
                return _HwAddress;
            }

            set
            {
                _HwAddress = (value);
            }
        }

        private ObjectPath[] _Peers = default(ObjectPath[]);
        public ObjectPath[] Peers
        {
            get
            {
                return _Peers;
            }

            set
            {
                _Peers = (value);
            }
        }
    }

    static class WifiP2PExtensions
    {
        public static Task<string> GetHwAddressAsync(this IWifiP2P o) => o.GetAsync<string>("HwAddress");
        public static Task<ObjectPath[]> GetPeersAsync(this IWifiP2P o) => o.GetAsync<ObjectPath[]>("Peers");
    }

    [DBusInterface("org.freedesktop.NetworkManager.Device.Wireless")]
    interface IWireless : IDBusObject
    {
        Task<ObjectPath[]> GetAccessPointsAsync();
        Task<ObjectPath[]> GetAllAccessPointsAsync();
        Task RequestScanAsync(IDictionary<string, object> Options);
        Task<IDisposable> WatchAccessPointAddedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchAccessPointRemovedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<WirelessProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class WirelessProperties
    {
        private string _HwAddress = default(string);
        public string HwAddress
        {
            get
            {
                return _HwAddress;
            }

            set
            {
                _HwAddress = (value);
            }
        }

        private string _PermHwAddress = default(string);
        public string PermHwAddress
        {
            get
            {
                return _PermHwAddress;
            }

            set
            {
                _PermHwAddress = (value);
            }
        }

        private uint _Mode = default(uint);
        public uint Mode
        {
            get
            {
                return _Mode;
            }

            set
            {
                _Mode = (value);
            }
        }

        private uint _Bitrate = default(uint);
        public uint Bitrate
        {
            get
            {
                return _Bitrate;
            }

            set
            {
                _Bitrate = (value);
            }
        }

        private ObjectPath[] _AccessPoints = default(ObjectPath[]);
        public ObjectPath[] AccessPoints
        {
            get
            {
                return _AccessPoints;
            }

            set
            {
                _AccessPoints = (value);
            }
        }

        private ObjectPath _ActiveAccessPoint = default(ObjectPath);
        public ObjectPath ActiveAccessPoint
        {
            get
            {
                return _ActiveAccessPoint;
            }

            set
            {
                _ActiveAccessPoint = (value);
            }
        }

        private uint _WirelessCapabilities = default(uint);
        public uint WirelessCapabilities
        {
            get
            {
                return _WirelessCapabilities;
            }

            set
            {
                _WirelessCapabilities = (value);
            }
        }

        private long _LastScan = default(long);
        public long LastScan
        {
            get
            {
                return _LastScan;
            }

            set
            {
                _LastScan = (value);
            }
        }
    }

    static class WirelessExtensions
    {
        public static Task<string> GetHwAddressAsync(this IWireless o) => o.GetAsync<string>("HwAddress");
        public static Task<string> GetPermHwAddressAsync(this IWireless o) => o.GetAsync<string>("PermHwAddress");
        public static Task<uint> GetModeAsync(this IWireless o) => o.GetAsync<uint>("Mode");
        public static Task<uint> GetBitrateAsync(this IWireless o) => o.GetAsync<uint>("Bitrate");
        public static Task<ObjectPath[]> GetAccessPointsAsync(this IWireless o) => o.GetAsync<ObjectPath[]>("AccessPoints");
        public static Task<ObjectPath> GetActiveAccessPointAsync(this IWireless o) => o.GetAsync<ObjectPath>("ActiveAccessPoint");
        public static Task<uint> GetWirelessCapabilitiesAsync(this IWireless o) => o.GetAsync<uint>("WirelessCapabilities");
        public static Task<long> GetLastScanAsync(this IWireless o) => o.GetAsync<long>("LastScan");
    }

    #region AccessPoing

    [DBusInterface("org.freedesktop.NetworkManager.AccessPoint")]
    interface IAccessPoint : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<AccessPointProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class AccessPointProperties
    {
        private uint _Flags = default(uint);
        public uint Flags
        {
            get
            {
                return _Flags;
            }

            set
            {
                _Flags = (value);
            }
        }

        private uint _WpaFlags = default(uint);
        public uint WpaFlags
        {
            get
            {
                return _WpaFlags;
            }

            set
            {
                _WpaFlags = (value);
            }
        }

        private uint _RsnFlags = default(uint);
        public uint RsnFlags
        {
            get
            {
                return _RsnFlags;
            }

            set
            {
                _RsnFlags = (value);
            }
        }

        private byte[] _Ssid = default(byte[]);
        public byte[] Ssid
        {
            get
            {
                return _Ssid;
            }

            set
            {
                _Ssid = (value);
            }
        }

        private uint _Frequency = default(uint);
        public uint Frequency
        {
            get
            {
                return _Frequency;
            }

            set
            {
                _Frequency = (value);
            }
        }

        private string _HwAddress = default(string);
        public string HwAddress
        {
            get
            {
                return _HwAddress;
            }

            set
            {
                _HwAddress = (value);
            }
        }

        private uint _Mode = default(uint);
        public uint Mode
        {
            get
            {
                return _Mode;
            }

            set
            {
                _Mode = (value);
            }
        }

        private uint _MaxBitrate = default(uint);
        public uint MaxBitrate
        {
            get
            {
                return _MaxBitrate;
            }

            set
            {
                _MaxBitrate = (value);
            }
        }

        private byte _Strength = default(byte);
        public byte Strength
        {
            get
            {
                return _Strength;
            }

            set
            {
                _Strength = (value);
            }
        }

        private int _LastSeen = default(int);
        public int LastSeen
        {
            get
            {
                return _LastSeen;
            }

            set
            {
                _LastSeen = (value);
            }
        }
    }

    static class AccessPointExtensions
    {
        public static Task<uint> GetLFlagsAsync(this IAccessPoint o) => o.GetAsync<uint>("Flags");
        public static Task<uint> GetLWpaFlagsAsync(this IAccessPoint o) => o.GetAsync<uint>("WpaFlags");
        public static Task<uint> GetRsnFlagsAsync(this IAccessPoint o) => o.GetAsync<uint>("RsnFlags");
        public static Task<byte[]> GetSsidAsync(this IAccessPoint o) => o.GetAsync<byte[]>("Ssid");
        public static Task<uint> GetFrequencyAsync(this IAccessPoint o) => o.GetAsync<uint>("Frequency");
        public static Task<string> GetHwAddressAsync(this IAccessPoint o) => o.GetAsync<string>("HwAddress");
        public static Task<uint> GetModeAsync(this IAccessPoint o) => o.GetAsync<uint>("Mode");
        public static Task<uint> GetMaxBitrateAsync(this IAccessPoint o) => o.GetAsync<uint>("MaxBitrate");
        public static Task<byte> GetStrengthAsync(this IAccessPoint o) => o.GetAsync<byte>("Strength");
        public static Task<int> GetLastSeenAsync(this IAccessPoint o) => o.GetAsync<int>("LastSeen");
    }

    #endregion


    [DBusInterface("org.freedesktop.NetworkManager.Device.Tun")]
    interface ITun : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<TunProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class TunProperties
    {
        private long _Owner = default(long);
        public long Owner
        {
            get
            {
                return _Owner;
            }

            set
            {
                _Owner = (value);
            }
        }

        private long _Group = default(long);
        public long Group
        {
            get
            {
                return _Group;
            }

            set
            {
                _Group = (value);
            }
        }

        private string _Mode = default(string);
        public string Mode
        {
            get
            {
                return _Mode;
            }

            set
            {
                _Mode = (value);
            }
        }

        private bool _NoPi = default(bool);
        public bool NoPi
        {
            get
            {
                return _NoPi;
            }

            set
            {
                _NoPi = (value);
            }
        }

        private bool _VnetHdr = default(bool);
        public bool VnetHdr
        {
            get
            {
                return _VnetHdr;
            }

            set
            {
                _VnetHdr = (value);
            }
        }

        private bool _MultiQueue = default(bool);
        public bool MultiQueue
        {
            get
            {
                return _MultiQueue;
            }

            set
            {
                _MultiQueue = (value);
            }
        }

        private string _HwAddress = default(string);
        public string HwAddress
        {
            get
            {
                return _HwAddress;
            }

            set
            {
                _HwAddress = (value);
            }
        }
    }

    static class TunExtensions
    {
        public static Task<long> GetOwnerAsync(this ITun o) => o.GetAsync<long>("Owner");
        public static Task<long> GetGroupAsync(this ITun o) => o.GetAsync<long>("Group");
        public static Task<string> GetModeAsync(this ITun o) => o.GetAsync<string>("Mode");
        public static Task<bool> GetNoPiAsync(this ITun o) => o.GetAsync<bool>("NoPi");
        public static Task<bool> GetVnetHdrAsync(this ITun o) => o.GetAsync<bool>("VnetHdr");
        public static Task<bool> GetMultiQueueAsync(this ITun o) => o.GetAsync<bool>("MultiQueue");
        public static Task<string> GetHwAddressAsync(this ITun o) => o.GetAsync<string>("HwAddress");
    }

    [DBusInterface("org.freedesktop.NetworkManager.DHCP4Config")]
    interface IDHCP4Config : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<DHCP4ConfigProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class DHCP4ConfigProperties
    {
        private IDictionary<string, object> _Options = default(IDictionary<string, object>);
        public IDictionary<string, object> Options
        {
            get
            {
                return _Options;
            }

            set
            {
                _Options = (value);
            }
        }
    }

    static class DHCP4ConfigExtensions
    {
        public static Task<IDictionary<string, object>> GetOptionsAsync(this IDHCP4Config o) => o.GetAsync<IDictionary<string, object>>("Options");
    }

    [DBusInterface("org.freedesktop.NetworkManager.DnsManager")]
    interface IDnsManager : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<DnsManagerProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class DnsManagerProperties
    {
        private string _Mode = default(string);
        public string Mode
        {
            get
            {
                return _Mode;
            }

            set
            {
                _Mode = (value);
            }
        }

        private string _RcManager = default(string);
        public string RcManager
        {
            get
            {
                return _RcManager;
            }

            set
            {
                _RcManager = (value);
            }
        }

        private IDictionary<string, object>[] _Configuration = default(IDictionary<string, object>[]);
        public IDictionary<string, object>[] Configuration
        {
            get
            {
                return _Configuration;
            }

            set
            {
                _Configuration = (value);
            }
        }
    }

    static class DnsManagerExtensions
    {
        public static Task<string> GetModeAsync(this IDnsManager o) => o.GetAsync<string>("Mode");
        public static Task<string> GetRcManagerAsync(this IDnsManager o) => o.GetAsync<string>("RcManager");
        public static Task<IDictionary<string, object>[]> GetConfigurationAsync(this IDnsManager o) => o.GetAsync<IDictionary<string, object>[]>("Configuration");
    }

    [DBusInterface("org.freedesktop.NetworkManager.IP6Config")]
    interface IIP6Config : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<IP6ConfigProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class IP6ConfigProperties
    {
        private (byte[], uint, byte[])[] _Addresses = default((byte[], uint, byte[])[]);
        public (byte[], uint, byte[])[] Addresses
        {
            get
            {
                return _Addresses;
            }

            set
            {
                _Addresses = (value);
            }
        }

        private IDictionary<string, object>[] _AddressData = default(IDictionary<string, object>[]);
        public IDictionary<string, object>[] AddressData
        {
            get
            {
                return _AddressData;
            }

            set
            {
                _AddressData = (value);
            }
        }

        private string _Gateway = default(string);
        public string Gateway
        {
            get
            {
                return _Gateway;
            }

            set
            {
                _Gateway = (value);
            }
        }

        private (byte[], uint, byte[], uint)[] _Routes = default((byte[], uint, byte[], uint)[]);
        public (byte[], uint, byte[], uint)[] Routes
        {
            get
            {
                return _Routes;
            }

            set
            {
                _Routes = (value);
            }
        }

        private IDictionary<string, object>[] _RouteData = default(IDictionary<string, object>[]);
        public IDictionary<string, object>[] RouteData
        {
            get
            {
                return _RouteData;
            }

            set
            {
                _RouteData = (value);
            }
        }

        private byte[][] _Nameservers = default(byte[][]);
        public byte[][] Nameservers
        {
            get
            {
                return _Nameservers;
            }

            set
            {
                _Nameservers = (value);
            }
        }

        private string[] _Domains = default(string[]);
        public string[] Domains
        {
            get
            {
                return _Domains;
            }

            set
            {
                _Domains = (value);
            }
        }

        private string[] _Searches = default(string[]);
        public string[] Searches
        {
            get
            {
                return _Searches;
            }

            set
            {
                _Searches = (value);
            }
        }

        private string[] _DnsOptions = default(string[]);
        public string[] DnsOptions
        {
            get
            {
                return _DnsOptions;
            }

            set
            {
                _DnsOptions = (value);
            }
        }

        private int _DnsPriority = default(int);
        public int DnsPriority
        {
            get
            {
                return _DnsPriority;
            }

            set
            {
                _DnsPriority = (value);
            }
        }
    }

    static class IP6ConfigExtensions
    {
        public static Task<(byte[], uint, byte[])[]> GetAddressesAsync(this IIP6Config o) => o.GetAsync<(byte[], uint, byte[])[]>("Addresses");
        public static Task<IDictionary<string, object>[]> GetAddressDataAsync(this IIP6Config o) => o.GetAsync<IDictionary<string, object>[]>("AddressData");
        public static Task<string> GetGatewayAsync(this IIP6Config o) => o.GetAsync<string>("Gateway");
        public static Task<(byte[], uint, byte[], uint)[]> GetRoutesAsync(this IIP6Config o) => o.GetAsync<(byte[], uint, byte[], uint)[]>("Routes");
        public static Task<IDictionary<string, object>[]> GetRouteDataAsync(this IIP6Config o) => o.GetAsync<IDictionary<string, object>[]>("RouteData");
        public static Task<byte[][]> GetNameserversAsync(this IIP6Config o) => o.GetAsync<byte[][]>("Nameservers");
        public static Task<string[]> GetDomainsAsync(this IIP6Config o) => o.GetAsync<string[]>("Domains");
        public static Task<string[]> GetSearchesAsync(this IIP6Config o) => o.GetAsync<string[]>("Searches");
        public static Task<string[]> GetDnsOptionsAsync(this IIP6Config o) => o.GetAsync<string[]>("DnsOptions");
        public static Task<int> GetDnsPriorityAsync(this IIP6Config o) => o.GetAsync<int>("DnsPriority");
    }

    [DBusInterface("org.freedesktop.NetworkManager.Settings")]
    interface ISettings : IDBusObject
    {
        Task<ObjectPath[]> ListConnectionsAsync();
        Task<ObjectPath> GetConnectionByUuidAsync(string Uuid);
        Task<ObjectPath> AddConnectionAsync(IDictionary<string, IDictionary<string, object>> Connection);
        Task<ObjectPath> AddConnectionUnsavedAsync(IDictionary<string, IDictionary<string, object>> Connection);
        Task<(ObjectPath path, IDictionary<string, object> result)> AddConnection2Async(IDictionary<string, IDictionary<string, object>> Settings, uint Flags, IDictionary<string, object> Args);
        Task<(bool status, string[] failures)> LoadConnectionsAsync(string[] Filenames);
        Task<bool> ReloadConnectionsAsync();
        Task SaveHostnameAsync(string Hostname);
        Task<IDisposable> WatchNewConnectionAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchConnectionRemovedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<SettingsProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class SettingsProperties
    {
        private ObjectPath[] _Connections = default(ObjectPath[]);
        public ObjectPath[] Connections
        {
            get
            {
                return _Connections;
            }

            set
            {
                _Connections = (value);
            }
        }

        private string _Hostname = default(string);
        public string Hostname
        {
            get
            {
                return _Hostname;
            }

            set
            {
                _Hostname = (value);
            }
        }

        private bool _CanModify = default(bool);
        public bool CanModify
        {
            get
            {
                return _CanModify;
            }

            set
            {
                _CanModify = (value);
            }
        }
    }

    static class SettingsExtensions
    {
        public static Task<ObjectPath[]> GetConnectionsAsync(this ISettings o) => o.GetAsync<ObjectPath[]>("Connections");
        public static Task<string> GetHostnameAsync(this ISettings o) => o.GetAsync<string>("Hostname");
        public static Task<bool> GetCanModifyAsync(this ISettings o) => o.GetAsync<bool>("CanModify");
    }

    [DBusInterface("org.freedesktop.NetworkManager.Settings.Connection")]
    interface IConnection : IDBusObject
    {
        Task UpdateAsync(IDictionary<string, IDictionary<string, object>> Properties);
        Task UpdateUnsavedAsync(IDictionary<string, IDictionary<string, object>> Properties);
        Task DeleteAsync();
        Task<IDictionary<string, IDictionary<string, object>>> GetSettingsAsync();
        Task<IDictionary<string, IDictionary<string, object>>> GetSecretsAsync(string SettingName);
        Task ClearSecretsAsync();
        Task SaveAsync();
        Task<IDictionary<string, object>> Update2Async(IDictionary<string, IDictionary<string, object>> Settings, uint Flags, IDictionary<string, object> Args);
        Task<IDisposable> WatchUpdatedAsync(Action handler, Action<Exception> onError = null);
        Task<IDisposable> WatchRemovedAsync(Action handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<ConnectionProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class ConnectionProperties
    {
        private bool _Unsaved = default(bool);
        public bool Unsaved
        {
            get
            {
                return _Unsaved;
            }

            set
            {
                _Unsaved = (value);
            }
        }

        private uint _Flags = default(uint);
        public uint Flags
        {
            get
            {
                return _Flags;
            }

            set
            {
                _Flags = (value);
            }
        }

        private string _Filename = default(string);
        public string Filename
        {
            get
            {
                return _Filename;
            }

            set
            {
                _Filename = (value);
            }
        }
    }

    static class ConnectionExtensions
    {
        public static Task<bool> GetUnsavedAsync(this IConnection o) => o.GetAsync<bool>("Unsaved");
        public static Task<uint> GetFlagsAsync(this IConnection o) => o.GetAsync<uint>("Flags");
        public static Task<string> GetFilenameAsync(this IConnection o) => o.GetAsync<string>("Filename");
    }

    #endregion
}