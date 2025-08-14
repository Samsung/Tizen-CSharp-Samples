interface AccountManagerObject {
	/** 
 Object representing an account manager.
             */
	account: AccountManager;
}

interface AccountProvider {
	/** 
 Identifier of the account provider application.
             */
	applicationId: ApplicationId;
	/** 
 Logical (translatable) display name.
             */
	displayName: DOMString;
	/** 
 Path to the icon representing the account provider.
             */
	iconUri: DOMString;
	/** 
 Path to the small icon representing the account provider.
             */
	smallIconUri: DOMString;
	/** 
 Capabilities of the account provider defined in IRI format.
             */
	capabilities: DOMString[];
	/** 
 Boolean value that indicates whether multiple accounts are supported.
             */
	isMultipleAccountSupported: boolean;
}

interface Account {
	/** 
 Identifier for the account.
By default, this attribute is set to null.
             */
	id: AccountId;
	/** 
 Account user name.
By default, this attribute is set to null.
             */
	userName: DOMString;
	/** 
 Name, identifier or URI of the icon.
By default, this attribute is set to null.
             */
	iconUri: DOMString;
	/** 
 Reference to the provider.
There is one provider for each account.
A given provider can be referenced from many accounts.
             */
	provider: AccountProvider;
	/** 
 Adds the specified key and value to the extended data.
             */
	setExtendedData(key: DOMString,value: DOMString): void
	/** 
 Gets the value for the given key from the extended data.
Returns null if the given key is not found.
             */
	getExtendedData(key: DOMString): void
	/** 
 Gets the extended data for the account as an array, asynchronously.
Returns an empty array if there is no extended data.
             */
	getExtendedData(successCallback: AccountExtendedDataArraySuccessCallback,errorCallback: ErrorCallback): void
}

interface AccountManager {
	/** 
 Adds an account to the account database.
             */
	add(account: Account): void
	/** 
 Removes an account from the account database.
             */
	remove(accountId: AccountId): void
	/** 
 Updates an account.
             */
	update(account: Account): void
	/** 
 Gets the Account object with the given identifier.
             */
	getAccount(accountId: AccountId): Account
	/** 
 Gets the accounts associated with the provider that has a specified applicationId, asynchronously.
If you want to get all accounts, omit the applicationId argument.
             */
	getAccounts(successCallback: AccountArraySuccessCallback,errorCallback: ErrorCallback,applicationId: DOMString): void
	/** 
 Gets the account provider with the given application identifier.
             */
	getProvider(applicationId: ApplicationId): AccountProvider
	/** 
 Gets the providers with the given capabilities, asynchronously.
If you want to get all the providers, omit the capability argument.
             */
	getProviders(successCallback: AccountProviderArraySuccessCallback,errorCallback: ErrorCallback,capability: DOMString): void
	/** 
 Adds an account listener for watching changes to accounts.
             */
	addAccountListener(callback: AccountChangeCallback): void
	/** 
 Removes the previously registered listener.
             */
	removeAccountListener(accountListenerId: long): void
}

interface AccountExtendedData {
	/** 
 Name (key) of the account extended data item.
             */
	key: DOMString;
	/** 
 Value of the account extended data item.
             */
	value: DOMString;
}

interface AccountArraySuccessCallback {
	/** 
 Called when information of accounts is ready.
             */
	onsuccess(accounts: Account[]): void
}

interface AccountExtendedDataArraySuccessCallback {
	/** 
 Called when information of extended data is ready.
             */
	onsuccess(extendedDataArray: AccountExtendedData[]): void
}

interface AccountProviderArraySuccessCallback {
	/** 
 Called when information of providers are ready.
             */
	onsuccess(providers: AccountProvider[]): void
}

interface AccountChangeCallback {
	/** 
 Called when an account is added.
             */
	onadded(account: Account): void
	/** 
 Called when an account is removed.
             */
	onremoved(accountId: AccountId): void
	/** 
 Called when an account is updated.
             */
	onupdated(account: Account): void
}

interface AlarmManagerObject {
	/** 
 Object representing an alarm manager.
             */
	alarm: AlarmManager;
}

interface AlarmManager {
	/** 
 Adds an alarm to the storage.
             */
	add(alarm: Alarm,applicationId: ApplicationId,appControl: ApplicationControl): void
	/** 
 Adds an alarm notification to the storage.
             */
	addAlarmNotification(alarm: Alarm,notification: Notification): void
	/** 
 Removes an alarm from the storage.
             */
	remove(id: AlarmId): void
	/** 
 Removes all alarms added by an application.
             */
	removeAll(): void
	/** 
 Returns an alarm as per the specified identifier.
             */
	get(id: AlarmId): Alarm
	/** 
 Gets the notification to be posted when an alarm is triggered. Returned  contains exactly the same data like passed to method .
             */
	getAlarmNotification(id: AlarmId): UserNotification
	/** 
 Retrieves all alarms in an application storage.
             */
	getAll(): void
}

interface Alarm {
	/** 
 The alarm identifier.
             */
	id: AlarmId;
}

interface AlarmRelative {
	/** 
 An attribute to store the difference in time (in seconds) between when an alarm is added and when it is triggered.
             */
	delay: long;
	/** 
 An attribute to store the duration in seconds between each trigger of an alarm.
By default, this attribute is set to , indicating that this alarm does not repeat.
             */
	period: long;
	/** 
 Returns the duration in seconds before the next alarm is triggered.
             */
	getRemainingSeconds(): void
}

interface AlarmAbsolute {
	/** 
 An attribute to store the absolute date/time when the alarm is initially triggered.
             */
	date: Date;
	/** 
 An attribute to store the duration in seconds between each trigger of the alarm.
             */
	period: long;
	/** 
 An attribute to store the days of the week associated with the recurrence rule.
             */
	daysOfTheWeek: ByDayValue[];
	/** 
 Returns the date / time of the next alarm trigger.
             */
	getNextScheduledDate(): void
}

interface ApplicationManagerObject {
	/** 
 Object representing a account manager.
             */
	application: ApplicationManager;
}

interface ApplicationManager {
	/** 
 Gets the  object defining the current application.
             */
	getCurrentApplication(): Application
	/** 
 Kills an application with the specified application context ID.
             */
	kill(contextId: ApplicationContextId,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Launches an application with the given application ID.
             */
	launch(id: ApplicationId,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Launches an application with the specified application control.
             */
	launchAppControl(appControl: ApplicationControl,id: ApplicationId,successCallback: SuccessCallback,errorCallback: ErrorCallback,replyCallback: ApplicationControlDataArrayReplyCallback): void
	/** 
 Finds which applications can be launched with the given application control.
             */
	findAppControl(appControl: ApplicationControl,successCallback: FindAppControlSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets a list of application contexts for applications that are currently running on a device.
The information contained for each application corresponds to the application state at the time when the list had been generated.
             */
	getAppsContext(successCallback: ApplicationContextArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets the application context for the specified application context ID.
If the ID is set to  or is not set at all, the method returns the application context of the current application.
The list of running applications and their application IDs is obtained with .
             */
	getAppContext(contextId: ApplicationContextId): ApplicationContext
	/** 
 Gets the list of installed applications' information on a device.
The information contained on each application corresponds to the application state at the time when the list had been generated.
             */
	getAppsInfo(successCallback: ApplicationInformationArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets application information for a specified application ID.
             */
	getAppInfo(id: ApplicationId): ApplicationInformation
	/** 
 Gets application certificates for a specified application ID.
             */
	getAppCerts(id: ApplicationId): void
	/** 
 Gets the URI of the read-only shared directory of an application for a specified application ID.
             */
	getAppSharedURI(id: ApplicationId): void
	/** 
 Gets the application meta data array for a specified application ID.
             */
	getAppMetaData(id: ApplicationId): void
	/** 
 Gets information about battery usage per application.
             */
	getBatteryUsageInfo(successCallback: BatteryUsageInfoArraySuccessCallback,errorCallback: ErrorCallback,days: long,limit: long): void
	/** 
 Gets the usage statistics of applications.
             */
	getAppsUsageInfo(successCallback: AppsUsageInfoArraySuccessCallback,errorCallback: ErrorCallback,mode: ApplicationUsageMode,filter: ApplicationUsageFilter,limit: long): void
	/** 
 Adds a listener for receiving any notification for changes in the list of installed applications
on a device.
             */
	addAppInfoEventListener(eventCallback: ApplicationInformationEventCallback): void
	/** 
 Removes the listener to stop receiving notifications for changes on the list of installed applications on a device.
             */
	removeAppInfoEventListener(watchId: long): void
	/** 
 Adds a listener for receiving any notification for status changes of the installed applications on a device.
             */
	addAppStatusChangeListener(eventCallback: StatusEventCallback,appId: ApplicationId): void
	/** 
 Removes the listener to stop receiving notifications for status changes of the installed applications on a device.
             */
	removeAppStatusChangeListener(watchId: long): void
}

interface Application {
	/** 
 An attribute to store the application information for the current application.
             */
	appInfo: ApplicationInformation;
	/** 
 An attribute to store the ID of a running application.
             */
	contextId: ApplicationContextId;
	/** 
 Exits the current application.
             */
	exit(): void
	/** 
 Hides the current application.
             */
	hide(): void
	/** 
 Gets the requested application control passed to the current application.
             */
	getRequestedAppControl(): RequestedApplicationControl
	/** 
 Adds a listener which will invoke a callback function when an event occurs.
             */
	addEventListener(event: EventInfo,callback: EventCallback): void
	/** 
 Removes an event listener with a specified listener identifier.
             */
	removeEventListener(watchId: long): void
	/** 
 Broadcasts a user defined event to all the listeners which are listening for this event.
             */
	broadcastEvent(event: EventInfo,data: UserEventData): void
	/** 
 Broadcasts a user defined event to all the trusted listeners which are listening for this event. Applications which have the same certificate as the sending application can receive the event.
             */
	broadcastTrustedEvent(event: EventInfo,data: UserEventData): void
}

interface ApplicationInformation {
	/** 
 An attribute to store the identifier of an application for application management.
             */
	id: ApplicationId;
	/** 
 An attribute to store the name of an application.
             */
	name: DOMString;
	/** 
 An attribute to store the icon path of an application.
             */
	iconPath: DOMString;
	/** 
 An attribute to store the version of an application.
             */
	version: DOMString;
	/** 
 An attribute that determines whether the application information should
be shown (such as in menus).
             */
	show: boolean;
	/** 
 An array of attributes to store the categories that the app belongs to.
             */
	categories: DOMString[];
	/** 
 An attribute to store the application install/update time.
             */
	installDate: Date;
	/** 
 An attribute to store the application size (installed space).
             */
	size: long;
	/** 
 An attribute to store the package ID of an application.
             */
	packageId: PackageId;
}

interface ApplicationContext {
	/** 
 An attribute to store the ID of a running application.
             */
	id: ApplicationContextId;
	/** 
 An attribute to store the ID of an installed application.
             */
	appId: ApplicationId;
}

interface ApplicationBatteryUsage {
	/** 
 An attribute storing ID of an application.
             */
	appId: ApplicationId;
	/** 
 An attribute which stores information about battery usage of an application with ApplicationId .
Battery usage is a ratio of battery consumption of the application with ApplicationId  to the total battery consumption of all applications.
The attribute value scales from  to , the higher value, the more battery is consumed.
             */
	batteryUsage: double;
}

interface ApplicationUsage {
	/** 
 An attribute to store the ID of an application.
             */
	appId: ApplicationId;
	/** 
 An attribute to store the total number of times the application was in the foreground.
             */
	totalCount: number;
	/** 
 An attribute to store the total time of application usage in seconds.
             */
	totalDuration: number;
	/** 
 An attribute to store the last time when the application was used.
             */
	lastTime: Date;
}

interface ApplicationControlData {
	/** 
 An attribute to store the name of a key.
             */
	key: DOMString;
	/** 
 An attribute to store the value associated with a key.
             */
	value: DOMString[];
}

interface ApplicationControl {
	/** 
 An attribute to store the string that defines the action to be
performed by an application control.
             */
	operation: DOMString;
	/** 
 An attribute to store the URI needed by an application control.
             */
	uri: DOMString;
	/** 
 An attribute to store the MIME type of content.
             */
	mime: DOMString;
	/** 
 An attribute to store the category of the application to be launched.
             */
	category: DOMString;
	/** 
 An array of attributes to store the data needed for an application control.
             */
	data: ApplicationControlData[];
	/** 
 An attribute to specify launch mode. Default application launch mode is .
             */
	launchMode: ApplicationControlLaunchMode;
}

interface RequestedApplicationControl {
	/** 
 An attribute to store the application control object that describes the caller application's request.
It contains the information that the calling application passed to .
             */
	appControl: ApplicationControl;
	/** 
 An attribute to store the caller application's ID.
             */
	callerAppId: ApplicationId;
	/** 
 Sends the results to the caller application.
             */
	replyResult(data: ApplicationControlData[]): void
	/** 
 Notifies the calling application that the application failed
to perform the requested action.
             */
	replyFailure(): void
}

interface ApplicationCertificate {
	/** 
 An attribute to store the type of the application certificate
             */
	type: DOMString;
	/** 
 An attribute to store the value of the application certificate
             */
	value: DOMString;
}

interface ApplicationMetaData {
	/** 
 An attribute to store the key of the application meta data
             */
	key: DOMString;
	/** 
 An attribute to store the value of the application meta data
             */
	value: DOMString;
}

interface BatteryUsageInfoArraySuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(batteryInfoArray: ApplicationBatteryUsage[]): void
}

interface AppsUsageInfoArraySuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(appsInfoArray: ApplicationUsage[]): void
}

interface ApplicationInformationArraySuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(informationArray: ApplicationInformation[]): void
}

interface FindAppControlSuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(informationArray: ApplicationInformation[],appControl: ApplicationControl): void
}

interface ApplicationContextArraySuccessCallback {
	/** 
 Called when  completes successfully.
             */
	onsuccess(contexts: ApplicationContext[]): void
}

interface ApplicationControlDataArrayReplyCallback {
	/** 
 Called when the callee application calls .
             */
	onsuccess(data: ApplicationControlData[]): void
	/** 
 Called when the callee application calls .
             */
	onfailure(): void
}

interface SystemEventData {
	/** 
 Value of the system event data item.
             */
	value: DOMString;
	/** 
 Type of the system event data item.
             */
	type: DOMString;
}

interface EventCallback {
	/** 
 Called when the event occurs.
             */
	onevent(event: EventInfo,data: EventData): void
}

interface StatusEventCallback {
	/** 
 Called when the status event occurs.
             */
	onchange(appId: ApplicationId,isActive: boolean): void
}

interface ArchiveManagerObject {
	/** 
 Object representing an archive manager.
             */
	archive: ArchiveManager;
}

interface ArchiveManager {
	/** 
 Opens the archive file. After this operation, it is possible to add or get files to and from the archive.
             */
	open(file: FileReference,mode: FileMode,onsuccess: ArchiveFileSuccessCallback,onerror: ErrorCallback,options: ArchiveFileOptions): void
	/** 
 Cancels an operation with the given identifier.
             */
	abort(operationIdentifier: long): void
}

interface ArchiveFile {
	/** 
 Describes the mode the file is opened with.
             */
	mode: FileMode;
	/** 
 Size of all the files included in the archive after decompression.
             */
	decompressedSize: number;
	/** 
 Adds a new member file to .
             */
	add(sourceFile: FileReference,onsuccess: SuccessCallback,onerror: ErrorCallback,onprogress: ArchiveFileProgressCallback,options: ArchiveFileEntryOptions): void
	/** 
 Extracts every file from this  to a given directory.
             */
	extractAll(destinationDirectory: FileReference,onsuccess: SuccessCallback,onerror: ErrorCallback,onprogress: ArchiveFileProgressCallback,overwrite: boolean): void
	/** 
 Retrieves information about the member files in .
             */
	getEntries(onsuccess: ArchiveFileEntryArraySuccessCallback,onerror: ErrorCallback): void
	/** 
 Retrieves information about  with the specified name in .
             */
	getEntryByName(name: DOMString,onsuccess: ArchiveFileEntrySuccessCallback,onerror: ErrorCallback): void
	/** 
 Closes the .
             */
	close(): void
}

interface ArchiveFileEntry {
	/** 
 Path identifying the member file of ArchiveFile. This is a full path with the directory and base name of the entry.
             */
	name: DOMString;
	/** 
 Original size of the member file [bytes].
             */
	size: number;
	/** 
 Amount of storage space used by the member file, which may be compressed, in ArchiveFile [bytes].
             */
	compressedSize: number;
	/** 
 Date and time stored with the member file.
This is usually the modification date of the file.
             */
	modified: Date;
	/** 
 Extracts ArchiveFileEntry to the given location.
             */
	extract(destinationDirectory: FileReference,onsuccess: SuccessCallback,onerror: ErrorCallback,onprogress: ArchiveFileProgressCallback,stripName: boolean,overwrite: boolean): void
}

interface ArchiveFileSuccessCallback {
	/** 
 Called when the archive file with the given name is ready to use.
             */
	onsuccess(archive: ArchiveFile): void
}

interface ArchiveFileEntrySuccessCallback {
	/** 
 Called when the file with the given name through getEntryByName() is found successfully.
             */
	onsuccess(entry: ArchiveFileEntry): void
}

interface ArchiveFileEntryArraySuccessCallback {
	/** 
 Called when all file entries in the archive file are retrieved successfully.
             */
	onsuccess(entries: ArchiveFileEntry[]): void
}

interface ArchiveFileProgressCallback {
	/** 
 Called to signal compressing or extracting operation progress.
             */
	onprogress(operationIdentifier: long,value: double,filename: DOMString): void
}

interface BadgeManagerObject {
	/** 
 Object representing a badge manager object.
             */
	badge: BadgeManager;
}

interface BadgeManager {
	/** 
 Maximum length of a badge number.
             */
	maxBadgeCount: long;
	/** 
 Sets the badge count for the designated application. Only applications with the same author signature can have their badge count modified.
             */
	setBadgeCount(appId: ApplicationId,count: long): void
	/** 
 Gets the badge count for the designated application.
             */
	getBadgeCount(appId: ApplicationId): void
	/** 
 Adds a listener to receive a notification when the badge number for the designated application changes.
             */
	addChangeListener(appIdList: ApplicationId[],successCallback: BadgeChangeCallback): void
	/** 
 Unsubscribes from receiving notifications for badge number changes.
             */
	removeChangeListener(appIdList: ApplicationId[]): void
}

interface BadgeChangeCallback {
	/** 
 Called when the badge number of a specified application is updated.
             */
	onsuccess(appId: ApplicationId,count: long): void
}

interface BluetoothManagerObject {
	/** 
 Object representing a bluetooth manager.
             */
	bluetooth: BluetoothManager;
}

interface BluetoothLEServiceData {
	/** 
 The UUID of service data.
             */
	uuid: BluetoothUUID;
	/** 
 The service data of the Bluetooth LE device.
             */
	data: DOMString;
}

interface BluetoothLEManufacturerData {
	/** 
 The manufacturer assigned ID
             */
	id: DOMString;
	/** 
 The manufacturer data content
             */
	data: DOMString;
}

interface BluetoothLEAdvertiseData {
	/** 
 The flag indicating whether the device name should be included in advertise or scan response data.
If attribute is set to null, The default value is set to a false.
             */
	includeName: boolean;
	/** 
 The service UUIDs for advertise or scan response data.
             */
	uuids: BluetoothUUID[];
	/** 
 The service solicitation UUIDs for advertise or scan response data.
             */
	solicitationuuids: BluetoothLESolicitationUUID[];
	/** 
 The external appearance of this device for advertise or scan response data.
             */
	appearance: number;
	/** 
 The transmission power level should be included in advertise or scan response data.
If attribute is set to null, The default value is set to a false.
             */
	includeTxPowerLevel: boolean;
	/** 
 The array of objects representing service data for advertise.
             */
	servicesData: BluetoothLEServiceData[];
	/** 
 The service data for advertise or scan response data.
             */
	serviceData: BluetoothLEServiceData;
	/** 
 The manufacturer specific data for advertise or scan response data.
             */
	manufacturerData: BluetoothLEManufacturerData;
}

interface BluetoothManager {
	/** 
 The major device class identifier of Bluetooth class of device (CoD).
             */
	deviceMajor: BluetoothClassDeviceMajor;
	/** 
 The minor device class identifier of Bluetooth class of device (CoD).
             */
	deviceMinor: BluetoothClassDeviceMinor;
	/** 
 The major service class identifier of Bluetooth class of device (CoD).
             */
	deviceService: BluetoothClassDeviceService;
	/** 
 Gets the default local Bluetooth adapter.
             */
	getDefaultAdapter(): BluetoothAdapter
	/** 
 Gets the default Low Energy Bluetooth adapter.
             */
	getLEAdapter(): BluetoothLEAdapter
	/** 
 Gets the BluetoothGATTServer object, which allows starting, stopping the local GATT server, and configuring its services.
             */
	getGATTServer(): BluetoothGATTServer
	/** 
 Converts given data to byte array.
             */
	toByteArray(data: Bytes): void
	/** 
 Converts given data to DOMString.
             */
	toDOMString(data: Bytes): void
	/** 
 Converts given data to Uint8Array.
             */
	toUint8Array(data: Bytes): Uint8Array
	/** 
 Converts given UUID to 128 bit representation.
             */
	uuidTo128bit(uuid: BluetoothUUID): BluetoothUUID
	/** 
 Converts given UUID to the shortest format, in which it can be expressed.
             */
	uuidToShortestPossible(uuid: BluetoothUUID): BluetoothUUID
	/** 
 Checks if given parameters are representations of the same UUID.
             */
	uuidsEqual(uuid1: BluetoothUUID,uuid2: BluetoothUUID): void
}

interface BluetoothAdapter {
	/** 
 The readable name of the Bluetooth adapter.
             */
	name: DOMString;
	/** 
 The unique hardware address of the Bluetooth adapter, also known as the MAC address.
             */
	address: BluetoothAddress;
	/** 
 The current state of the Bluetooth adapter.
             */
	powered: boolean;
	/** 
 The current visibility state of the Bluetooth adapter, that is, whether the local device is discoverable by remote devices.
             */
	visible: boolean;
	/** 
 Sets the local Bluetooth adapter name.
             */
	setName(name: DOMString,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Sets the state of a Bluetooth adapter to on or off by sending a request to Bluetooth hardware to change the power state.
For most Bluetooth actions, the Bluetooth adapter must be powered on.
             */
	setPowered(state: boolean,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Sets the local device visibility by sending a request to a Bluetooth hardware to change the device visible state to .
             */
	setVisible(mode: boolean,successCallback: SuccessCallback,errorCallback: ErrorCallback,timeout: number): void
	/** 
 Sets the listener to receive notifications about changes of Bluetooth adapter.
             */
	setChangeListener(listener: BluetoothAdapterChangeCallback): void
	/** 
 Unsets the listener, so stop receiving notifications about changes of Bluetooth adapter.
             */
	unsetChangeListener(): void
	/** 
 Discovers nearby Bluetooth devices if any, that is, devices within proximity to the local device.
             */
	discoverDevices(successCallback: BluetoothDiscoverDevicesSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Stops an active device discovery session.
             */
	stopDiscovery(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets all the known devices that have information stored in the local Bluetooth adapter.
             */
	getKnownDevices(successCallback: BluetoothDeviceArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets the  object for a given device hardware address.
             */
	getDevice(address: BluetoothAddress,successCallback: BluetoothDeviceSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Creates a bond with a remote device by initiating the bonding process with peer device, using the given MAC address. The remote device must be bonded with the local device in order to connect to services of the remote device and then exchange data with each other.
             */
	createBonding(address: BluetoothAddress,successCallback: BluetoothDeviceSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Destroys the bond with a remote device.
             */
	destroyBonding(address: BluetoothAddress,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Registers a service record in the device service record database with the specified , .
             */
	registerRFCOMMServiceByUUID(uuid: BluetoothUUID,name: DOMString,successCallback: BluetoothServiceSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets the profile handler for the given type.
             */
	getBluetoothProfileHandler(profileType: BluetoothProfileType): BluetoothProfileHandler
}

interface BluetoothLEAdapter {
	/** 
 Starts scanning for Low Energy advertisement.
             */
	startScan(successCallback: BluetoothLEScanCallback,errorCallback: ErrorCallback): void
	/** 
 Stops scanning for Low Energy advertisement.
             */
	stopScan(): void
	/** 
 Checks if scanning for Bluetooth Low Energy devices is currently in progress.
             */
	isScanning(): void
	/** 
 Starts advertising for Low Energy Devices.
             */
	startAdvertise(advertiseData: BluetoothLEAdvertiseData,packetType: BluetoothAdvertisePacketType,successCallback: BluetoothLEAdvertiseCallback,errorCallback: ErrorCallback,mode: BluetoothAdvertisingMode,connectable: boolean): void
	/** 
 Stops advertising for Low Energy Devices.
             */
	stopAdvertise(): void
	/** 
 Registers a listener that is called whenever a GATT connection with another device is established or terminated.
             */
	addConnectStateChangeListener(listener: BluetoothLEConnectChangeCallback): void
	/** 
 Unregisters a listener called whenever a GATT connection with another device is established or terminated.
             */
	removeConnectStateChangeListener(watchID: long): void
}

interface BluetoothGATTService {
	/** 
 UUID of the service.
             */
	uuid: BluetoothUUID;
	/** 
 UUID of the service.
             */
	serviceUuid: BluetoothUUID;
	/** 
 A list of services included in this service.
             */
	services: BluetoothGATTServiceVariant[];
	/** 
 A list of characteristics in this service.
             */
	characteristics: BluetoothGATTCharacteristicVariant[];
}

interface BluetoothGATTServerService {
	/** 
 Flag indicating whether the service is primary or secondary.
             */
	isPrimary: boolean;
	/** 
 Unregisters the service and all its characteristics from the local GATT server.
             */
	unregister(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
}

interface BluetoothGATTCharacteristic {
	/** 
 A list of descriptors in this characteristic.
             */
	descriptors: BluetoothGATTDescriptorVariant[];
	/** 
 Indicates if the characteristic is broadcastable.
             */
	isBroadcast: boolean;
	/** 
 Indicates if the characteristic has extended properties.
             */
	hasExtendedProperties: boolean;
	/** 
 Indicates if the characteristic supports notification.
             */
	isNotify: boolean;
	/** 
 Indicates if the characteristic supports indication.
             */
	isIndication: boolean;
	/** 
 Indicates if the characteristic is readable.
             */
	isReadable: boolean;
	/** 
 Indicates if the characteristic supports write with the signature.
             */
	isSignedWrite: boolean;
	/** 
 Indicates if the characteristic is writable.
             */
	isWritable: boolean;
	/** 
 Indicates if the characteristic supports writing without response.
             */
	isWriteNoResponse: boolean;
	/** 
 UUID of the characteristic.
             */
	uuid: BluetoothUUID;
	/** 
 Reads the characteristic value from the remote device. Updates characteristic value attribute.
             */
	readValue(successCallback: ReadValueSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Writes the characteristic value to the remote device.
             */
	writeValue(value: Bytes,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Registers a callback to be called when characteristic value of the characteristic changes.
             */
	addValueChangeListener(callback: ReadValueSuccessCallback): void
	/** 
 Unregisters a characteristic value change listener.
             */
	removeValueChangeListener(watchID: long): void
}

interface BluetoothGATTServerCharacteristic {
	/** 
 Indicates if clients have the permission to read the value of the characteristic.
             */
	readPermission: boolean;
	/** 
 Indicates if clients have the permission to write the value of the characteristic.
             */
	writePermission: boolean;
	/** 
 Indicates if clients have the permission to read the value of the characteristic through encrypted connections.
             */
	encryptedReadPermission: boolean;
	/** 
 Indicates if clients have the permission to write the value of the characteristic through encrypted connections.
             */
	encryptedWritePermission: boolean;
	/** 
 Indicates if clients have the permission to perform signed reads of the characteristic's value.
             */
	encryptedSignedReadPermission: boolean;
	/** 
 Indicates if clients have the permission to perform signed writes of the characteristic's value.
             */
	encryptedSignedWritePermission: boolean;
	/** 
 Notifies the clients of the local GATT server of the changes in the characteristic.
             */
	notifyAboutValueChange(value: Bytes,clientAddress: BluetoothAddress,notificationCallback: NotificationCallback,errorCallback: ErrorCallback): void
	/** 
 Registers the callback called when a client reads the value of the characteristic from the local GATT server.
             */
	setReadValueRequestCallback(readValueRequestCallback: ReadValueRequestCallback,successCallback: SuccessCallback,errorCallback: ErrorCallback,sendResponseSuccessCallback: SuccessCallback,sendResponseErrorCallback: ErrorCallback): void
	/** 
 Registers the callback called when a client writes the value of the characteristic of the local GATT server.
             */
	setWriteValueRequestCallback(writeValueRequestCallback: WriteValueRequestCallback,successCallback: SuccessCallback,errorCallback: ErrorCallback,sendResponseSuccessCallback: SuccessCallback,sendResponseErrorCallback: ErrorCallback): void
}

interface BluetoothGATTDescriptor {
	/** 
 UUID of the descriptor.
             */
	uuid: BluetoothUUID;
	/** 
 Reads descriptor value from remote device. Updates descriptor value attribute.
             */
	readValue(successCallback: ReadValueSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Writes the descriptor value to the remote device.
             */
	writeValue(value: Bytes,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
}

interface BluetoothGATTServerDescriptor {
	/** 
 Indicates if clients have the permission to read the value of the descriptor.
             */
	readPermission: boolean;
	/** 
 Indicates if clients have the permission to write the value of the descriptor.
             */
	writePermission: boolean;
	/** 
 Indicates if clients have the permission to read the value of the descriptor through encrypted connections.
             */
	encryptedReadPermission: boolean;
	/** 
 Indicates if clients have the permission to write the value of the descriptor through encrypted connections.
             */
	encryptedWritePermission: boolean;
	/** 
 Indicates if clients have the permission to perform signed reads of the charactersitic's value.
             */
	encryptedSignedReadPermission: boolean;
	/** 
 Indicates if clients have the permission to perform signed writes of the charactersitic's value.
             */
	encryptedSignedWritePermission: boolean;
	/** 
 Registers the callback called when a client reads the value of the descriptor from the local GATT server.
             */
	setReadValueRequestCallback(readValueRequestCallback: ReadValueRequestCallback,successCallback: SuccessCallback,errorCallback: ErrorCallback,sendResponseSuccessCallback: SuccessCallback,sendResponseErrorCallback: ErrorCallback): void
	/** 
 Registers the callback called when a remote client writes the value of the descriptor from the local GATT server.
             */
	setWriteValueRequestCallback(writeValueRequestCallback: WriteValueRequestCallback,successCallback: SuccessCallback,errorCallback: ErrorCallback,sendResponseSuccessCallback: SuccessCallback,sendResponseErrorCallback: ErrorCallback): void
}

interface BluetoothGATTServer {
	/** 
 The flag indicating if remote GATT clients can currently connect to the server, exposing services defined in . It is toggled on  and  calls.
             */
	isRunning: boolean;
	/** 
 The list of GATT services hosted on this server.
             */
	services: BluetoothGATTServerService[];
	/** 
 Starts the local GATT server. After it starts, it can conduct GATT server operations. Also, the remote clients can discover and use the services provided by the local Bluetooth GATT Server.
             */
	start(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Stops GATT server operation.
             */
	stop(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Registers a primary service in the local GATT server.
             */
	registerService(service: BluetoothGATTServerServiceInit,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Unregisters all services from the local GATT server.
             */
	unregisterAllServices(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets the ATT MTU for the connection with a client.
             */
	getConnectionMtu(clientAddress: BluetoothAddress,callback: ConnectionMtuCallback,errorCallback: ErrorCallback): void
}

interface BluetoothLEScanCallback {
	/** 
 Called when a new device is successfully discovered in the process of scanning.
             */
	onsuccess(device: BluetoothLEDevice): void
}

interface BluetoothLEAdvertiseCallback {
	/** 
 Called when the advertising state is changed.
             */
	onstate(state: BluetoothAdvertisingState): void
}

interface BluetoothLEConnectChangeCallback {
	/** 
 Called at the beginning of connect to a specific LE based service on a remote Bluetooth LE device.
             */
	onconnected(device: BluetoothLEDevice): void
	/** 
 Called at the beginning of disconnect to a specific LE based service on a remote Bluetooth LE device.
             */
	ondisconnected(device: BluetoothLEDevice): void
}

interface ReadValueSuccessCallback {
	/** 
 Called when a characteristic value has been read.
             */
	onread(value: byte[]): void
}

interface GATTRequestReply {
	/** 
 Reply status code.
             */
	statusCode: long;
	/** 
 Response data. It is only relevant for read value requests. It will be ignored in replies to write requests and thus can be uninitialized in such replies.
             */
	data: Bytes;
}

interface ReadValueRequestCallback {
	/** 
 Called when a client makes a read request for GATT characteristic to the connected local GATT server.
             */
	onreadrequest(clientAddress: BluetoothAddress,offset: long): GATTRequestReply
}

interface WriteValueRequestCallback {
	/** 
 Called when a client connected to the local GATT server requests characteristic's value write.
             */
	onwriterequest(clientAddress: BluetoothAddress,value: byte[],offset: long,replyRequired: boolean): GATTRequestReply
}

interface NotificationCallback {
	/** 
 Called when the local GATT server successfully notifies a client of a characteristic's value change.
             */
	onnotificationsuccess(clientAddress: BluetoothAddress): void
	/** 
 Called when the local GATT server fails to notify a client of a characteristic's value change.
             */
	onnotificationfail(clientAddress: BluetoothAddress,error: WebAPIException): void
	/** 
 Called when the last of all client notifications was sent.
             */
	onnotificationfinish(clientAddress: BluetoothAddress): void
}

interface ConnectionMtuCallback {
	/** 
 Called when the requested ATT MTU size for a connection is ready.
             */
	onsuccess(mtu: long): void
}

interface BluetoothDevice {
	/** 
 The readable name of this remote device.
             */
	name: DOMString;
	/** 
 The hardware address of this remote device.
             */
	address: BluetoothAddress;
	/** 
 The device class, which represents the type of the device and the services it provides.
             */
	deviceClass: BluetoothClass;
	/** 
 The bond state of this remote device with the local device.
             */
	isBonded: boolean;
	/** 
 The flag indicating whether the local device recognizes this remote device as a trusted device or not.
             */
	isTrusted: boolean;
	/** 
 The flag indicating whether the connection state of this remote device with the local device.
             */
	isConnected: boolean;
	/** 
 The list of 128-bit service UUIDs available on this remote device.
             */
	uuids: BluetoothUUID[];
	/** 
 Connects to a specified service identified by  on this remote device.
             */
	connectToServiceByUUID(uuid: BluetoothUUID,successCallback: BluetoothSocketSuccessCallback,errorCallback: ErrorCallback): void
}

interface BluetoothLEDevice {
	/** 
 The address of the Bluetooth LE device from the scan result information.
             */
	address: BluetoothAddress;
	/** 
 The name of the Bluetooth LE device from the scan result information.
             */
	name: DOMString;
	/** 
 The transmission power level of the Bluetooth LE device from the scan result information.
             */
	txpowerlevel: long;
	/** 
 The appearance of the Bluetooth LE device from the scan result information.
             */
	appearance: number;
	/** 
 The list of service UUIDs from scan result.
             */
	uuids: BluetoothUUID[];
	/** 
 The list of service solicitation UUIDs available on Bluetooth LE device from the scan result information.
             */
	solicitationuuids: BluetoothLESolicitationUUID[];
	/** 
 The list of service data available on Bluetooth LE device from the scan result information.
             */
	serviceData: BluetoothLEServiceData[];
	/** 
 The manufacturer data from the scan result information.
             */
	manufacturerData: BluetoothLEManufacturerData;
	/** 
 The received signal strength indicator in dBm (decibel-milliwatts) units.
             */
	rssi: long;
	/** 
 Establishes Low Energy connection to the device.
             */
	connect(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Disconnects from the device.
             */
	disconnect(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Checks if Bluetooth Low Energy device is currently connected.
             */
	isConnected(): void
	/** 
 Retrieves a service from the device for the given UUID.
             */
	getService(uuid: BluetoothUUID): BluetoothGATTService
	/** 
 Retrieves list of all service UUIDs from connected GATT server.
             */
	getServiceAllUuids(): void
	/** 
 Registers a listener to be called when the device connects or disconnects.
             */
	addConnectStateChangeListener(listener: BluetoothLEConnectChangeCallback): void
	/** 
 Unregisters a Bluetooth device connection listener.
             */
	removeConnectStateChangeListener(watchID: long): void
	/** 
 Gets the current value of Attribute Protocol(ATT) Maximum Transmission Unit(MTU) from the connected device.
             */
	getAttMtu(): void
	/** 
 Requests the GATT server to change the Attribute Protocol (ATT) Maximum Transmission Unit (MTU) value.
             */
	requestAttMtuChange(newAttMtu: number): void
	/** 
 Registers a listener to be called when ATT MTU value is changed.
             */
	addAttMtuChangeListener(callback: ConnectionMtuCallback): void
	/** 
 Unregisters the ATT MTU value change listener.
             */
	removeAttMtuChangeListener(watchId: long): void
}

interface BluetoothSocket {
	/** 
 The service UUID to which this socket is connected.
             */
	uuid: BluetoothUUID;
	/** 
 The socket state.
             */
	state: BluetoothSocketState;
	/** 
 The peer device to which this socket is connected.
             */
	peer: BluetoothDevice;
	/** 
 Called when an incoming message is received successfully from the peer.
By default, this attribute is set to null.
             */
	onmessage: SuccessCallback;
	/** 
 Called when the socket is closed successfully.
By default, this attribute is set to null.
             */
	onclose: SuccessCallback;
	/** 
 Writes data as a sequence of bytes onto the socket and returns the number of bytes actually written.
             */
	writeData(data: Bytes): void
	/** 
 Reads data from the socket.
             */
	readData(): void
	/** 
 Closes the socket.
             */
	close(): void
}

interface BluetoothClass {
	/** 
 The major device class.
             */
	major: octet;
	/** 
 The minor device class.
             */
	minor: octet;
	/** 
 The services provided by this device and it refers to the  interface for the list of possible
values.
             */
	services: number[];
	/** 
 Checks whether the given service exists in the .
             */
	hasService(service: number): void
}

interface BluetoothClassDeviceMajor {
}

interface BluetoothClassDeviceMinor {
}

interface BluetoothClassDeviceService {
}

interface BluetoothServiceHandler {
	/** 
 The UUID of the service. See .
             */
	uuid: BluetoothUUID;
	/** 
 The name of the service. See .
             */
	name: DOMString;
	/** 
 The flag indicating whether any remote devices is using this service. See .
             */
	isConnected: boolean;
	/** 
 Called when a remote device is connected successfully to this service.
By default, this attribute is set to null.
             */
	onconnect: BluetoothSocketSuccessCallback;
	/** 
 Unregisters a service record from the Bluetooth services record database and stops listening for new connections to this service.
             */
	unregister(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
}

interface BluetoothProfileHandler {
	/** 
 The Bluetooth profile type.
             */
	profileType: BluetoothProfileType;
}

interface BluetoothHealthProfileHandler {
	/** 
 Registers an application for the Sink role.
             */
	registerSinkApplication(dataType: number,name: DOMString,successCallback: BluetoothHealthApplicationSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Connects to the health device which acts as the Source role.
             */
	connectToSource(peer: BluetoothDevice,application: BluetoothHealthApplication,successCallback: BluetoothHealthChannelSuccessCallback,errorCallback: ErrorCallback): void
}

interface BluetoothHealthApplication {
	/** 
 The MDEP data type used for communication, which is referenced in the ISO/IEEE 11073-20601 spec.
             */
	dataType: number;
	/** 
 The friendly name associated with sink application. See .
             */
	name: DOMString;
	/** 
 Called when a health device is connected successfully through this application.
             */
	onconnect: BluetoothHealthChannelSuccessCallback;
	/** 
 Unregisters this application.
             */
	unregister(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
}

interface BluetoothHealthChannel {
	/** 
 The remote device to which this channel is connected. See .
             */
	peer: BluetoothDevice;
	/** 
 The type of this channel. See .
             */
	channelType: BluetoothHealthChannelType;
	/** 
 The health application which is used to communicate with the remote device. See .
             */
	application: BluetoothHealthApplication;
	/** 
 The flag indicating whether any remote device is connected.
             */
	isConnected: boolean;
	/** 
 Closes the connected channel.
 is changed to  and  is invoked when this channel is closed successfully.
             */
	close(): void
	/** 
 Sends data and returns the number of bytes actually written.
             */
	sendData(data: byte[]): void
	/** 
 Sets the listener to receive notifications.
             */
	setListener(listener: BluetoothHealthChannelChangeCallback): void
	/** 
 Unsets the listener. This stops receiving notifications.
             */
	unsetListener(): void
}

interface BluetoothAdapterChangeCallback {
	/** 
 Called when the power state is changed.
             */
	onstatechanged(powered: boolean): void
	/** 
 Called when the name is changed.
             */
	onnamechanged(name: DOMString): void
	/** 
 Called when the visibility is changed.
             */
	onvisibilitychanged(visible: boolean): void
}

interface BluetoothDeviceSuccessCallback {
	/** 
 Called on success.
             */
	onsuccess(device: BluetoothDevice): void
}

interface BluetoothDeviceArraySuccessCallback {
	/** 
 Called when device information is ready.
             */
	onsuccess(devices: BluetoothDevice[]): void
}

interface BluetoothDiscoverDevicesSuccessCallback {
	/** 
 Called at the beginning of a device discovery process for finding the nearby Bluetooth device.
             */
	onstarted(): void
	/** 
 Called when a new device is discovered in the process of inquiry/discovery.
             */
	ondevicefound(device: BluetoothDevice): void
	/** 
 Called when a device is lost from proximity.
After that, this device is no longer visible.
             */
	ondevicedisappeared(address: BluetoothAddress): void
	/** 
 Called when the device discovery process has finished.
             */
	onfinished(foundDevices: BluetoothDevice[]): void
}

interface BluetoothSocketSuccessCallback {
	/** 
 Called when the connection to a service is ready.
             */
	onsuccess(socket: BluetoothSocket): void
}

interface BluetoothServiceSuccessCallback {
	/** 
 Called when registering a service with the local device is successful.
             */
	onsuccess(handler: BluetoothServiceHandler): void
}

interface BluetoothHealthApplicationSuccessCallback {
	/** 
 Called when the application is registered successfully.
             */
	onsuccess(application: BluetoothHealthApplication): void
}

interface BluetoothHealthChannelSuccessCallback {
	/** 
 Called when a connection is established.
             */
	onsuccess(channel: BluetoothHealthChannel): void
}

interface BluetoothHealthChannelChangeCallback {
	/** 
 Called when the message is received.
             */
	onmessage(data: byte[]): void
	/** 
 Called when the health channel is closed.
             */
	onclose(): void
}

interface CalendarManagerObject {
	/** 
 Object representing a calendar manager.
             */
	calendar: CalendarManager;
}

interface CalendarManager {
	/** 
 Gets an array of Calendar objects.
             */
	getCalendars(type: CalendarType,successCallback: CalendarArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 The unified calendar is the aggregation of all calendars that are obtained from  and contains all events or tasks. It does not have the calendar ID nor name which are set to .
             */
	getUnifiedCalendar(type: CalendarType): Calendar
	/** 
 Gets the default calendar, which is used for new items.
             */
	getDefaultCalendar(type: CalendarType): Calendar
	/** 
 Adds a calendar to the calendar database synchronously.
             */
	addCalendar(calendar: Calendar): void
	/** 
 Removes a calendar from the calendar database synchronously.
             */
	removeCalendar(type: CalendarType,id: CalendarId): void
	/** 
 Gets the calendar with the specified identifier and type.
             */
	getCalendar(type: CalendarType,id: CalendarId): Calendar
}

interface Calendar {
	/** 
 Calendar identifier.
             */
	id: CalendarId;
	/** 
 Readable (descriptive) name for a calendar, such as work or personal.
             */
	name: DOMString;
	/** 
 Account identifier.
             */
	accountId: AccountId;
	/** 
 Gets the calendar item with the specified identifier.
             */
	get(id: CalendarItemId): CalendarItem
	/** 
 Adds an item to the calendar synchronously.
             */
	add(item: CalendarItem): void
	/** 
 Adds an array of items to the calendar asynchronously.
             */
	addBatch(items: CalendarItem[],successCallback: CalendarItemArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Updates an existing item in the calendar synchronously with the one specified in the argument.
             */
	update(item: CalendarItem,updateAllInstances: boolean): void
	/** 
 Updates an array of existing items in the calendar asynchronously with the specified values in the argument.
             */
	updateBatch(items: CalendarItem[],successCallback: SuccessCallback,errorCallback: ErrorCallback,updateAllInstances: boolean): void
	/** 
 Removes an item from the calendar that corresponds to the specified identifier. This method will throw an exception if it fails to remove the specified calendar item.
             */
	remove(id: CalendarItemId): void
	/** 
 Removes several items from the calendar asynchronously depending on the specified identifiers.
             */
	removeBatch(ids: CalendarItemId[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Finds and fetches an array of  objects for items stored in the calendar according to the supplied filter if it is valid else it will return all the items stored.
             */
	find(successCallback: CalendarItemArraySuccessCallback,errorCallback: ErrorCallback,filter: AbstractFilter,sortMode: SortMode): void
	/** 
 Adds a listener to receive notifications about calendar changes.
             */
	addChangeListener(successCallback: CalendarChangeCallback): void
	/** 
 Unsubscribes from receiving notification for a calendar item change.
             */
	removeChangeListener(watchId: long): void
}

interface CalendarItem {
	/** 
 Calendar item identifier.
             */
	id: CalendarItemId;
	/** 
 Identifier of the calendar in which this item is saved.
             */
	calendarId: CalendarId;
	/** 
 The last modified date and time of an item.
             */
	lastModificationDate: TZDate;
	/** 
 The textual descriptions of an item.
             */
	description: DOMString;
	/** 
 The short summary or subject for an item.
(See RFC 5545 - Section 3.8.1.12)
             */
	summary: DOMString;
	/** 
 Flag to indicate whether an event is an all-day event.
             */
	isAllDay: boolean;
	/** 
 The start date or time for an item.
(see RFC 5545 - Section 3.8.2.4).
             */
	startDate: TZDate;
	/** 
 The duration of the calendar component.
(See RFC 5545 - Section 3.8.2.5).
             */
	duration: TimeDuration;
	/** 
 The location or the intended venue for the activity defined by the calendar item.
(See RFC 5545 - Section 3.8.1.7)
             */
	location: DOMString;
	/** 
 The global position latitude and longitude of the location where the event is planned to take place.
(See RFC 5545 - Section 3.8.1.6).
             */
	geolocation: SimpleCoordinates;
	/** 
 The name of the person who organized this event.
(See RFC 5545 - Section 3.8.4.3).
             */
	organizer: DOMString;
	/** 
 The visibility (secrecy) level of the item.
(See RFC 5545 - Section 3.8.1.3).
             */
	visibility: CalendarItemVisibility;
	/** 
 The overall confirmation status for a calendar component.
(See RFC 5545 - Section 3.8.1.11).
             */
	status: CalendarItemStatus;
	/** 
 The priority level of an item and may be used to relatively prioritize multiple items during a given period of time.
(See RFC 5545 - Section 3.8.1.9).
             */
	priority: CalendarItemPriority;
	/** 
 The array of the alarms (or reminders) associated to an item.
             */
	alarms: CalendarAlarm[];
	/** 
 The flag that indicates the item categories or subtypes of a calendar component. The categories are useful in searching for a calendar component of a particular type and category.
(See RFC 5545 - Section 3.8.1.2).
             */
	categories: DOMString[];
	/** 
 The array that lists the people attending an event.
(See RFC 5545 - Section 3.8.4.3).
             */
	attendees: CalendarAttendee[];
	/** 
 Converts the calendar item to a string format that will be generated and returned synchronously.
The export format is set using the format parameter.
             */
	convertToString(format: CalendarTextFormat): void
	/** 
 Clones the  object, detached from any calendar.
             */
	clone(): CalendarItem
}

interface CalendarTask {
	/** 
 The due date and time for completing a task. (See RFC 5545 - Section 3.8.2.3).
             */
	dueDate: TZDate;
	/** 
 The date and time when an task is completed.
(See RFC 5545 - Section 3.8.2.1).
             */
	completedDate: TZDate;
	/** 
 The percent of completion of a task.
             */
	progress: number;
}

interface CalendarEvent {
	/** 
 The flag that indicates whether an instance of a recurring event is detached and if it has been modified and saved to the calendar.
             */
	isDetached: boolean;
	/** 
 The end date/time of an event.
             */
	endDate: TZDate;
	/** 
 The availability of a person for an event.
(See RFC 5545 - Section 3.2.9).
             */
	availability: EventAvailability;
	/** 
 The recurrence rule for the event.
             */
	recurrenceRule: CalendarRecurrenceRule;
	/** 
 Expands a recurring event and returns asynchronously the list of instances occurring in a given time interval (inclusive).
             */
	expandRecurrence(startDate: TZDate,endDate: TZDate,successCallback: CalendarEventArraySuccessCallback,errorCallback: ErrorCallback): void
}

interface CalendarAttendee {
	/** 
 The URI for the attendee.
             */
	uri: DOMString;
	/** 
 The name of an attendee.
             */
	name: DOMString;
	/** 
 The role of the attendee.
             */
	role: AttendeeRole;
	/** 
 The participant's attendance status.
(See RFC 5545, Section 3.2.12.)
             */
	status: AttendeeStatus;
	/** 
 The attendee's participation status reply (RSVP).
(See RFC 5545, Section 3.2.17.)
             */
	RSVP: boolean;
	/** 
 The type of a participant.
(See RFC 5545, Section 3.2.3.)
             */
	type: AttendeeType;
	/** 
 The participant's group or list membership.
(See RFC 5545, Section 3.2.11.)
             */
	group: DOMString;
	/** 
 The URI of the person who has delegated their participation to this attendee.
(See RFC 5545, Section 3.2.4.)
             */
	delegatorURI: DOMString;
	/** 
 The URI of the attendee to whom the person has delegated his participation.
(See RFC 5545, Section 3.2.5.)
             */
	delegateURI: DOMString;
	/** 
 The participant's reference in the Contact API.
             */
	contactRef: ContactRef;
}

interface CalendarRecurrenceRule {
	/** 
 The frequency of a recurrence rule.
             */
	frequency: RecurrenceRuleFrequency;
	/** 
 The interval for the recurrence rule to repeat over the unit of time indicated by its frequency.
             */
	interval: number;
	/** 
 The end date of a recurrence duration of an event using either an end date ( attribute) or a number of occurrences ( attribute).
             */
	untilDate: TZDate;
	/** 
 The number of occurrences of a recurring event.
             */
	occurrenceCount: long;
	/** 
 The days of the week associated with the recurrence rule.
             */
	daysOfTheWeek: ByDayValue[];
	/** 
 The list of ordinal numbers that filters which recurrences to include in the recurrence rule's frequency.
             */
	setPositions: short[];
	/** 
 Array to list date/time exceptions for the recurring event.
(See RFC 5545, Section 3.8.5.1.)
             */
	exceptions: TZDate[];
}

interface CalendarEventId {
	/** 
 The calendar event identifier.
             */
	uid: DOMString;
	/** 
 The recurrence ID of a  instance.
             */
	rid: DOMString;
}

interface CalendarAlarm {
	/** 
 The absolute date and time when an alarm should be triggered.
             */
	absoluteDate: TZDate;
	/** 
 The duration before an event starts on which the alarm should be triggered.
             */
	before: TimeDuration;
	/** 
 The notification method used by an alarm.
             */
	method: AlarmMethod;
	/** 
 The description of an alarm.
             */
	description: DOMString;
}

interface CalendarEventArraySuccessCallback {
	/** 
 Called when the list of calendar events is retrieved successfully.
             */
	onsuccess(events: CalendarEvent[]): void
}

interface CalendarItemArraySuccessCallback {
	/** 
 Called when the list of calendar items is retrieved successfully.
             */
	onsuccess(items: CalendarItem[]): void
}

interface CalendarArraySuccessCallback {
	/** 
 Called when the array of  objects is retrieved successfully.
             */
	onsuccess(calendars: Calendar[]): void
}

interface CalendarChangeCallback {
	/** 
 Called when items are added to the calendar.
             */
	onitemsadded(items: CalendarItem[]): void
	/** 
 Called when items are updated in the calendar.
             */
	onitemsupdated(items: CalendarItem[]): void
	/** 
 Called when item are removed from the calendar.
             */
	onitemsremoved(ids: CalendarItemId[]): void
}

interface CallHistoryObject {
	/** 
 Object representing a callhistory.
             */
	callhistory: CallHistory;
}

interface RemoteParty {
	/** 
 An attribute to store the remote party identifier (RPID).
The RPID is a unique identifier used by a service with call capability. It also includes phone numbers.
Contacts are also defined per service, so an RPID can be resolved to a Contact.
A  value means that the remote is hidden (private number).
             */
	remoteParty: DOMString;
	/** 
 An attribute to store the identifier of the person to whom the raw contact belongs.
             */
	personId: PersonId;
}

interface CallHistoryEntry {
	/** 
 An attribute to store the unique identifier of a call record.
             */
	uid: DOMString;
	/** 
 An attribute to store the service type of the call saved to call history.
             */
	type: DOMString;
	/** 
 An attributes to store the features associated with the call service saved to call history.
The following values are supported:
             */
	features: DOMString[];
	/** 
 An attribute to store the remote parties participating in the call.
             */
	remoteParties: RemoteParty[];
	/** 
 An attribute to store the start time of the call.
This attribute is the moment when media channels come up. The exact meaning is defined by the back-end.
             */
	startTime: Date;
	/** 
 An attribute to store the duration of the call in seconds.
This attribute is the duration from media channels coming up to the moment when media channels tear down on the same call service.
If the call is migrated to another service, another call history entry is used.
The exact meaning is defined by the back-end.
             */
	duration: number;
	/** 
 An attribute to indicate whether the call had been dialed, received, missed, blocked, or rejected.
The following values are supported:
             */
	direction: DOMString;
	/** 
 Indicates the phone number of SIM card used for the call.
             */
	callingParty: DOMString;
}

interface CallHistory {
	/** 
 Finds and returns call history.
             */
	find(successCallback: CallHistoryEntryArraySuccessCallback,errorCallback: ErrorCallback,filter: AbstractFilter,sortMode: SortMode,limit: number,offset: number): void
	/** 
 Removes call history synchronously.
             */
	remove(entry: CallHistoryEntry): void
	/** 
 Removes a call history list asynchronously.
             */
	removeBatch(entries: CallHistoryEntry[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Removes call history asynchronously.
             */
	removeAll(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Adds a listener to register for callback to observe call history changes.
             */
	addChangeListener(observer: CallHistoryChangeCallback): void
	/** 
 Removes a listener to unregister a previously registered listener.
             */
	removeChangeListener(handle: long): void
}

interface CallHistoryEntryArraySuccessCallback {
	/** 
 Called when the queries on call history have been successfully completed.
             */
	onsuccess(entries: CallHistoryEntry[]): void
}

interface CallHistoryChangeCallback {
	/** 
 Called when a new call history item is added.
             */
	onadded(newItems: CallHistoryEntry[]): void
	/** 
 Called when the call history has been changed.
             */
	onchanged(changedItems: CallHistoryEntry[]): void
	/** 
 Called when the call history has been removed.
             */
	onremoved(removedItems: DOMString[]): void
}

interface ContactManagerObject {
	/** 
 Object representing a contact manager.
             */
	contact: ContactManager;
}

interface ContactManager {
	/** 
 Gets the available address books.
             */
	getAddressBooks(successCallback: AddressBookArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets the aggregation of all address books.
             */
	getUnifiedAddressBook(): AddressBook
	/** 
 Gets the default address book.
             */
	getDefaultAddressBook(): AddressBook
	/** 
 Adds an addressbook to the contact database synchronously.
             */
	addAddressBook(addressBook: AddressBook): void
	/** 
 Removes an address book from the contact database synchronously.
             */
	removeAddressBook(addressBookId: AddressBookId): void
	/** 
 Gets the address book with the specified identifier.
             */
	getAddressBook(addressBookId: AddressBookId): AddressBook
	/** 
 Gets the person with the specified identifier.
             */
	get(personId: PersonId): Person
	/** 
 Updates a person in the address book synchronously.
             */
	update(person: Person): void
	/** 
 Updates several existing persons in the contact DB asynchronously.
             */
	updateBatch(persons: Person[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Removes a person from the contact DB synchronously.
             */
	remove(personId: PersonId): void
	/** 
 Removes persons from contact DB asynchronously.
             */
	removeBatch(personIds: PersonId[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets an array of all  objects from the contact DB or the ones that match the optionally supplied filter.
             */
	find(successCallback: PersonArraySuccessCallback,errorCallback: ErrorCallback,filter: AbstractFilter,sortMode: SortMode): void
	/** 
 Gets an array of  objects from the contact DB which match the supplied filter. This method is for filtering usageCount property of Person objects.
             */
	findByUsageCount(filter: ContactUsageCountFilter,successCallback: PersonArraySuccessCallback,errorCallback: ErrorCallback,sortModeOrder: SortModeOrder,limit: number,offset: number): void
	/** 
 Subscribes to receive notifications about person changes.
             */
	addChangeListener(successCallback: PersonsChangeCallback): void
	/** 
 Unsubscribes to person changes watch operation.
             */
	removeChangeListener(watchId: long): void
}

interface AddressBook {
	/** 
 Unique identifier of the address book.
             */
	id: AddressBookId;
	/** 
 The address book descriptive name.
             */
	name: DOMString;
	/** 
 Indicates whether the address book is read-only.
             */
	readOnly: boolean;
	/** 
 Account identifier.
             */
	accountId: AccountId;
	/** 
 Gets the contact with the specified identifier.
             */
	get(id: ContactId): Contact
	/** 
 Adds a contact to the address book synchronously.
             */
	add(contact: Contact): void
	/** 
 Adds several contacts to the address book asynchronously.
             */
	addBatch(contacts: Contact[],successCallback: ContactArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Updates a contact in the address book synchronously.
             */
	update(contact: Contact): void
	/** 
 Updates several existing contacts in the address book asynchronously.
             */
	updateBatch(contacts: Contact[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Removes a contact from the address book synchronously.
             */
	remove(id: ContactId): void
	/** 
 Removes several contacts from the address book asynchronously.
             */
	removeBatch(ids: ContactId[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Finds an array of all Contact objects from the specified address book or an array of
Contact objects that match the optionally supplied filter.
             */
	find(successCallback: ContactArraySuccessCallback,errorCallback: ErrorCallback,filter: AbstractFilter,sortMode: SortMode): void
	/** 
 Subscribes to receive notifications about address book changes.
             */
	addChangeListener(successCallback: AddressBookChangeCallback,errorCallback: ErrorCallback): void
	/** 
 Unsubscribes an address book change watch operation.
             */
	removeChangeListener(watchId: long): void
	/** 
 Gets the group with the specified identifier.
             */
	getGroup(groupId: ContactGroupId): ContactGroup
	/** 
 Adds a group to the address book.
             */
	addGroup(group: ContactGroup): void
	/** 
 Updates a group in the address book.
             */
	updateGroup(group: ContactGroup): void
	/** 
 Removes a group from the address book.
             */
	removeGroup(groupId: ContactGroupId): void
	/** 
 Gets an array of all ContactGroup objects from the specified address book.
             */
	getGroups(): void
}

interface Person {
	/** 
 The identifier of the person.
             */
	id: PersonId;
	/** 
 The person display name as a string.
It is selected from the contacts' display names.
             */
	displayName: DOMString;
	/** 
 The count of the contacts of a person.
             */
	contactCount: long;
	/** 
 Indicates whether a person has a phone number.
             */
	hasPhoneNumber: boolean;
	/** 
 Indicates whether the person has an email addresses.
             */
	hasEmail: boolean;
	/** 
 Indicates whether the contact is a favorite.
             */
	isFavorite: boolean;
	/** 
 The URI of a picture of a person.
             */
	photoURI: DOMString;
	/** 
 The URI of a custom ringtone for a contact.
             */
	ringtoneURI: DOMString;
	/** 
 The ID of a contact that represents information of the person.
             */
	displayContactId: ContactId;
	/** 
 Aggregates another person to this person.
             */
	link(personId: PersonId): void
	/** 
 Separates a contact from this person.
             */
	unlink(contactId: ContactId): Person
	/** 
 Gets person's usage count.
             */
	getUsageCount(type: ContactUsageType): void
	/** 
 Resets a person's usage count.
             */
	resetUsageCount(type: ContactUsageType): void
}

interface Contact {
	/** 
 The identifier of a Raw contact.
             */
	id: ContactId;
	/** 
 The identifier of the person corresponding to the raw contact.
             */
	personId: PersonId;
	/** 
 The identifier of the address book that corresponds to the raw contact.
By default, this attribute is set to .
             */
	addressBookId: AddressBookId;
	/** 
 The timestamp for the last update of a contact.
             */
	lastUpdated: Date;
	/** 
 Indicates whether a contact is favorite.
             */
	isFavorite: boolean;
	/** 
 The name of a contact.
             */
	name: ContactName;
	/** 
 The contact addresses.
             */
	addresses: ContactAddress[];
	/** 
 The URI to the picture of the contact.
             */
	photoURI: DOMString;
	/** 
 The telephone numbers of the contact.
             */
	phoneNumbers: ContactPhoneNumber[];
	/** 
 The email addresses of the contact.
             */
	emails: ContactEmailAddress[];
	/** 
 The instant messenger addresses of the contact.
             */
	messengers: ContactInstantMessenger[];
	/** 
 The relationships of the contact.
             */
	relationships: ContactRelationship[];
	/** 
 The extended data of the contact.
             */
	extensions: ContactExtension[];
	/** 
 The birthday of the contact.
             */
	birthday: Date;
	/** 
 The list of anniversaries for the contact.
             */
	anniversaries: ContactAnniversary[];
	/** 
 The organizations the contact belongs to.
             */
	organizations: ContactOrganization[];
	/** 
 The notes associated to the contact.
             */
	notes: DOMString[];
	/** 
 The URLs associated to the contact.
             */
	urls: ContactWebSite[];
	/** 
 The URI to the custom ringtone for the contact.
             */
	ringtoneURI: DOMString;
	/** 
 The URI of a custom message alert for a contact.
             */
	messageAlertURI: DOMString;
	/** 
 The URI of a custom vibration alert for a contact.
             */
	vibrationURI: DOMString;
	/** 
 The groups the contact belongs to.
             */
	groupIds: ContactGroupId[];
	/** 
 Converts the Contact item to a string format.
             */
	convertToString(format: ContactTextFormat): void
	/** 
 Creates a clone of the Contact object, detached from any address book.
             */
	clone(): Contact
}

interface ContactRef {
	/** 
 The address book identifier.
             */
	addressBookId: AddressBookId;
	/** 
 The contact identifier inside the address book.
             */
	contactId: ContactId;
}

interface ContactName {
	/** 
 The name prefix of a contact.
             */
	prefix: DOMString;
	/** 
 The name suffix of a contact.
             */
	suffix: DOMString;
	/** 
 The first (given) name of a contact.
             */
	firstName: DOMString;
	/** 
 The middle name of a contact.
             */
	middleName: DOMString;
	/** 
 The last (family) name of a contact.
             */
	lastName: DOMString;
	/** 
 The nicknames of a contact.
             */
	nicknames: DOMString[];
	/** 
 The phonetic first name of a contact.
             */
	phoneticFirstName: DOMString;
	/** 
 The phonetic middle name of a contact.
             */
	phoneticMiddleName: DOMString;
	/** 
 The phonetic last name of a contact.
             */
	phoneticLastName: DOMString;
	/** 
 The display name of a contact.
             */
	displayName: DOMString;
}

interface ContactOrganization {
	/** 
 The name of an organization.
             */
	name: DOMString;
	/** 
 The organizational unit name.
             */
	department: DOMString;
	/** 
 The job title.
             */
	title: DOMString;
	/** 
 An attribute to store the role, occupation, or business category
(such as "Programmer").
             */
	role: DOMString;
	/** 
 The URI to the logo of a company.
             */
	logoURI: DOMString;
}

interface ContactWebSite {
	/** 
 The URL for the contact's web site.
             */
	url: DOMString;
	/** 
 The type of web site.
             */
	type: DOMString;
}

interface ContactAnniversary {
	/** 
 The date of an anniversary.
             */
	date: Date;
	/** 
 The text describing an anniversary.
             */
	label: DOMString;
}

interface ContactAddress {
	/** 
 The country of the address.
             */
	country: DOMString;
	/** 
 The name of a country subdivision.
             */
	region: DOMString;
	/** 
 The name of the locality.
For example, the city, county, town, or village.
             */
	city: DOMString;
	/** 
 The street address, for example, building number and street name/number.
             */
	streetAddress: DOMString;
	/** 
 Additional address details that are required for an accurate address.
For example, floor number, apartment number, suite name, the name of an office occupant, and so on.
             */
	additionalInformation: DOMString;
	/** 
 The postal code of the location (also known as the zip code in the US).
             */
	postalCode: DOMString;
	/** 
 The default state of an address.
             */
	isDefault: boolean;
	/** 
 The case insensitive list of address types.
             */
	types: DOMString[];
	/** 
 The address label, can hold a custom address type.
             */
	label: DOMString;
}

interface ContactPhoneNumber {
	/** 
 The full phone number.
             */
	number: DOMString;
	/** 
 The default state of the phone number.
             */
	isDefault: boolean;
	/** 
 The case insensitive list of phone types, as defined in RFC 2426.
             */
	types: DOMString[];
	/** 
 The phone number label, can hold a custom phone number type.
             */
	label: DOMString;
}

interface ContactEmailAddress {
	/** 
 The full email address.
             */
	email: DOMString;
	/** 
 The default state of an email address.
             */
	isDefault: boolean;
	/** 
 The case insensitive list of email types.
             */
	types: DOMString[];
	/** 
 The email label, can hold a custom email type.
             */
	label: DOMString;
}

interface ContactInstantMessenger {
	/** 
 The full instant messenger address.
             */
	imAddress: DOMString;
	/** 
 The instant messenger provider type.
             */
	type: ContactInstantMessengerType;
	/** 
 The instant messenger label, can hold a custom messenger type.
             */
	label: DOMString;
}

interface ContactGroup {
	/** 
 The identifier of a group.
             */
	id: ContactGroupId;
	/** 
 The identifier of the address book that the group belongs to.
             */
	addressBookId: AddressBookId;
	/** 
 The name of a group.
             */
	name: DOMString;
	/** 
 The URI to the custom ringtone for a group.
             */
	ringtoneURI: DOMString;
	/** 
 The URI that points to an image that can represent the  object. This attribute only contains a local file URI.
             */
	photoURI: DOMString;
	/** 
 The flag indicating whether the group can be modified or removed.
Some groups cannot be edited if this flag is set to .
             */
	readOnly: DOMString;
}

interface ContactRelationship {
	/** 
 The name of the person in a particular relationship.
             */
	relativeName: DOMString;
	/** 
 The relationship type.
             */
	type: ContactRelationshipType;
	/** 
 The relationship label, can hold a custom relationship type.
             */
	label: DOMString;
}

interface ContactExtension {
	/** 
 The extended data of a contact.
             */
	data1: long;
	/** 
 The extended data of a contact.
             */
	data2: DOMString;
	/** 
 The extended data of a contact.
             */
	data3: DOMString;
	/** 
 The extended data of a contact.
             */
	data4: DOMString;
	/** 
 The extended data of a contact.
             */
	data5: DOMString;
	/** 
 The extended data of a contact.
             */
	data6: DOMString;
	/** 
 The extended data of a contact.
             */
	data7: DOMString;
	/** 
 The extended data of a contact.
             */
	data8: DOMString;
	/** 
 The extended data of a contact.
             */
	data9: DOMString;
	/** 
 The extended data of a contact.
             */
	data10: DOMString;
	/** 
 The extended data of a contact.
             */
	data11: DOMString;
	/** 
 The extended data of a contact.
             */
	data12: DOMString;
}

interface PersonArraySuccessCallback {
	/** 
 Called when a list of persons is retrieved successfully.
             */
	onsuccess(persons: Person[]): void
}

interface ContactArraySuccessCallback {
	/** 
 Called when a list of contacts is retrieved successfully.
             */
	onsuccess(contacts: Contact[]): void
}

interface AddressBookArraySuccessCallback {
	/** 
 Called when a list of address books is retrieved successfully.
             */
	onsuccess(addressbooks: AddressBook[]): void
}

interface AddressBookChangeCallback {
	/** 
 Called when contacts are added to the address book.
             */
	oncontactsadded(contacts: Contact[]): void
	/** 
 Called when contacts are updated in the address book.
             */
	oncontactsupdated(contacts: Contact[]): void
	/** 
 Called when contacts are deleted from the address book.
             */
	oncontactsremoved(contactIds: ContactId[]): void
}

interface PersonsChangeCallback {
	/** 
 Called when persons are added to the person list.
             */
	onpersonsadded(persons: Person[]): void
	/** 
 Called when persons are updated in the person list.
             */
	onpersonsupdated(persons: Person[]): void
	/** 
 Called when persons are deleted from the person list.
             */
	onpersonsremoved(personIds: PersonId[]): void
}

interface ContentManagerObject {
	/** 
 Object representing a content manager.
             */
	content: ContentManager;
}

interface ContentManager {
	/** 
 Updates attributes of content in the content database synchronously.
             */
	update(content: Content): void
	/** 
 Updates a batch of content attributes in the content database asynchronously.
             */
	updateBatch(contents: Content[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets a list of content directories.
             */
	getDirectories(successCallback: ContentDirectoryArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Finds contents that satisfy the conditions set by a filter.
             */
	find(successCallback: ContentArraySuccessCallback,errorCallback: ErrorCallback,directoryId: ContentDirectoryId,filter: AbstractFilter,sortMode: SortMode,count: number,offset: number): void
	/** 
 Scans a file to create or update content in the content database.
             */
	scanFile(contentURI: DOMString,successCallback: ContentScanSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Scans a content directory to create or update content in the content database.
             */
	scanDirectory(contentDirURI: DOMString,recursive: boolean,successCallback: ContentScanSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Cancels a scan process of a content directory.
             */
	cancelScanDirectory(contentDirURI: DOMString): void
	/** 
 Adds a listener which receives notifications when content changes.
             */
	addChangeListener(changeCallback: ContentChangeCallback): void
	/** 
 Removes a listener which receives notifications about content changes.
             */
	removeChangeListener(listenerId: long): void
	/** 
 Sets a listener to receive notifications of content changes.
             */
	setChangeListener(changeCallback: ContentChangeCallback): void
	/** 
 Unsets the listener to unsubscribe from receiving notifications for content changes.
             */
	unsetChangeListener(): void
	/** 
 Gets all playlists.
             */
	getPlaylists(successCallback: PlaylistArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Creates a new playlist.
             */
	createPlaylist(name: DOMString,successCallback: PlaylistSuccessCallback,errorCallback: ErrorCallback,sourcePlaylist: Playlist): void
	/** 
 Removes a playlist.
             */
	removePlaylist(id: PlaylistId,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Creates a content's thumbnail.
             */
	createThumbnail(content: Content,successCallback: ThumbnailSuccessCallback,errorCallback: ErrorCallback): void
}

interface ContentArraySuccessCallback {
	/** 
 Called when the list of content is retrieved successfully.
             */
	onsuccess(contents: Content[]): void
}

interface ContentDirectoryArraySuccessCallback {
	/** 
 Called when the directory list is retrieved successfully.
             */
	onsuccess(directories: ContentDirectory[]): void
}

interface ContentScanSuccessCallback {
	/** 
 Called when the scanning has been completed.
             */
	onsuccess(uri: DOMString): void
}

interface ContentChangeCallback {
	/** 
 Called when content is added.
             */
	oncontentadded(content: Content): void
	/** 
 Called when content is updated.
             */
	oncontentupdated(content: Content): void
	/** 
 Called when content is removed.
             */
	oncontentremoved(id: ContentId): void
	/** 
 Called when a content directory is added.
             */
	oncontentdiradded(contentDir: ContentDirectory): void
	/** 
 Called when a content directory is updated.
             */
	oncontentdirupdated(contentDir: ContentDirectory): void
	/** 
 Called when a content directory is removed.
             */
	oncontentdirremoved(id: ContentDirectoryId): void
}

interface ContentDirectory {
	/** 
 The opaque content directory identifier.
             */
	id: ContentDirectoryId;
	/** 
 The directory path on the device.
             */
	directoryURI: DOMString;
	/** 
 The directory name.
             */
	title: DOMString;
	/** 
 The type of device storage.
             */
	storageType: ContentDirectoryStorageType;
	/** 
 The last modified date for a directory.
             */
	modifiedDate: Date;
}

interface Content {
	/** 
 The list of attributes that are editable to the local backend using the update() or updateBatch() method.
             */
	editableAttributes: DOMString[];
	/** 
 The opaque content identifier.
             */
	id: ContentId;
	/** 
 The content name. The initial value is the file name of the content.
             */
	name: DOMString;
	/** 
 The content type.
             */
	type: ContentType;
	/** 
 The content MIME type.
             */
	mimeType: DOMString;
	/** 
 The content title.
             */
	title: DOMString;
	/** 
 The URI to access the content.
             */
	contentURI: DOMString;
	/** 
 The array of content thumbnail URIs.
             */
	thumbnailURIs: DOMString[];
	/** 
 The date when content has been released publicly. If only the release year is known, then the month and date are set to January and 1st respectively.
             */
	releaseDate: Date;
	/** 
 The last modified date for a content item.
             */
	modifiedDate: Date;
	/** 
 The file size of the content in bytes.
             */
	size: number;
	/** 
 The content description.
             */
	description: DOMString;
	/** 
 The content rating. This value can range from  to .
             */
	rating: number;
	/** 
 Boolean value that indicates whether a content item is marked as a favorite.
             */
	isFavorite: boolean;
}

interface VideoContent {
	/** 
 The geographical location where the video has been made.
             */
	geolocation: SimpleCoordinates;
	/** 
 The album name to which the video belongs.
             */
	album: DOMString;
	/** 
 The list of artists who created the video.
             */
	artists: DOMString[];
	/** 
 The video duration in milliseconds.
             */
	duration: number;
	/** 
 The width of a video in pixels.
             */
	width: number;
	/** 
 The height of the video in pixels.
             */
	height: number;
}

interface AudioContentLyrics {
	/** 
 The type of lyrics, that is, whether they are synchronized with the music.
             */
	type: AudioContentLyricsType;
	/** 
 The array of timestamps in milliseconds for lyrics.
             */
	timestamps: number[];
	/** 
 The array of lyrics snippets.
             */
	texts: DOMString[];
}

interface AudioContent {
	/** 
 The album name to which the audio belongs.
             */
	album: DOMString;
	/** 
 The list of genres to which the audio belongs.
             */
	genres: DOMString[];
	/** 
 The list of artists who created the audio.
             */
	artists: DOMString[];
	/** 
 The list of composers for the music.
             */
	composers: DOMString[];
	/** 
 The lyrics of a song in an audio file.
             */
	lyrics: AudioContentLyrics;
	/** 
 The copyright information.
             */
	copyright: DOMString;
	/** 
 The audio bitrate in bits per second. By default, this value is .
             */
	bitrate: number;
	/** 
 The track number if the audio belongs to an album.
             */
	trackNumber: number;
	/** 
 The audio duration in milliseconds.
             */
	duration: number;
}

interface ImageContent {
	/** 
 The geographical location where the image has been made.
             */
	geolocation: SimpleCoordinates;
	/** 
 The width of an image in pixels.
             */
	width: number;
	/** 
 The height of an image in pixels.
             */
	height: number;
	/** 
 The image orientation.
             */
	orientation: ImageContentOrientation;
}

interface PlaylistItem {
	/** 
 Content contained in this playlist item.
             */
	content: Content;
}

interface Playlist {
	/** 
 Identifier of a playlist.
             */
	id: PlaylistId;
	/** 
 Name of a playlist (case sensitive and unique).
             */
	name: DOMString;
	/** 
 Number of playlist items in the playlist.
             */
	numberOfTracks: long;
	/** 
 Thumbnail URI of a playlist.
             */
	thumbnailURI: DOMString;
	/** 
 Adds a content item to a playlist.
             */
	add(item: Content): void
	/** 
 Adds tracks to a playlist as a batch, asynchronously.
             */
	addBatch(items: Content[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Removes a track from a playlist.
             */
	remove(item: PlaylistItem): void
	/** 
 Removes tracks from a playlist as a batch, asynchronously.
             */
	removeBatch(items: PlaylistItem[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets playlist items from a playlist.
             */
	get(successCallback: PlaylistItemArraySuccessCallback,errorCallback: ErrorCallback,count: long,offset: long): void
	/** 
 Changes the play order of all playlist items in the playlist.
             */
	setOrder(items: PlaylistItem[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Moves the specified item up or down a specified amount in the play order.
             */
	move(item: PlaylistItem,delta: long,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
}

interface PlaylistArraySuccessCallback {
	/** 
 Called when the  method completes successfully.
             */
	onsuccess(playlists: Playlist[]): void
}

interface PlaylistSuccessCallback {
	/** 
 Called when the  method completes successfully.
             */
	onsuccess(playlist: Playlist): void
}

interface PlaylistItemArraySuccessCallback {
	/** 
 Called when the  method completes successfully.
             */
	onsuccess(items: PlaylistItem[]): void
}

interface ThumbnailSuccessCallback {
	/** 
 Called when the  method completes successfully.
             */
	onsuccess(path: DOMString): void
}

interface ConsoleManagerObject {
	/**  */
	console: ConsoleManager;
}

interface ConsoleManager {
	/** 
 Prints a line of text to a standard output.
The text will be logged as a standard message.
             */
	log(text: DOMString): void
	/** 
 Prints a line of text to a standard output.
The text will be logged as an error message.
             */
	error(text: DOMString): void
	/** 
 Prints a line of text to a standard output.
The text will be logged as a warning message.
             */
	warn(text: DOMString): void
	/** 
 Prints a line of text to a standard output.
The text will be logged as an info message.
             */
	info(text: DOMString): void
	/** 
 Prints a line of text to a standard output.
The text will be logged as a debug message.
             */
	debug(text: DOMString): void
	/** 
 If the specified expression is false,
the message is written to the console.
             */
	assert(expression: boolean,message: DOMString): void
	/** 
 Prints a JavaScript representation of the specified object.
If the object being logged is an HTML element, the JavaScript
Object view is forced.
             */
	dir(obj: Object): void
	/** 
 Prints an XML/HTML Element representation of the specified object
if possible or the JavaScript Object if it is not.
             */
	dirxml(obj: Object): void
	/** 
 Starts a new timer with an associated label. When  is called with the same label, the timer is stopped and the elapsed time
is displayed in the console. Timer values are accurate to the sub-millisecond.
             */
	time(label: DOMString): void
	/** 
 Stops the timer with the specified label and prints the elapsed time.
             */
	timeEnd(label: DOMString): void
}

interface CordovaManagerObject {
	/**  */
	cordova: Cordova;
}

interface Cordova {
}

interface SuccessCallback {
	/** 
 Success
             */
	onsuccess(): void
}

interface ErrorCallback {
	/** 
 Success
             */
	onerror(error: DOMException): void
}

interface DOMException {
	/** 
 The number representing the type of the error.
             */
	code: short;
	/** 
 The short text representing the type of the error.
             */
	name: DOMString;
	/** 
 A text containing information about the error.
             */
	message: DOMString;
}

interface AccelerometerManagerObject {
	/**  */
	accelerometer: Accelerometer;
}

interface Accelerometer {
	/** 
 Gets the current acceleration along the x, y, and z axes.
             */
	getCurrentAcceleration(onsuccess: AccelerometerSuccessCallback,onerror: ErrorCallback): void
	/** 
 Gets the acceleration along the x, y, and z axes at a regular interval.
             */
	watchAcceleration(onsuccess: AccelerometerSuccessCallback,onerror: ErrorCallback,options: AccelerationOptions): void
	/** 
 Stop watching the Acceleration referenced by the watchID parameter.
             */
	clearWatch(watchID: DOMString): void
}

interface Acceleration {
	/** 
 Amount of acceleration on the x-axis. (in m/s^2)
             */
	x: double;
	/** 
 Amount of acceleration on the y-axis. (in m/s^2)
             */
	y: double;
	/** 
 Amount of acceleration on the z-axis. (in m/s^2)
             */
	z: double;
	/** 
 Creation timestamp in milliseconds.
             */
	timestamp: long;
}

interface AccelerometerSuccessCallback {
	/**  */
	onsuccess(acceleration: Acceleration): void
}

interface ErrorCallback {
	/** 
 Success
             */
	onerror(error: DOMException): void
}

interface DeviceManagerObject {
	/**  */
	device: Device;
}

interface Device {
	/** 
 Returns the version of Cordova running on the device.
             */
	cordova: DOMString;
	/** 
 Get the the name of the device's model or product. The value is set by the device manufacturer and may be different across versions of the same product.
             */
	model: DOMString;
	/** 
 Get the device's operating system name.
             */
	platform: DOMString;
	/** 
 Get the device's Universally Unique Identifier (UUID). The details of how a UUID is generated are determined by the device manufacturer and are specific to the device's platform or model.
             */
	uuid: DOMString;
	/** 
 Get the operating system version.
             */
	version: DOMString;
}

interface DialogsManagerObject {
	/**  */
	notification: DialogsManager;
}

interface DialogsManager {
	/** 
 Shows a custom alert with one button.
             */
	alert(message: DOMString,alertCallback: SuccessCallback,title: DOMString,buttonName: DOMString): void
	/** 
 Shows a custom confirm box with set of buttons.
             */
	confirm(message: DOMString,confirmCallback: ConfirmCallback,title: DOMString,buttonNames: DOMString[]): void
	/** 
 Shows a custom confirm box with set of buttons.
             */
	prompt(message: DOMString,promptCallback: PromptCallback,title: DOMString,buttonNames: DOMString[],defaultText: DOMString): void
	/** 
 Method allows to make custom number of beeps by device.
             */
	beep(times: long): void
}

interface PromptData {
	/** 
 The index of button, which was pressed by user.
the index uses one-based indexing, so the values could be 1, 2, 3, etc.
             */
	buttonIndex: long;
	/** 
 The text entered by user in the prompt of dialog box.
             */
	input1: DOMString;
}

interface ConfirmCallback {
	/** 
 Called when the user perform action on confirm dialog.
             */
	onsuccess(buttonIndex: long): void
}

interface PromptCallback {
	/** 
 Called when the user perform action on prompt dialog.
             */
	onsuccess(data: PromptData): void
}

interface DeviceReadyEventCallback {
	/** 
 Called when Cordova is fully loaded.
             */
	ondeviceready(): void
}

interface PauseEventCallback {
	/** 
 Called when an application is put into the background.
             */
	onpause(): void
}

interface ResumeEventCallback {
	/** 
 Called when an application is retrieved from the background.
             */
	onresume(): void
}

interface BackButtonEventCallback {
	/** 
 Called when user presses the back button.
             */
	onbackbutton(): void
}

interface MenuButtonEventCallback {
	/** 
 Called when user presses the menu button.
             */
	onmenubutton(): void
}

interface SearchButtonEventCallback {
	/** 
 Called when user presses the search button.
             */
	onsearchbutton(): void
}

interface StartCallEventCallback {
	/** 
 Called when user presses the start call button.
             */
	onstartcallbutton(): void
}

interface EndCallButtonEventCallback {
	/** 
 Called when user presses the end call button.
             */
	onendcallbutton(): void
}

interface VolumeDownButtonEventCallback {
	/** 
 Called when user presses the volume down button.
             */
	onvolumedownbutton(): void
}

interface VolumeUpButtonEventCallback {
	/** 
 Called when user presses the volume up button.
             */
	onvolumeupbutton(): void
}

interface FileUploadOptions {
	/** 
 The name of the form element. Defaults to file
             */
	fileKey: DOMString;
	/** 
 The file name to use when saving the file on the server. Defaults to image.jpg.
             */
	fileName: DOMString;
	/** 
 The HTTP method to use - either PUT or POST. Defaults to POST.
             */
	httpMethod: DOMString;
	/** 
 The mime type of the data to upload. Defaults to image/jpeg.
             */
	mimeType: DOMString;
	/** 
 A set of optional key/value pairs to pass in the HTTP request.
             */
	params: FileUploadParams;
	/** 
 Whether to upload the data in chunked streaming mode. Defaults to true.
             */
	chunkedMode: boolean;
	/** 
 A map of header name/header values. Use an array to specify more than one value.
             */
	headers: HeaderFields;
}

interface FileUploadResult {
	/** 
 The number of bytes sent to the server as part of the upload.
             */
	bytesSent: long;
	/** 
 The HTTP response code returned by the server.
             */
	responseCode: long;
	/** 
 The HTTP response returned by the server.
             */
	response: DOMString;
	/** 
 The HTTP response headers by the server.
             */
	headers: HeaderFields;
}

interface FileTransferError {
	/** 
 One of the predefined error codes listed above.
             */
	code: short;
	/** 
 URL to the source.
             */
	source: DOMString;
	/** 
 URL to the target.
             */
	target: DOMString;
	/** 
 HTTP status code. This attribute is only available when a response code is received from the HTTP connection.
             */
	http_status: long;
	/** 
 Response body. This attribute is only available when a response is received from the HTTP connection.
             */
	body: DOMString;
	/** 
 Either e.getMessage or e.toString
             */
	_exception: DOMString;
}

interface FileTransfer {
	/** 
 Called with a ProgressEvent whenever a new chunk of data is transferred.
             */
	onprogress: ProgressCallback;
	/** 
 Sends a file to a server.
             */
	upload(fileURL: DOMString,server: DOMString,successCallback: FileUploadSuccessCallback,errorCallback: FileTransferErrorCallback,options: FileUploadOptions,trustAllHosts: boolean): void
	/** 
 Downloads a file from a server.
             */
	download(source: DOMString,target: DOMString,successCallback: FileDownloadSuccessCallback,errorCallback: FileTransferErrorCallback,trustAllHosts: boolean,options: FileDownloadOptions): void
	/** 
 Aborts an in-progress transfer. The onerror callback is passed a FileTransferError object which has an error code of FileTransferError.ABORT_ERR.
             */
	abort(): void
}

interface FileUploadSuccessCallback {
	/** 
 Called when upload is successfully finished.
             */
	onsuccess(result: FileUploadResult): void
}

interface FileDownloadSuccessCallback {
	/** 
 Called when download is successfully finished.
             */
	onsuccess(file: FileEntry): void
}

interface FileTransferErrorCallback {
	/** 
 Called when file transfer is finished with an error
             */
	onerror(error: FileTransferError): void
}

interface ProgressEvent {
	/** 
 The type of event, e.g. "click", "hashchange", or "submit".
             */
	type: DOMString;
	/** 
 True if event's goes through its target attribute value's ancestors in reverse tree order, and false otherwise.
             */
	bubbles: boolean;
	/** 
 Cancel bubble
             */
	cancelBubble: boolean;
	/** 
 Its return value does not always carry meaning, but true can indicate that part of the operation during which event was dispatched, can be canceled.
             */
	cancelable: boolean;
	/** 
 True when the length of the transferred content is known.
             */
	lengthComputable: boolean;
	/** 
 Number of bytes transferred.
             */
	loaded: number;
	/** 
 The total length of the content.  This attribute is set when lengthComputable is true.
             */
	total: number;
	/** 
 The object to which event is dispatched.
             */
	target: EventTarget;
}

interface FileSystemManagerObject {
	/**  */
	file: FileSystemManager;
}

interface FileSystemManager {
	/** 
 Read-only directory where the application is installed.
             */
	applicationDirectory: DOMString;
	/** 
 Root directory of the application's sandbox.
             */
	applicationStorageDirectory: DOMString;
	/** 
 Persistent and private data storage within the application's sandbox using internal memory
             */
	dataDirectory: DOMString;
	/** 
 Directory for cached data files or any files that your app can re-create easily. The OS may delete these files when the device runs low on storage, nevertheless, apps should not rely on the OS to delete files in here.
             */
	cacheDirectory: DOMString;
	/** 
 Application space on external storage.
             */
	externalApplicationStorageDirectory: DOMString;
	/** 
 Where to put app-specific data files on external storage.
             */
	externalDataDirectory: DOMString;
	/** 
 Application cache on external storage.
             */
	externalCacheDirectory: DOMString;
	/** 
 External storage root.
             */
	externalRootDirectory: DOMString;
	/** 
 Temp directory that the OS can clear at will. Do not rely on the OS to clear this directory; your app should always remove files as applicable.
             */
	tempDirectory: DOMString;
	/** 
 Holds app-specific files that should be synced.
             */
	syncedDataDirectory: DOMString;
	/** 
 Files private to the app, but that are meaningful to other application (e.g. Office files).
             */
	documentsDirectory: DOMString;
	/** 
 Files globally available to all applications.
             */
	sharedDirectory: DOMString;
}

interface FileSystem {
	/** 
 This is the name of the file system.
             */
	name: DOMString;
	/** 
 The root directory of the file system.
             */
	root: DirectoryEntry;
}

interface Metadata {
	/** 
 This is the time at which the file or directory was last modified.
             */
	modificationTime: Date;
	/** 
 The size of the file, in bytes. This must return 0 for directories.
             */
	size: number;
}

interface FileError {
	/**  */
	code: number;
}

interface File {
	/** 
 The name of the file.
             */
	name: DOMString;
	/** 
 The full path of the file, including the name.
             */
	localURL: DOMString;
	/** 
 The mime type of the file.
             */
	type: DOMString;
	/** 
 The last modified date of the file.
             */
	lastModified: Date;
	/** 
 The size of the file in bytes.
             */
	size: long long;
}

interface LocalFileSystem {
	/** 
 Request a file system in which to store application data.
             */
	requestFileSystem(type: short,size: long long,successCallback: FileSystemCallback,errorCallback: ErrorCallback): void
	/** 
 Retrieves a  or  using local URI.
             */
	resolveLocalFileSystemURL(uri: DOMString,successCallback: EntryCallback,errorCallback: ErrorCallback): void
}

interface Entry {
	/** 
 True if the entry is a file.
             */
	isFile: boolean;
	/** 
 True if the entry is a directory.
             */
	isDirectory: boolean;
	/** 
 The full absolute path from the root to the entry. An absolute path is a relative path from the root directory, prepended with a "/".
             */
	fullPath: DOMString;
	/** 
 The name of the entry, excluding the path leading to it.
             */
	name: DOMString;
	/** 
 The file system where the entry resides.
             */
	filesystem: FileSystem;
	/** 
 Look up metadata about this entry.
             */
	getMetadata(onsuccess: MetadataCallback,onerror: ErrorCallback): void
	/** 
 Move an entry to a different location on the file system.
             */
	moveTo(parent: DirectoryEntry,newName: DOMString,onsuccess: EntryCallback,onerror: ErrorCallback): void
	/** 
 Copy an entry to a different location on the file system.
             */
	copyTo(parent: DirectoryEntry,newName: DOMString,onsuccess: EntryCallback,onerror: ErrorCallback): void
	/** 
 Look up the parent DirectoryEntry containing this Entry. If this Entry is the root of its filesystem, its parent is itself.
             */
	getParent(onsuccess: EntryCallback,onerror: ErrorCallback): void
	/** 
 Deletes a file or directory. It is an error to attempt to delete a directory that is not empty. It is an error to attempt to delete the root directory of a filesystem.
             */
	remove(onsuccess: VoidCallback,onerror: ErrorCallback): void
	/** 
 Returns a URL that can be used to identify this entry. Unlike the URN defined in [FILE-API-ED], it has no specific expiration; as it describes a location on disk, it should be valid at least as long as that location exists.
             */
	toURL(): void
}

interface DirectoryEntry {
	/** 
 Creates a new DirectoryReader to read Entries from this Directory.
             */
	createReader(): DirectoryReader
	/** 
 Creates or looks up a directory.
             */
	getDirectory(path: DOMString,options: Flags,onsuccess: EntryCallback,onerror: ErrorCallback): void
	/** 
 Creates or looks up a file.
             */
	getFile(path: DOMString,options: Flags,onsuccess: EntryCallback,onerror: ErrorCallback): void
	/** 
 Deletes a directory and all of its contents, if any. In the event of an error [e.g. trying to delete a directory that contains a file that cannot be removed], some of the contents of the directory may be deleted. It is an error to attempt to delete the root directory of a filesystem.
             */
	removeRecursively(onsuccess: VoidCallback,onerror: ErrorCallback): void
}

interface DirectoryReader {
	/** 
 Read the next block of entries from this directory.
             */
	readEntries(onsuccess: EntriesCallback,onerror: ErrorCallback): void
}

interface FileEntry {
	/** 
 Creates a new FileWriter associated with the file that this FileEntry represents.
             */
	createWriter(onsuccess: FileWriterCallback,onerror: ErrorCallback): void
	/** 
 Returns a File that represents the current state of the file that this FileEntry represents.
             */
	file(onsuccess: FileCallback,onerror: ErrorCallback): void
}

interface FileReader {
	/** 
 The state of FileReader. Possible values are ,  or .
             */
	readyState: short;
	/** 
 The contents of the file, that have been read.
Returns a Blob's data as a DOMString, or byte[], or null, depending on the read method that has been called on the FileReader. It is  if any errors occurred.
             */
	result: ReadResultData;
	/** 
 An object describing error, if any occurred.
             */
	error: FileError;
	/** 
 Callback, which is triggered, when the read starts.
             */
	onloadstart: ProgressCallback;
	/** 
 Callback, which is triggered, when the read has successfully completed.
             */
	onload: ProgressCallback;
	/** 
 Callback, which is triggered, when the read has been aborted. For instance, by invoking the abort() method.
             */
	onabort: ProgressCallback;
	/** 
 Callback, which is triggered, when the read has failed.
             */
	onerror: ProgressCallback;
	/** 
 Callback, which is triggered, when the request has completed (either in success or failure).
             */
	onloadend: ProgressCallback;
	/** 
 Aborts current operation of reading file.
             */
	abort(): void
	/** 
 Reads file and return data as a base64-encoded data URL.
             */
	readAsDataURL(blob: Blob): void
	/** 
 Reads text file.
             */
	readAsText(blob: Blob,label: DOMString): void
	/** 
 Reads file as binary and returns a binary string.
             */
	readAsBinaryString(blob: Blob): void
	/** 
 Reads file as an array buffer and result would be .
             */
	readAsArrayBuffer(blob: Blob): void
}

interface FileWriter {
	/** 
 One of the three possible states, either INIT, WRITING, or DONE.
             */
	readyState: short;
	/** 
 The name of the file to be written.
             */
	fileName: DOMString;
	/** 
 The length of the file to be written.
             */
	length: long;
	/** 
 The current position of the file pointer.
             */
	position: long;
	/** 
 An object containing errors.
             */
	error: FileError;
	/** 
 Called when the write starts.
             */
	onwritestart: ProgressCallback;
	/** 
 Called when the request has completed successfully.
             */
	onwrite: ProgressCallback;
	/** 
 Called when the write has been aborted. For instance, by invoking the abort() method.
             */
	onabort: ProgressCallback;
	/** 
 Called when the write has failed.
             */
	onerror: ProgressCallback;
	/** 
 Called when the request has completed (either in success or failure).
             */
	onwriteend: ProgressCallback;
	/** 
 Aborts writing the file.
             */
	abort(): void
	/** 
 Moves the file pointer to the specified byte.
             */
	seek(offset: long): void
	/** 
 Shortens the file to the specified length.
             */
	truncate(size: long): void
	/** 
 Writes data to the file.
             */
	write(data: WriteData): void
}

interface ProgressCallback {
	/** 
 Called with a ProgressEvent object.
             */
	onsuccess(event: ProgressEvent): void
}

interface FileSystemCallback {
	/** 
 Called when the file system was successfully obtained.
             */
	handleEvent(filesystem: FileSystem): void
}

interface EntryCallback {
	/** 
 Used to supply an Entry as a response to a user query.
             */
	handleEvent(entry: Entry): void
}

interface EntriesCallback {
	/** 
 Used to supply an array of Entries as a response to a user query.
             */
	handleEvent(entries: Entry[]): void
}

interface MetadataCallback {
	/** 
 Used to supply file or directory metadata as a response to a user query.
             */
	handleEvent(metadata: Metadata): void
}

interface FileWriterCallback {
	/** 
 Used to supply a FileWriter as a response to a user query.
             */
	handleEvent(fileWriter: FileWriter): void
}

interface FileCallback {
	/** 
 Used to supply a File as a response to a user query.
             */
	handleEvent(file: File): void
}

interface VoidCallback {
	/** 
 .
             */
	handleEvent(): void
}

interface ErrorCallback {
	/** 
 There was an error with the request. Details are provided by the FileError parameter.
             */
	handleEvent(err: FileError): void
}

interface GlobalizationManagerObject {
	/**  */
	globalization: GlobalizationManager;
}

interface GlobalizationManager {
	/** 
 Gets the BCP 47 language tag for the client's current language.
             */
	getPreferredLanguage(onsuccess: StringSuccessCallback,onerror: ErrorCallback): void
	/** 
 Returns the BCP 47 compliant tag for the client's current locale settings.
             */
	getLocaleName(onsuccess: StringSuccessCallback,onerror: ErrorCallback): void
	/** 
 Returns a date formatted as a string according to the client's locale and timezone.
             */
	dateToString(date: Date,onsuccess: StringSuccessCallback,onerror: ErrorCallback,options: DateOptions): void
	/** 
 Returns a pattern string to format and parse currency values according to
the client's user preferences and ISO 4217 currency code.
             */
	getCurrencyPattern(currencyCode: DOMString,onsuccess: PatternSuccessCallback,onerror: ErrorCallback): void
	/** 
 Returns an array of the names of the months or the days of the week,
depending on the client's user preferences and calendar.
             */
	getDateNames(onsuccess: ArrayStringSuccessCallback,onerror: ErrorCallback,options: GetDateNamesOptions): void
	/** 
 Gets a pattern string to format and parse dates according to the client's
user preferences.
             */
	getDatePattern(onsuccess: GetDatePatternSuccessCallback,onerror: ErrorCallback,options: DateOptions): void
	/** 
 Gets the first day of the week according to the client's
user preferences and calendar.
             */
	getFirstDayOfWeek(onsuccess: LongSuccessCallback,onerror: ErrorCallback): void
	/** 
 Gets a pattern string to format and parse numbers
according to the client's user preferences.
             */
	getNumberPattern(onsuccess: GetNumberPatternSuccessCallback,onerror: ErrorCallback,options: NumberPatternOptions): void
	/** 
 Indicates whether or not daylight savings time is in effect
for a given date using the client's time zone and calendar.
             */
	isDayLightSavingsTime(date: Date,onsuccess: DSTSuccessCallback,onerror: ErrorCallback): void
	/** 
 Returns a number formatted as a string
according to the client's user preferences.
             */
	numberToString(number: double,onsuccess: StringSuccessCallback,onerror: ErrorCallback,options: NumberPatternOptions): void
	/** 
 Parses a date formatted as a DOMString according to the client's
user preferences and calendar using the time zone of the client.
Returns the corresponding date object.
             */
	stringToDate(dateString: DOMString,onsuccess: GlobalizationDateSuccessCallback,onerror: ErrorCallback,options: DateOptions): void
	/** 
 Parses a number formatted as a string according to the client's
user preferences and returns the corresponding number.
             */
	stringToNumber(numberString: DOMString,onsuccess: DoubleSuccessCallback,onerror: ErrorCallback,options: NumberPatternOptions): void
}

interface DSTSuccessCallback {
	/** 
 Called when a function returns the DST data successfully.
             */
	onsuccess(properties: object): void
}

interface StringSuccessCallback {
	/** 
 Called when a function returns the properties object with data successfully.
             */
	onsuccess(properties: object): void
}

interface ArrayStringSuccessCallback {
	/** 
 Called when a function returns the array of DOMString values successfully.
             */
	onsuccess(properties: object): void
}

interface LongSuccessCallback {
	/** 
 Called when a function returns a numeric (long) data successfully.
             */
	onsuccess(properties: object): void
}

interface DoubleSuccessCallback {
	/** 
 Called when a function returns a numeric (double) data successfully.
             */
	onsuccess(properties: object): void
}

interface GlobalizationDateSuccessCallback {
	/** 
 Called when a function returns a GlobalizationDate object successfully.
             */
	onsuccess(date: Date): void
}

interface PatternSuccessCallback {
	/** 
 Called when a function returns the currency pattern information successfully.
             */
	onsuccess(pattern: CurrencyPattern): void
}

interface GetDatePatternSuccessCallback {
	/** 
 Called when a function returns the date pattern information successfully.
             */
	onsuccess(pattern: DatePattern): void
}

interface GetNumberPatternSuccessCallback {
	/** 
 Called when a function returns the number pattern information successfully.
             */
	onsuccess(pattern: NumberPattern): void
}

interface GlobalizationError {
	/** 
 One of the following codes representing the error type.
             */
	code: long;
	/** 
 A text message that includes the error's explanation and/or details.
             */
	message: DOMString;
}

interface ErrorCallback {
	/** 
 Success
             */
	onerror(error: DOMException): void
}

interface Media {
	/** 
 A URI containing the audio content.
             */
	src: DOMString;
	/** 
 The callback that executes after a Media object has completed the current play, record, or stop action.
             */
	successCallback: SuccessCallback;
	/** 
 The callback that executes if an error occurs.
             */
	errorCallback: MediaErrorCallback;
	/** 
 The callback that executes to indicate status changes.
             */
	statusCallback: StatusChangeCallback;
	/** 
 Returns the current position within an audio file in seconds.
             */
	getCurrentPosition(positionSuccessCallback: PositionSuccessCallback,errorCallback: MediaErrorCallback): void
	/** 
 Returns the duration of an audio file in seconds. If the duration is unknown, it returns a value of -1.
             */
	getDuration(): void
	/** 
 Pauses playing an audio file.
             */
	pause(): void
	/** 
 Starts or resumes playing an audio file.
             */
	play(): void
	/** 
 Releases the underlying operating system's audio resources.
Applications should call the release function for any Media resource that is no longer needed.
             */
	release(): void
	/** 
 Sets the current position within an audio file.
             */
	seekTo(position: double): void
	/** 
 Set the volume for an audio file.
             */
	setVolume(volume: double): void
	/** 
 Starts recording an audio file.
             */
	startRecord(): void
	/** 
 Stops recording an audio file.
             */
	stopRecord(): void
	/** 
 Stops playing an audio file.
             */
	stop(): void
}

interface MediaError {
	/** 
 One of the predefined error codes listed above.
             */
	code: short;
}

interface MediaErrorCallback {
	/** 
 Success
             */
	onerror(error: MediaError): void
}

interface StatusChangeCallback {
	/** 
 Called when the status of the  object has been changed.
             */
	onchanged(status: short): void
}

interface PositionSuccessCallback {
	/** 
 Called when getting current position of the media file is retrieved successfully.
             */
	onsuccess(position: double): void
}

interface NetworkInformationManagerObject {
	/**  */
	connection: NetworkInformationManager;
}

interface NetworkInformationManager {
	/** 
 Returns the current connection type. The value returned is one of the following strings, case-sensitively: unknown, ethernet, wifi, 2g, 3g, 4g, none.
             */
	type: DOMString;
}

interface Connection {
	/** 
 The value returned is "unknown".
             */
	UNKNOWN: DOMString;
	/** 
 The value returned is "ethernet".
             */
	ETHERNET: DOMString;
	/** 
 The value returned is "wifi".
             */
	WIFI: DOMString;
	/** 
 The value returned is "2g".
             */
	CELL_2G: DOMString;
	/** 
 The value returned is "3g".
             */
	CELL_3G: DOMString;
	/** 
 The value returned is "4g".
             */
	CELL_4G: DOMString;
	/** 
 The value returned is "cellular".
             */
	CELL: DOMString;
	/** 
 The value returned is "none".
             */
	NONE: DOMString;
}

interface OnlineEventCallback {
	/** 
 Called when an application goes online.
             */
	online(): void
}

interface OfflineEventCallback {
	/** 
 Called when an application goes offline.
             */
	offline(): void
}

interface DataControlManagerObject {
	/** 
 Object representing a data control manager.
             */
	datacontrol: DataControlManager;
}

interface DataControlManager {
	/** 
 Gets  with a given DataType.
             */
	getDataControlConsumer(providerId: DOMString,dataId: DOMString,type: DataType): DataControlConsumerObject
}

interface DataControlConsumerObject {
	/** 
 An attribute to store the DataType.
             */
	type: DataType;
	/** 
 An attribute to hold a provider identifier of the application with whom it shares the DataControl.
This attribute should be known to users who want to interact with the application.
             */
	providerId: DOMString;
	/** 
 The dataId identifies specific data, usually a database table to process(insert, delete, update).
The string consists of one or more components, separated by a slash("/").
             */
	dataId: DOMString;
	/** 
 Adds a listener to receive notifications about provider data changes.
             */
	addChangeListener(dataChangeCallback: DataControlChangeCallback,errorCallback: ErrorCallback): void
	/** 
 Removes data change listener.
             */
	removeChangeListener(watchId: long): void
}

interface SQLDataControlConsumer {
	/** 
 Inserts new rows into a table owned by an SQL-type data control provider.
             */
	insert(reqId: number,insertionData: RowData,successCallback: DataControlInsertSuccessCallback,errorCallback: DataControlErrorCallback): void
	/** 
 Updates values of a table owned by an SQL-type data control provider.
             */
	update(reqId: number,updateData: RowData,where: DOMString,successCallback: DataControlSuccessCallback,errorCallback: DataControlErrorCallback): void
	/** 
 Delete rows from a table that is owned by an SQL-type data control provider.
             */
	remove(reqId: number,where: DOMString,successCallback: DataControlSuccessCallback,errorCallback: DataControlErrorCallback): void
	/** 
 Selects the specified columns to be queried. The result set of the specified columns is retrieved from a table owned by an SQL-type data control provider.
             */
	select(reqId: number,columns: DOMString[],where: DOMString,successCallback: DataControlSelectSuccessCallback,errorCallback: DataControlErrorCallback,page: number,maxNumberPerPage: number,order: DOMString): void
}

interface MappedDataControlConsumer {
	/** 
 Adds the value associated with the specified key to a key-values map owned by a MAP-type data control provider.
             */
	addValue(reqId: number,key: DOMString,value: DOMString,successCallback: DataControlSuccessCallback,errorCallback: DataControlErrorCallback): void
	/** 
 Removes the value associated with the specified key from a key-values map owned by a MAP-type data control provider.
             */
	removeValue(reqId: number,key: DOMString,value: DOMString,successCallback: DataControlSuccessCallback,errorCallback: DataControlErrorCallback): void
	/** 
 Gets the value associated with the specified key, from a key-values map owned by a MAP-type data control provider.
             */
	getValue(reqId: number,key: DOMString,successCallback: DataControlGetValueSuccessCallback,errorCallback: DataControlErrorCallback): void
	/** 
 Sets the value associated with the specified key to a new value.
             */
	updateValue(reqId: number,key: DOMString,oldValue: DOMString,newValue: DOMString,successCallback: DataControlSuccessCallback,errorCallback: DataControlErrorCallback): void
}

interface DataControlSuccessCallback {
	/** 
 Called on success.
             */
	onsuccess(reqId: number): void
}

interface DataControlErrorCallback {
	/** 
 Called on error.
             */
	onerror(reqId: number,error: WebAPIError): void
}

interface DataControlInsertSuccessCallback {
	/** 
 Called on success.
             */
	onsuccess(reqId: number,insertRowId: long): void
}

interface DataControlSelectSuccessCallback {
	/** 
 Called on success.
             */
	onsuccess(rows: RowData[],reqId: number): void
}

interface DataControlGetValueSuccessCallback {
	/** 
 Called on success.
             */
	onsuccess(values: DOMString[],reqid: number): void
}

interface DataControlChangeCallback {
	/** 
 Called when the data is modified.
             */
	onsuccess(type: EventType,data: RowData): void
}

interface DataSynchronizationManagerObject {
	/** 
 Object representing a data synchronization manager.
             */
	datasync: DataSynchronizationManager;
}

interface SyncInfo {
	/** 
 An attribute to store the URL of the sync server.
             */
	url: DOMString;
	/** 
 An attribute to store the login ID for the sync server.
             */
	id: DOMString;
	/** 
 An attribute to store the login password to the sync server.
             */
	password: DOMString;
	/** 
 An attribute to store the sync mode.
             */
	mode: SyncMode;
	/** 
 An attribute to store the sync type.
             */
	type: SyncType;
	/** 
 An attribute to store the sync interval.
             */
	interval: SyncInterval;
}

interface SyncServiceInfo {
	/** 
 An attribute to enable or disable a service category for sync.
             */
	enable: boolean;
	/** 
 An attribute to indicate the sync service type.
             */
	serviceType: SyncServiceType;
	/** 
 An attribute to store the sync service DB URI of the server.
             */
	serverDatabaseUri: DOMString;
	/** 
 An attribute to store the sync service DB access ID to the server.
             */
	id: DOMString;
	/** 
 An attribute to store the sync service DB access password to the server.
             */
	password: DOMString;
}

interface SyncProfileInfo {
	/** 
 An attribute to store the unique identifier provided by the platform for a profile that has been successfully added.
             */
	profileId: SyncProfileId;
	/** 
 An attribute to store the profile name.
             */
	profileName: DOMString;
	/** 
 An attribute to store sync info.
             */
	syncInfo: SyncInfo;
	/** 
 An attribute to indicate service info.
             */
	serviceInfo: SyncServiceInfo[];
}

interface SyncStatistics {
	/** 
 An attribute to store the last sync status for a corresponding service category.
             */
	syncStatus: SyncStatus;
	/** 
 An attribute to indicate the sync service type.
             */
	serviceType: SyncServiceType;
	/** 
 An attribute to store the last sync time.
             */
	lastSyncTime: Date;
	/** 
 An attribute to indicate the total number of items sent from the server to the client.
             */
	serverToClientTotal: number;
	/** 
 An attribute to indicate the number of added items from the server to the client.
             */
	serverToClientAdded: number;
	/** 
 An attribute to indicate the number of updated items from the server to the client.
             */
	serverToClientUpdated: number;
	/** 
 An attribute to indicate the number of removed items from the server to the client.
             */
	serverToClientRemoved: number;
	/** 
 An attribute to indicate the total number of items from the client to the server.
             */
	clientToServerTotal: number;
	/** 
 An attribute to indicate the number of added items from the client to the server.
             */
	clientToServerAdded: number;
	/** 
 An attribute to indicate the number of updated items from the client to the server.
             */
	clientToServerUpdated: number;
	/** 
 An attribute to indicate the number of removed items from the client to the server.
             */
	clientToServerRemoved: number;
}

interface DataSynchronizationManager {
	/** 
 Adds a sync profile.
             */
	add(profile: SyncProfileInfo): void
	/** 
 Updates an existing sync profile.
             */
	update(profile: SyncProfileInfo): void
	/** 
 Removes an existing sync profile.
             */
	remove(profileId: SyncProfileId): void
	/** 
 Gets the maximum number of supported sync profiles on a platform.
Normally the platform sets a limitation on the number of supported profiles. It returns  or a negative value if no limitation is set.
             */
	getMaxProfilesNum(): void
	/** 
 Gets the current number of sync profiles on a device.
             */
	getProfilesNum(): void
	/** 
 Gets the  object from a given profile ID.
             */
	get(profileId: SyncProfileId): SyncProfileInfo
	/** 
 Gets the information of all sync profiles saved in a device.
             */
	getAll(): void
	/** 
 Starts a sync operation with a given profile ID.
             */
	startSync(profileId: SyncProfileId,progressCallback: SyncProgressCallback): void
	/** 
 Stops an ongoing sync operation that is specified by the  parameter.
             */
	stopSync(profileId: SyncProfileId): void
	/** 
 Gets the sync statistics of a given profile ID.
             */
	getLastSyncStatistics(profileId: SyncProfileId): void
}

interface SyncProgressCallback {
	/** 
 Called when a synchronization operation is started and progress is made.
             */
	onprogress(profileId: SyncProfileId,serviceType: SyncServiceType,isFromServer: boolean,totalPerService: number,syncedPerService: number): void
	/** 
 Called when the sync operation has completed.
             */
	oncompleted(profileId: SyncProfileId): void
	/** 
 Called when the sync operation is stopped by the user.
             */
	onstopped(profileId: SyncProfileId): void
	/** 
 Called when the sync operation fails.
             */
	onfailed(profileId: SyncProfileId,error: WebAPIError): void
}

interface DownloadManagerObject {
	/** 
 Object representing a download manager.
             */
	download: DownloadManager;
}

interface DownloadRequest {
	/** 
 An attribute to store the URL of the object to download.
             */
	url: DOMString;
	/** 
 An attribute to store the folder path of the destination folder to which a requested file object will be downloaded.
             */
	destination: DOMString;
	/** 
 An attribute to store the file name for the specified URL.
             */
	fileName: DOMString;
	/** 
 An attribute to store the allowed network type.
             */
	networkType: DownloadNetworkType;
	/** 
 An attribute to store extra HTTP header fields.
             */
	httpHeader: DownloadHTTPHeaderFields;
}

interface DownloadManager {
	/** 
 Starts a download operation with the specified URL information.
             */
	start(downloadRequest: DownloadRequest,downloadCallback: DownloadCallback): void
	/** 
 Cancels an ongoing download operation that is specified by the  parameter.
The abandoned download operation cannot be canceled and trying to do so will result in InvalidValuesError exception.
             */
	cancel(downloadId: long): void
	/** 
 Pauses an ongoing download operation that is specified by the  parameter.
The paused download operation can be resumed later by the  method.
             */
	pause(downloadId: long): void
	/** 
 Abandons a download operation that is specified by the  parameter.
The abandoned download operation cannot be resumed later with the  method.
Trying to resume this download operation will result in  exception.
Calling the  method or the  method with this  will also result in  exception.
All resources needed by download operation are freed.
             */
	abandon(downloadId: long): void
	/** 
 Resumes a paused download operation that is specified by the  parameter.
             */
	resume(downloadId: long): void
	/** 
 Gets the download state of an operation synchronously with the specified ID.
             */
	getState(downloadId: long): DownloadState
	/** 
 Gets the DownloadRequest object from a given ID.
             */
	getDownloadRequest(downloadId: long): DownloadRequest
	/** 
 Gets the MIME type of the downloaded file.
             */
	getMIMEType(downloadId: long): void
	/** 
 Sets the download callback to the download operation of the given ID.
It's possible to change or register the listener of the download operation using the saved ID.
             */
	setListener(downloadId: long,downloadCallback: DownloadCallback): void
}

interface DownloadCallback {
	/** 
 Called when a download is successful and it is called multiple times as the download progresses.
The interval between the  callback is platform-dependent. When the download is started, the  can be .
             */
	onprogress(downloadId: long,receivedSize: number,totalSize: number): void
	/** 
 Called when the download operation is paused by the  method.
             */
	onpaused(downloadId: long): void
	/** 
 Called when the download operation is canceled by the  method.
             */
	oncanceled(downloadId: long): void
	/** 
 Called when the download operation is completed with the final full path or virtual path.
If the same file name already exists in the destination, it is changed according to the platform policy and delivered in this callback.
             */
	oncompleted(downloadId: long,path: DOMString): void
	/** 
 Called when the download operation fails.
             */
	onfailed(downloadId: long,error: WebAPIError): void
}

interface ExifManagerObject {
	/** 
 Object representing a exif manager.
             */
	exif: ExifManager;
}

interface ExifManager {
	/** 
 Gets the  object to manipulate the Exif data in a JPEG file.
             */
	getExifInfo(uri: DOMString,successCallback: ExifInformationSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Saves the Exif data of the  object into the JPEG file.
             */
	saveExifInfo(exifInfo: ExifInformation,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets the thumbnail of the specified JPEG file. If there is no thumbnail in the JPEG file,  is returned.
             */
	getThumbnail(uri: DOMString,successCallback: ExifThumbnailSuccessCallback,errorCallback: ErrorCallback): void
}

interface ExifInformation {
	/** 
 URI of the image.
             */
	uri: DOMString;
	/** 
 Width of the image i.e. the number of points (pixels) per image line.
             */
	width: number;
	/** 
 Height of the image i.e. the number of lines in the image.
             */
	height: number;
	/** 
 Name of the camera manufacturer.
             */
	deviceMaker: DOMString;
	/** 
 Model name or model number of the camera or input device.
             */
	deviceModel: DOMString;
	/** 
 Date and time when the picture was taken.
             */
	originalTime: Date;
	/** 
 Orientation of the image when displayed.
             */
	orientation: ImageContentOrientation;
	/** 
 The f-number when the image was taken.
             */
	fNumber: double;
	/** 
 Photo sensitivity (also called ISO speed and ISO latitude) of the camera or input device.
             */
	isoSpeedRatings: number[];
	/** 
 Exposure time, given in seconds.
             */
	exposureTime: DOMString;
	/** 
 Exposure balance program used by the camera to set exposure when the picture was taken.
             */
	exposureProgram: ExposureProgram;
	/** 
 Boolean value that indicates whether flash was fired when the picture was taken (true: flash fired).
             */
	flash: boolean;
	/** 
 Focal length of the lens, given in mm.
             */
	focalLength: double;
	/** 
 White balance mode set when the picture was taken.
             */
	whiteBalance: WhiteBalanceMode;
	/** 
 Latitude and longitude of the camera (from GPS) when the picture was taken.
             */
	gpsLocation: SimpleCoordinates;
	/** 
 Altitude (from GPS) of the camera when the picture was taken.
             */
	gpsAltitude: double;
	/** 
 Name of the method used for finding the location.
             */
	gpsProcessingMethod: DOMString;
	/** 
 Date and time information relative to UTC (Universal Time Coordinated) provided by GPS when the photo was taken.
             */
	gpsTime: TZDate;
	/** 
 User comment.
             */
	userComment: DOMString;
}

interface ExifInformationSuccessCallback {
	/** 
 Called when the Exif information object has been successfully retrieved.
             */
	onsuccess(exifInfo: ExifInformation): void
}

interface ExifThumbnailSuccessCallback {
	/** 
 Called when the thumbnail of the JPEG file has been successfully retrieved.
             */
	onsuccess(uri: DOMString): void
}

interface FeedbackManagerObject {
	/** 
 Object representing a feedback manager.
             */
	feedback: FeedbackManager;
}

interface FeedbackManager {
	/** 
 Plays various types of reactions that are predefined.
             */
	play(pattern: FeedbackPattern,type: FeedbackType): void
	/** 
 Stops various of vibration patterns.
             */
	stop(): void
	/** 
 Checks if a pattern is supported.
             */
	isPatternSupported(pattern: FeedbackPattern,type: FeedbackType): void
}

interface FileSystemManagerObject {
	/** 
 Object representing a filesystem.
             */
	filesystem: FileSystemManager;
}

interface FileSystemManager {
	/** 
 The maximum file or directory name length for the current platform.
             */
	maxNameLength: long;
	/** 
 The maximum path length limit for the current platform.
             */
	maxPathLength: long;
	/** 
 Opens a file or creates a file pointed by .
             */
	openFile(path: Path,openMode: FileMode,makeParents: boolean): FileHandle
	/** 
 Creates directory pointed by .
             */
	createDirectory(path: Path,makeParents: boolean,successCallback: PathSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Deletes file pointed by .
             */
	deleteFile(path: Path,successCallback: PathSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Deletes directory or directory tree under the current directory pointed by .
             */
	deleteDirectory(path: Path,recursive: boolean,successCallback: PathSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Copies file from location pointed by  to .
             */
	copyFile(sourcePath: Path,destinationPath: Path,overwrite: boolean,successCallback: PathSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Recursively copies directory pointed by  to .
             */
	copyDirectory(sourcePath: Path,destinationPath: Path,overwrite: boolean,successCallback: PathSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Moves file pointed by  to .
             */
	moveFile(sourcePath: Path,destinationPath: Path,overwrite: boolean,successCallback: PathSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Recursively moves directory pointed by  to .
             */
	moveDirectory(sourcePath: Path,destinationPath: Path,overwrite: boolean,successCallback: PathSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Renames file or directory located in  to name .
             */
	rename(path: Path,newName: DOMString,successCallback: PathSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Lists directory content located in .
             */
	listDirectory(path: Path,successCallback: ListDirectorySuccessCallback,errorCallback: ErrorCallback,filter: FileFilter): void
	/** 
 Converts  to file URI.
             */
	toURI(path: Path): void
	/** 
 Checks if given  points to a file.
             */
	isFile(path: Path): void
	/** 
 Checks if given  points to a directory.
             */
	isDirectory(path: Path): void
	/** 
 Checks if given  exists.
             */
	pathExists(path: Path): void
	/** 
 Returns path to directory for given .
             */
	getDirName(path: DOMString): void
	/** 
 Resolves a location to a file handle after validating it.             */
	resolve(location: DOMString,onsuccess: FileSuccessCallback,onerror: ErrorCallback,mode: FileMode): void
	/** 
 Gets information about a storage based on its label.For example: "MyThumbDrive", "InternalFlash".
             */
	getStorage(label: DOMString,onsuccess: FileSystemStorageSuccessCallback,onerror: ErrorCallback): void
	/** 
 Lists the available storages (both internal and external) on a device.
The onsuccess method receives a list of the data structures as input argument containing additional information about each drive found.
It can get storages that would have a label named as "internal0", virtual roots (images, documents, ...), "removable1", "removable2".
"removable1" label is used to resolve sdcard and "removable2" label is used to resolve USB host, if supported.
The vfat filesystem used to sdcard filesystem widely is not case-sensitive.
If you want to handle the file on sdcard, you need to consider case-sensitive filenames are regarded as same name.
             */
	listStorages(onsuccess: FileSystemStorageArraySuccessCallback,onerror: ErrorCallback): void
	/** 
 Adds a listener to subscribe to notifications when a change in storage state occurs.
             */
	addStorageStateChangeListener(onsuccess: FileSystemStorageSuccessCallback,onerror: ErrorCallback): void
	/** 
 Removes a listener to unsubscribe from a storage watch operation.
             */
	removeStorageStateChangeListener(watchId: long): void
}

interface FileSystemStorage {
	/** 
 The storage name.
             */
	label: DOMString;
	/** 
 The storage type as internal or external.
             */
	type: FileSystemStorageType;
	/** 
 The storage state as mounted or not.
             */
	state: FileSystemStorageState;
}

interface FileHandle {
	/** 
 Path, as passed to .
             */
	path: Path;
	/** 
 Sets position indicator in file stream to .
             */
	seek(offset: long long,whence: BaseSeekPosition): void
	/** 
 Sets position indicator in file stream to .
             */
	seekNonBlocking(offset: long long,onsuccess: SeekSuccessCallback,onerror: ErrorCallback,whence: BaseSeekPosition): void
	/** 
 Reads file content as string.
             */
	readString(count: long long,inputEncoding: DOMString): void
	/** 
 Reads file content as string.
             */
	readStringNonBlocking(onsuccess: ReadStringSuccessCallback,onerror: ErrorCallback,count: long long,inputEncoding: DOMString): void
	/** 
 Writes  content to a file.
             */
	writeString(inputString: DOMString,outputEncoding: DOMString): void
	/** 
 Writes  content to a file.
             */
	writeStringNonBlocking(inputString: DOMString,onsuccess: WriteStringSuccessCallback,onerror: ErrorCallback,outputEncoding: DOMString): void
	/** 
 Reads file content as .
             */
	readBlob(size: long long): Blob
	/** 
 Reads file content as .
             */
	readBlobNonBlocking(onsuccess: ReadBlobSuccessCallback,onerror: ErrorCallback,size: long long): void
	/** 
 Writes  to file.
             */
	writeBlob(blob: Blob): void
	/** 
 Writes  to file.
             */
	writeBlobNonBlocking(blob: Blob,onsuccess: SuccessCallback,onerror: ErrorCallback): void
	/** 
 Reads file content as binary data.
             */
	readData(size: long long): Uint8Array
	/** 
 Reads file content as binary data.
             */
	readDataNonBlocking(onsuccess: ReadDataSuccessCallback,onerror: ErrorCallback,size: long long): void
	/** 
 Writes binary data to file.
             */
	writeData(data: Uint8Array): void
	/** 
 Writes binary data to file.
             */
	writeDataNonBlocking(data: Uint8Array,onsuccess: SuccessCallback,onerror: ErrorCallback): void
	/** 
 Flushes data.
             */
	flush(): void
	/** 
 Flushes data.
             */
	flushNonBlocking(onsuccess: SuccessCallback,onerror: ErrorCallback): void
	/** 
 Synchronizes data to storage device.
             */
	sync(): void
	/** 
 Synchronizes data to storage device.
             */
	syncNonBlocking(onsuccess: SuccessCallback,onerror: ErrorCallback): void
	/** 
 Closes file handle.
             */
	close(): void
	/** 
 Closes file handle.
             */
	closeNonBlocking(onsuccess: SuccessCallback,onerror: ErrorCallback): void
}

interface File {
	/** 
 The parent directory handle.
             */
	parent: File;
	/** 
 The file/directory access state in the filesystem.
             */
	readOnly: boolean;
	/** 
 The flag indicating whether it is a file.
             */
	isFile: boolean;
	/** 
 The flag indicating whether it is a directory.
             */
	isDirectory: boolean;
	/** 
 The timestamp when a file is first created in the filesystem.
             */
	created: Date;
	/** 
 The timestamp when the most recent modification is made to a file, usually when the last write operation succeeds.
             */
	modified: Date;
	/** 
 The path of a file after excluding its file name.
             */
	path: DOMString;
	/** 
 The file name after excluding the root name and any path components.
             */
	name: DOMString;
	/** 
 The full path of a file.
             */
	fullPath: DOMString;
	/** 
 The size of this file, in bytes.
             */
	fileSize: number;
	/** 
 The number of files and directories contained in a file handle.
             */
	length: long;
	/** 
 Returns a URI for a file to identify an entry (such as using it as the src attribute on an HTML img element).
The URI has no specific expiration, it should be valid at least as long as the file exists.
             */
	toURI(): void
	/** 
 Lists all files in a directory.
             */
	listFiles(onsuccess: FileArraySuccessCallback,onerror: ErrorCallback,filter: FileFilter): void
	/** 
 Opens the file in the given mode supporting a specified encoding.
             */
	openStream(mode: FileMode,onsuccess: FileStreamSuccessCallback,onerror: ErrorCallback,encoding: DOMString): void
	/** 
 Reads the content of a file as a DOMString.
             */
	readAsText(onsuccess: FileStringSuccessCallback,onerror: ErrorCallback,encoding: DOMString): void
	/** 
 Copies (and overwrites if possible and specified) a file or a
directory from a specified location to another specified location.
             */
	copyTo(originFilePath: DOMString,destinationFilePath: DOMString,overwrite: boolean,onsuccess: SuccessCallback,onerror: ErrorCallback): void
	/** 
 Moves (and overwrites if possible and specified) a file or a directory from a specified location to another.
This operation is different from instantiating copyTo() and then deleting the original file, as on certain platforms, this operation does not require extra disk space.
             */
	moveTo(originFilePath: DOMString,destinationFilePath: DOMString,overwrite: boolean,onsuccess: SuccessCallback,onerror: ErrorCallback): void
	/** 
 Creates a new directory.
             */
	createDirectory(dirPath: DOMString): File
	/** 
 Creates a empty new file in a specified location that is relative to the directory indicated by current  object's  attribute.
             */
	createFile(relativeFilePath: DOMString): File
	/** 
 Resolves an existing file or directory relative to the current directory this operation is performed on and returns a file handle for it.
             */
	resolve(filePath: DOMString): File
	/** 
 Deletes a specified directory and directory tree if specified.
             */
	deleteDirectory(directoryPath: DOMString,recursive: boolean,onsuccess: SuccessCallback,onerror: ErrorCallback): void
	/** 
 Deletes a specified file.This function attempts to asynchronously delete a file under the current directory.
             */
	deleteFile(filePath: DOMString,onsuccess: SuccessCallback,onerror: ErrorCallback): void
}

interface FileStream {
	/** 
 The flag indicating whether the current file pointer is at the end of the file.
             */
	eof: boolean;
	/** 
 The flag indicating the stream position for reads/writes.
             */
	position: long;
	/** 
 The number of bytes that are available for reading from the stream.
             */
	bytesAvailable: long;
	/** 
 Closes this FileStream.
             */
	close(): void
	/** 
 Reads the specified number of characters from the position of the file pointer in a FileStream and returns the characters as a string.
The resulting string length might be shorter than  if EOF is .
             */
	read(charCount: long): void
	/** 
 Reads the specified number of bytes from a FileStream.
             */
	readBytes(byteCount: long): void
	/** 
 Reads the specified number of bytes from this FileStream, encoding the result in base64.
             */
	readBase64(byteCount: long): void
	/** 
 Writes the specified DOMString to a FileStream.
             */
	write(stringData: DOMString): void
	/** 
 Writes the specified bytes to this FileStream.
             */
	writeBytes(byteData: octet[]): void
	/** 
 Writes the result to this FileStream after converting the specified base64 DOMString to bytes.
             */
	writeBase64(base64Data: DOMString): void
}

interface FileSuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(file: File): void
}

interface FileSystemStorageArraySuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(storages: FileSystemStorage[]): void
}

interface FileSystemStorageSuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(storage: FileSystemStorage): void
}

interface PathSuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(path: Path): void
}

interface SeekSuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(position: long long): void
}

interface ReadStringSuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(string: DOMString): void
}

interface WriteStringSuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(bytesCount: long long): void
}

interface ReadBlobSuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(blob: Blob): void
}

interface ReadDataSuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(data: Uint8Array): void
}

interface FileStringSuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(fileStr: DOMString): void
}

interface FileStreamSuccessCallback {
	/** 
 Called when the File.openStream asynchronous call completes successfully.
             */
	onsuccess(filestream: FileStream): void
}

interface ListDirectorySuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(names: DOMString[],path: Path): void
}

interface FileArraySuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(files: File[]): void
}

interface FMRadioObject {
	/** 
 Object representing a exif manager.
             */
	fmradio: FMRadioManager;
}

interface FMRadioManager {
	/** 
 Current frequency(MHz) of the radio.
             */
	frequency: double;
	/** 
 Maximum frequency(MHz) available on the radio device.
             */
	frequencyUpperBound: double;
	/** 
 Minimum frequency(MHz) available on the radio device.
             */
	frequencyLowerBound: double;
	/** 
 Strength of the radio signal ranging between  and  (dBm).
             */
	signalStrength: long;
	/** 
 State of the radio.
             */
	state: RadioState;
	/** 
 Indicates if the FM Radio antenna is connected.
             */
	isAntennaConnected: boolean;
	/** 
 Mute state of the radio. If the value is , there is no sound from the radio (muted). If the values is , sound is playing. The default value is .
             */
	mute: boolean;
	/** 
 Starts playing the radio. This method is available in the  or  state. After , the radio state is .
             */
	start(frequency: double): void
	/** 
 Stops playing the radio. This method is only available in the  state. After the radio stops, the state is .
             */
	stop(): void
	/** 
 Finds a radio channel at a higher frequency than the current one while the radio is playing, asynchronously.
             */
	seekUp(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Finds a radio channel at a lower frequency than the current one while the radio is playing, asynchronously.
             */
	seekDown(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Starts scanning all radio channels, asynchronously.
             */
	scanStart(radioScanCallback: FMRadioScanCallback,errorCallback: ErrorCallback): void
	/** 
 Stops scanning radio channels, asynchronously.
This method is only available in the  state. After the scan stops, the radio state is .
             */
	scanStop(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Sets a listener to receive a notification when the radio is interrupted.
             */
	setFMRadioInterruptedListener(interruptCallback: FMRadioInterruptCallback): void
	/** 
 Unsets the listener to stop receiving a notification when the radio is interrupted.
             */
	unsetFMRadioInterruptedListener(): void
	/** 
 Sets the listener which is called when the status of antenna has been changed.
             */
	setAntennaChangeListener(changeCallback: AntennaChangeCallback): void
	/** 
 Unsets the listener set with the  method.
             */
	unsetAntennaChangeListener(): void
}

interface FMRadioScanCallback {
	/** 
 Called when a new radio channel is discovered in the process of scanning.
             */
	onfrequencyfound(frequency: double): void
	/** 
 Called when the scan is complete.
             */
	onfinished(frequencies: double[]): void
}

interface FMRadioInterruptCallback {
	/** 
 Called when the FM radio is interrupted.
             */
	oninterrupted(reason: RadioInterruptReason): void
	/** 
 Called when the cause of the interrupt ends.
             */
	oninterruptfinished(): void
}

interface AntennaChangeCallback {
	/** 
 Called when the antenna is connected or disconnected which causes value of  attribute to change.
             */
	onchanged(isAntennaConnected: boolean): void
}

interface HumanActivityMonitorManagerObject {
	/** 
 Object representing a exif manager.
             */
	humanactivitymonitor: HumanActivityMonitorManager;
}

interface HumanActivityMonitorManager {
	/** 
 Gets the current human activity data for certain human activity types.
             */
	getHumanActivityData(type: HumanActivityType,successCallback: HumanActivityMonitorSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Starts a sensor and registers a change listener to be called when new human activity data for a given human activity type is available.
             */
	start(type: HumanActivityType,changedCallback: HumanActivityMonitorSuccessCallback,errorCallback: ErrorCallback,option: HumanActivityMonitorOption): void
	/** 
 Stops the sensor and unregisters a previously registered listener for available human activity data.
             */
	stop(type: HumanActivityType): void
	/** 
 Starts the sensor and registers a listener to be called when new accumulative pedometer data is available.
             */
	setAccumulativePedometerListener(changeCallback: HumanActivityMonitorSuccessCallback): void
	/** 
 Stops the sensor and unregisters a previously registered listener for the accumulative pedometer data.
             */
	unsetAccumulativePedometerListener(): void
	/** 
 Registers a listener that is to be called when the activity is recognized.
             */
	addActivityRecognitionListener(type: ActivityRecognitionType,listener: HumanActivityMonitorSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Unsubscribes from receiving notifications when the activity is recognized.
             */
	removeActivityRecognitionListener(listenerId: long,errorCallback: ErrorCallback): void
	/** 
 Starts recording human activity data for a given human activity type.
             */
	startRecorder(type: HumanActivityRecorderType,option: HumanActivityRecorderOption): void
	/** 
 Stops recording human activity data for a given human activity type.
             */
	stopRecorder(type: HumanActivityRecorderType): void
	/** 
 Reads the recorded human activity data with some query.
             */
	readRecorderData(type: HumanActivityRecorderType,query: HumanActivityRecorderQuery,successCallback: HumanActivityReadRecorderSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Checks if gesture type is supported on a device.
             */
	isGestureSupported(type: GestureType): void
	/** 
 Adds a listener to be invoked when given gesture type is detected.
             */
	addGestureRecognitionListener(type: GestureType,listener: GestureRecognitionCallback,errorCallback: ErrorCallback,alwaysOn: boolean): void
	/** 
 Removes listener with the given id.
             */
	removeGestureRecognitionListener(watchId: long): void
	/** 
 Adds a listener to be invoked when returned value enters into defined range (the range is changed).
             */
	addStressMonitorChangeListener(ranges: StressMonitorDataRange[],listener: StressMonitorCallback): void
	/** 
 Removes listener with the given id.
             */
	removeStressMonitorChangeListener(watchId: long): void
}

interface StepDifference {
	/** 
 Count difference between the steps.
             */
	stepCountDifference: long;
	/** 
 Timestamp in seconds.
             */
	timestamp: long;
}

interface HumanActivityData {
}

interface HumanActivityPedometerData {
	/** 
 The current movement type.
             */
	stepStatus: PedometerStepStatus;
	/** 
 Current speed in km/h.
             */
	speed: double;
	/** 
 Step count per second.
             */
	walkingFrequency: double;
	/** 
 Cumulative distance traveled since the last  method call in meters.
             */
	cumulativeDistance: double;
	/** 
 Cumulative calories burnt since the last  method call in kcal.
             */
	cumulativeCalorie: double;
	/** 
 Cumulative walking and running step count since the last start() method call.
             */
	cumulativeTotalStepCount: double;
	/** 
 Cumulative walking step count since the last  method call.
             */
	cumulativeWalkStepCount: double;
	/** 
 Cumulative running step count since the last  method call.
             */
	cumulativeRunStepCount: double;
	/** 
 Accumulative distance traveled since the device boot in meters.
             */
	accumulativeDistance: double;
	/** 
 Accumulative calories burnt since the device boot in kcal.
             */
	accumulativeCalorie: double;
	/** 
 Accumulative walking and running step count since the device boot.
             */
	accumulativeTotalStepCount: double;
	/** 
 Accumulative walking step count since the device boot.
             */
	accumulativeWalkStepCount: double;
	/** 
 Accumulative running step count since the device boot.
             */
	accumulativeRunStepCount: double;
	/** 
 Array of the StepDifference.
             */
	stepCountDifferences: StepDifference[];
}

interface HumanActivityAccumulativePedometerData {
	/** 
 Current movement type.
             */
	stepStatus: PedometerStepStatus;
	/** 
 Current speed in km/h.
             */
	speed: double;
	/** 
 Step count per second.
             */
	walkingFrequency: double;
	/** 
 Accumulative distance traveled since the device boot in meters.
             */
	accumulativeDistance: double;
	/** 
 Accumulative calories burnt since the device boot in kcal.
             */
	accumulativeCalorie: double;
	/** 
 Accumulative walking and running step count since the device boot.
             */
	accumulativeTotalStepCount: double;
	/** 
 Accumulative walking step count since the device boot.
             */
	accumulativeWalkStepCount: double;
	/** 
 Accumulative running step count since the device boot.
             */
	accumulativeRunStepCount: double;
	/** 
 Array of the StepDifference.
             */
	stepCountDifferences: StepDifference[];
}

interface HumanActivityHRMData {
	/** 
 Heart rate in beats per minute.
When a user takes off the watch device, the heartRate is set to -3. When a user shakes the watch, the heartRate is set to -2.
             */
	heartRate: long;
	/** 
 Peak-to-peak interval in millisecond(s).
             */
	rRInterval: long;
}

interface HumanActivityGPSInfo {
	/** 
 An attribute to indicate the user's latitude in degrees.
             */
	latitude: double;
	/** 
 An attribute to indicate the user's longitude in degrees.
             */
	longitude: double;
	/** 
 An attribute to indicate the user's altitude in meters.
             */
	altitude: double;
	/** 
 An attribute to indicate the speed in km/h.
             */
	speed: double;
	/** 
 An attribute to indicate the error range of the user's position in meters.
             */
	errorRange: long;
	/** 
 An attribute to indicate timestamp in seconds.
             */
	timestamp: long;
}

interface HumanActivityGPSInfoArray {
	/** 
 An attribute to indicate the array of GPS information.
             */
	gpsInfo: HumanActivityGPSInfo[];
}

interface HumanActivitySleepMonitorData {
	/** 
 The sleep status.
             */
	status: SleepStatus;
	/** 
 The time when the sleep status is recognized. Epoch time in milliseconds.
             */
	timestamp: long long;
}

interface HumanActivitySleepDetectorData {
	/** 
 Sleep state (can be UNKNOWN, ASLEEP and AWAKE).
             */
	status: SleepStatus;
}

interface HumanActivityStressMonitorData {
	/** 
 Value returned from .
It's a .
             */
	stressScore: long;
}

interface HumanActivityRecognitionData {
	/** 
 The type of activity.
             */
	type: ActivityRecognitionType;
	/** 
 The time when the activity is recognized. Epoch time in seconds.
             */
	timestamp: long;
	/** 
 The degree of accuracy.
             */
	accuracy: ActivityAccuracy;
}

interface HumanActivityRecorderData {
	/** 
 Recording start time of the data in this HumanActivityRecorderData object. Epoch time in seconds.
             */
	startTime: long;
	/** 
 Recording end time of the data in this HumanActivityRecorderData object. Epoch time in seconds.
             */
	endTime: long;
}

interface HumanActivityRecorderPedometerData {
	/** 
 Distance traveled from  to  in meters.
             */
	distance: double;
	/** 
 Calories burnt from  to  in kcal.
             */
	calorie: double;
	/** 
 Walking and running step count from  to . The value is the sum of  and .
             */
	totalStepCount: double;
	/** 
 Walking step count from  to .
             */
	walkStepCount: double;
	/** 
 Running step count from  to .
             */
	runStepCount: double;
}

interface HumanActivityRecorderHRMData {
	/** 
 Heart rate in beats per minute.
             */
	heartRate: long;
}

interface HumanActivityRecorderSleepMonitorData {
	/** 
 The sleep status.
             */
	status: SleepStatus;
}

interface HumanActivityRecorderPressureData {
	/** 
 Max pressure in hectopascal (hPa).
             */
	max: double;
	/** 
 Min pressure in hectopascal (hPa).
             */
	min: double;
	/** 
 Average pressure in hectopascal (hPa).
             */
	average: double;
}

interface GestureData {
	/** 
 Identifier of gesture type.
             */
	type: GestureType;
	/** 
 Event type related to the detected gesture.
             */
	event: GestureEvent;
	/** 
 Time when gesture was detected. Epoch time in seconds.
             */
	timestamp: long;
	/** 
 Tilt degree on X-axis. It is used only for  type. For other gesture types it is set to null.
             */
	x: double;
	/** 
 Tilt degree on Y-axis. It is used only for  type. For other gesture types it is set to null.
             */
	y: double;
}

interface StressMonitorDataRange {
	/** 
 Name of range. Default value is "";
             */
	label: DOMString;
	/** 
 Minimum value of range. Default value is 0.
             */
	min: number;
	/** 
 Maximum value of range. If  is undefined it means that this value represents infinity. Default value is undefined.
             */
	max: number;
}

interface HumanActivityMonitorSuccessCallback {
	/** 
 Called when there is new human activity data available.
             */
	onsuccess(humanactivitydata: HumanActivityData): void
}

interface HumanActivityReadRecorderSuccessCallback {
	/** 
 Called when recorded human activity data is successfully read.
             */
	onsuccess(humanactivitydata: HumanActivityRecorderData[]): void
}

interface GestureRecognitionCallback {
	/** 
 Called when a gesture is detected.
             */
	onsuccess(data: GestureData): void
}

interface StressMonitorCallback {
	/** 
 Called when value returned from  is within registered range.
             */
	onsuccess(label: DOMString): void
}

interface InputDeviceManagerObject {
	/** 
 Object representing a input device manager.
             */
	inputdevice: InputDeviceManager;
}

interface InputDeviceKey {
	/** 
 The name of the key, for example  or .
             */
	name: InputDeviceKeyName;
	/** 
 The numeric code of the key, like  or .
             */
	code: long;
}

interface InputDeviceManager {
	/** 
 Retrieves the list of keys can be registered with the  method.
             */
	getSupportedKeys(): void
	/** 
 Returns information about the key which has the given name.
             */
	getKey(keyName: InputDeviceKeyName): InputDeviceKey
	/** 
 Registers an input device key to receive DOM keyboard event when it is pressed or released
             */
	registerKey(keyName: InputDeviceKeyName): void
	/** 
 Unregisters an input device key
             */
	unregisterKey(keyName: InputDeviceKeyName): void
	/** 
 Registers a batch of input device keys to receive DOM keyboard event when any of them is pressed or released
             */
	registerKeyBatch(keyNames: InputDeviceKeyName[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Unregisters a batch of input device keys
             */
	unregisterKeyBatch(keyNames: InputDeviceKeyName[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
}

interface IotconObject {
	/** 
 Object representing a exif manager.
             */
	iotcon: Iotcon;
}

interface Iotcon {
	/** 
 The device name of this application.
             */
	deviceName: DOMString;
	/** 
 Connects to the iotcon service. Call this function to start Iotcon.
             */
	initialize(filePath: DOMString): void
	/** 
 Returns object of  singleton, which provides methods for working with remote resources.
             */
	getClient(): Client
	/** 
 Returns the  object, which provides methods for managing resources on current device.
             */
	getServer(): Server
	/** 
 Returns the number of seconds set as the timeout threshold of asynchronous API.
             */
	getTimeout(): void
	/** 
 Sets the timeout value, in seconds, of asynchronous APIs.
             */
	setTimeout(timeout: long): void
	/** 
 Adds a listener to receive generated random pin from provisioning tool.
             */
	addGeneratedPinListener(successCallback: GeneratedPinCallback): void
	/** 
 Unregisters the listener and stops receiving generated random pin.
             */
	removeGeneratedPinListener(watchId: long): void
}

interface Client {
	/** 
 Finds resources using host address and resource type.
             */
	findResource(hostAddress: DOMString,query: Query,connectivityType: ConnectivityType,successCallback: FoundResourceSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Adds a listener to receive a presence events from the server.
A server sends presence events when starts or stops presence.
             */
	addPresenceEventListener(hostAddress: DOMString,resourceType: ResourceType,connectivityType: ConnectivityType,successCallback: PresenceEventCallback): void
	/** 
 Unregisters a presence event listener.
             */
	removePresenceEventListener(watchId: long): void
	/** 
 Gets the device information of remote server.
             */
	findDeviceInfo(hostAddress: DOMString,query: Query,connectivityType: ConnectivityType,successCallback: FoundDeviceInfoSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets the platform information of remote server.
             */
	findPlatformInfo(hostAddress: DOMString,query: Query,connectivityType: ConnectivityType,successCallback: FoundPlatformInfoSuccessCallback,errorCallback: ErrorCallback): void
}

interface Server {
	/** 
 Returns an array of resources which are registered on the server.
             */
	getResources(): void
	/** 
 Creates a resource and registers the resource on server.
             */
	createResource(uriPath: DOMString,resourceTypes: ResourceType[],resourceInterfaces: ResourceInterface[],listener: RequestCallback,policy: ResourcePolicy): Resource
	/** 
 Removes the resource and unregisters it from server.
             */
	removeResource(resource: Resource): void
	/** 
 Starts sending presence event of server. Server can send presence event to client when become online for the first time or come back from offline to online.
             */
	startPresence(timeToLive: number): void
	/** 
 Stops sending presence announcement of a server.
             */
	stopPresence(): void
}

interface RemoteResource {
	/** 
 The resource URI.
             */
	uriPath: DOMString;
	/** 
 It is connectivity type.
             */
	connectivityType: ConnectivityType;
	/** 
 The host address
             */
	hostAddress: DOMString;
	/** 
 A list of types in this resource
             */
	resourceTypes: ResourceType[];
	/** 
 A list of interfaces in the resource.
             */
	resourceInterfaces: ResourceInterface[];
	/** 
 Indicates if the resource is observable or not
             */
	isObservable: boolean;
	/** 
 Indicates if the resource is discoverable or not
             */
	isDiscoverable: boolean;
	/** 
 Indicates if the resource is initialized and activated or not
             */
	isActive: boolean;
	/** 
 Indicates if the resource takes some delay to respond or not
             */
	isSlow: boolean;
	/** 
 Indicates if the resource is secure or not
             */
	isSecure: boolean;
	/** 
 Indicates if the resource is allowed to be discovered only if discovery request contains an explicit query string or not
             */
	isExplicitDiscoverable: boolean;
	/** 
 The device unique id. this is unique per-server independent on how it was discovered.
             */
	deviceId: DOMString;
	/** 
 The device name of the remote resource.
             */
	deviceName: DOMString;
	/** 
 The option for managing vendor specific option of COAP packet.
             */
	options: IotconOption[];
	/** 
 The cached representation of remote resource.
             */
	cachedRepresentation: Representation;
	/** 
 The time interval in seconds for monitoring state (registered with setResourceStateChangeListener() ) and caching (registered with startCaching() ). Provided value must be in range from 1 to 3600 inclusive. The default value is  seconds.
             */
	timeInterval: long;
	/** 
 Gets the attributes of a resource.
             */
	methodGet(responseCallback: RemoteResourceResponseCallback,query: Query,errorCallback: ErrorCallback): void
	/** 
 Puts the representation of a resource for update.
             */
	methodPut(representation: Representation,responseCallback: RemoteResourceResponseCallback,query: Query,errorCallback: ErrorCallback): void
	/** 
 Posts the representation of a resource for create.
             */
	methodPost(representation: Representation,responseCallback: RemoteResourceResponseCallback,query: Query,errorCallback: ErrorCallback): void
	/** 
 Deletes the remote resource.
             */
	methodDelete(responseCallback: RemoteResourceResponseCallback,errorCallback: ErrorCallback): void
	/** 
 Sets the listener to receive notification about attribute change of remote resource. When server sends notification message, successCallback will be called.
             */
	startObserving(observePolicy: ObservePolicy,successCallback: RemoteResourceResponseCallback,query: Query): void
	/** 
 Unregisters the listener. so stop receiving notification about attribute change of remote resource.
             */
	stopObserving(): void
	/** 
 Starts caching of a remote resource. cached representation is updated when remote resource is changed.
             */
	startCaching(updatedCallback: CacheUpdatedCallback): void
	/** 
 Stops caching of a remote resource.
             */
	stopCaching(): void
	/** 
 Sets a listener to monitor the state of the remote resource.
             */
	setResourceStateChangeListener(successCallback: ResourceStateChangeCallback): void
	/** 
 Unsets the listener to stop monitoring the state of the remote resource.
             */
	unsetResourceStateChangeListener(): void
}

interface Resource {
	/** 
 The resource URI.
             */
	uriPath: DOMString;
	/** 
 A list of types in this resource.
             */
	resourceTypes: ResourceType[];
	/** 
 A list of interfaces in the resource.
             */
	resourceInterfaces: ResourceInterface[];
	/** 
 Indicates if the resource is observable or not
             */
	isObservable: boolean;
	/** 
 Indicates if the resource is discoverable or not
             */
	isDiscoverable: boolean;
	/** 
 Indicates if the resource is initialized and activated or not
             */
	isActive: boolean;
	/** 
 Indicates if the resource takes some delay to respond or not
             */
	isSlow: boolean;
	/** 
 Indicates if the resource is secure or not
             */
	isSecure: boolean;
	/** 
 Indicates if the resource is allowed to be discovered only if discovery request contains an explicit query string or not
             */
	isExplicitDiscoverable: boolean;
	/** 
 A list of children of this resource.
             */
	resources: Resource[];
	/** 
 A list of observation IDs of this resource.
             */
	observerIds: long[];
	/** 
 A lists of attributes of this resource.
             */
	attributes: object;
	/** 
 Notifies specific clients that resource's attributes have been changed.
             */
	notify(qosLevel: QosLevel,observerIds: long[]): void
	/** 
 Adds resource type to this resource.
             */
	addResourceTypes(types: ResourceType[]): void
	/** 
 Adds resource interface to this resource.
             */
	addResourceInterface(interface: ResourceInterface): void
	/** 
 Adds child resource into the parent resource.
             */
	addChildResource(resource: Resource): void
	/** 
 Removes child resource from the parent resource.
             */
	removeChildResource(resource: Resource): void
	/** 
 Sets the listener for request from client.
             */
	setRequestListener(listener: RequestCallback): void
	/** 
 Remove the listener.
             */
	unsetRequestListener(): void
}

interface Representation {
	/** 
 The resource URI.
             */
	uriPath: DOMString;
	/** 
 A list of types in this resource
             */
	resourceTypes: ResourceType[];
	/** 
 A list of interfaces in the resource.
             */
	resourceInterfaces: ResourceInterface[];
	/** 
 A lists of attribute in this resource.
             */
	attributes: object;
	/** 
 Representations belonging to this representation.
             */
	children: Representation[];
}

interface PresenceResponse {
	/** 
 The host address of the presence.
             */
	hostAddress: DOMString;
	/** 
 The connectivity type of the presence.
             */
	connectivityType: ConnectivityType;
	/** 
 The resource type of the presence.
             */
	resourceType: ResourceType;
	/** 
 The results type of presence.
             */
	resultType: PresenceResponseResultType;
	/** 
 The trigger type of presence. It is set only if a response result type is "OK".
             */
	triggerType: PresenceTriggerType;
}

interface IotconOption {
	/** 
 The ID of the option. id is always situated between 2048 and 3000.
             */
	id: number;
	/** 
 The string data to add. Length of data is less than or equal to 15.
             */
	data: DOMString;
}

interface Request {
	/** 
 The address of host of the request.
             */
	hostAddress: DOMString;
	/** 
 Connectivity type of connection.
             */
	connectivityType: ConnectivityType;
	/** 
 The request representation.
             */
	representation: Representation;
	/** 
 The option which was sent from client.
             */
	options: IotconOption[];
	/** 
 The query parameters from the request.
             */
	query: Query;
}

interface Response {
	/** 
 The request, that server responded.
             */
	request: Request;
	/** 
 The result indicates the detailed information about the result of the response to request.
             */
	result: ResponseResult;
	/** 
 The representation indicates the information of the resource.
             */
	representation: Representation;
	/** 
 The options indicates the vendor specific options of COAP packet.
             */
	options: IotconOption[];
	/** 
 Sends the response.
             */
	send(): void
}

interface RemoteResponse {
	/** 
 The result indicates the detailed information about the result of the response to request.
             */
	result: ResponseResult;
	/** 
 The representation indicates the information of the resource.
             */
	representation: Representation;
	/** 
 The options indicates the vendor specific options of COAP packet.
             */
	options: IotconOption[];
}

interface DeviceInfo {
	/** 
 The device name
             */
	deviceName: DOMString;
	/** 
 The version of core specification.
             */
	specVersion: DOMString;
	/** 
 The unique identifier for OIC device.
             */
	oicDeviceId: DOMString;
	/** 
 The version of specification which the device's data model is implemented
             */
	dataModelVersion: DOMString;
}

interface PlatformInfo {
	/** 
 The platform identifier
             */
	platformId: DOMString;
	/** 
 The name of manufacturer.
             */
	manufacturerName: DOMString;
	/** 
 The URL of manufacturer.
             */
	manufacturerUrl: DOMString;
	/** 
 The model number is designated by manufacturer.
             */
	modelNumber: DOMString;
	/** 
 The manufacture date of device.
             */
	manufactureDate: DOMString;
	/** 
 The platform version is defined by manufacturer.
             */
	platformVersion: DOMString;
	/** 
 The operating system version.
             */
	operatingSystemVersion: DOMString;
	/** 
 The hardware version.
             */
	hardwareVersion: DOMString;
	/** 
 The firmware version.
             */
	firmwareVersion: DOMString;
	/** 
 The URL that points to support information from manufacturer.
             */
	supportUrl: DOMString;
	/** 
 The System time.
             */
	systemTime: DOMString;
}

interface RequestCallback {
	/** 
 Called when GET request was received.
             */
	onget(request: Request): void
	/** 
 Called when PUT request was received.
             */
	onput(request: Request): void
	/** 
 Called when POST request was received.
             */
	onpost(request: Request): void
	/** 
 Called when DELETE request was received.
             */
	ondelete(request: Request): void
	/** 
 Called when OBSERVE request was received.
             */
	onobserving(request: Request,observeType: ObserveType,observeId: number): void
}

interface FoundResourceSuccessCallback {
	/** 
 Called when request was received.
             */
	onfound(remoteResource: RemoteResource): void
}

interface PresenceEventCallback {
	/** 
 Called when client receive presence events.
             */
	onreceived(presenceResponse: PresenceResponse): void
}

interface FoundDeviceInfoSuccessCallback {
	/** 
 Called when the device information is received.
             */
	onsuccess(info: DeviceInfo): void
}

interface FoundPlatformInfoSuccessCallback {
	/** 
 Called when the platform information is received.
             */
	onsuccess(info: PlatformInfo): void
}

interface RemoteResourceResponseCallback {
	/** 
 Called when the response is received.
             */
	onsuccess(response: RemoteResponse): void
}

interface ResourceStateChangeCallback {
	/** 
 Called when connection change appeared.
             */
	onchanged(isAlive: boolean): void
}

interface CacheUpdatedCallback {
	/** 
 Called when caching is successfully started.
             */
	onupdated(representation: Representation): void
}

interface GeneratedPinCallback {
	/** 
 Called when random pin is successfully generated.
             */
	onsuccess(pin: DOMString): void
}

interface KeyManagerObject {
	/** 
 Object representing a key manager.
             */
	keymanager: KeyManager;
}

interface KeyManager {
	/** 
 Saves and stores data as a  inside the KeyManager.
             */
	saveData(name: DOMString,rawData: RawData,password: DOMString,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Removes data from the KeyManager.
             */
	removeData(dataAlias: KeyManagerAlias): void
	/** 
 Gets raw data from the KeyManager.
             */
	getData(dataAlias: KeyManagerAlias,password: DOMString): RawData
	/** 
 Gets all the aliases which an application can access.
             */
	getDataAliasList(): void
	/** 
 Sets permissions that another application has for accessing an application's data.
             */
	setPermission(dataAlias: KeyManagerAlias,packageId: PackageId,permissionType: PermissionType,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
}

interface MediaControllerObject {
	/** 
 Object representing a media controller manager.
             */
	mediacontroller: MediaControllerManager;
}

interface MediaControllerManager {
	/** 
 Gets the client object. If not exist, client will be automatically created.
             */
	getClient(): MediaControllerClient
	/** 
 Creates the Server object which holds playback state, meta data
and is controlled by Client.
             */
	createServer(): MediaControllerServer
}

interface MediaControllerServer {
	/** 
 Current playback info.
             */
	playbackInfo: MediaControllerPlaybackInfo;
	/** 
 Current playback info.
             */
	playback: MediaControllerServerPlaybackInfo;
	/** 
 Object representing features related to playlists of a media controller server.
             */
	playlists: MediaControllerPlaylists;
	/** 
 Server icon URI.
             */
	iconURI: DOMString;
	/** 
 Abilities of the media controller server.
             */
	abilities: MediaControllerAbilities;
	/** 
 Object representing features related to subtitles control of a media controller server.
             */
	subtitles: MediaControllerSubtitles;
	/** 
 Object representing features related to spherical (360°) mode control of a media controller server.
             */
	mode360: MediaControllerMode360;
	/** 
 Object representing features related to display mode control of a media controller server.
             */
	displayMode: MediaControllerDisplayMode;
	/** 
 Object representing features related to display rotation control of a media controller server.
             */
	displayRotation: MediaControllerDisplayRotation;
	/** 
 Returns all existing clients info.
             */
	getAllClientsInfo(): void
	/** 
 Updates playback state and send notification to the listening clients.
See  to check
how to receive playback info changes from server on client side.
             */
	updatePlaybackState(state: MediaControllerPlaybackState): void
	/** 
 Updates server icon URI.
             */
	updateIconURI(iconURI: DOMString): void
	/** 
 Updates playback position and send notification to the listening clients.
             */
	updatePlaybackPosition(position: number): void
	/** 
 Sets content age rating for current playback item.
             */
	updatePlaybackAgeRating(rating: MediaControllerContentAgeRating): void
	/** 
 Sets content type for the current playback item.
             */
	updatePlaybackContentType(type: MediaControllerContentType): void
	/** 
 Updates shuffle mode and send notification to the listening clients.
             */
	updateShuffleMode(mode: boolean): void
	/** 
 Updates repeat mode and send notification to the listening clients.
             */
	updateRepeatMode(mode: boolean): void
	/** 
 Updates repeat state and sends notification to the listening clients.
             */
	updateRepeatState(state: MediaControllerRepeatState): void
	/** 
 Updates metadata and send notification to the listening clients.
             */
	updateMetadata(metadata: MediaControllerMetadata): void
	/** 
 Adds the listener for a media playback info requests from client.
See  to check how to send playback info change
requests from client.
             */
	addChangeRequestPlaybackInfoListener(listener: MediaControllerChangeRequestPlaybackInfoCallback): void
	/** 
 Removes the listener, so stop receiving playback state requests from clients.
             */
	removeChangeRequestPlaybackInfoListener(watchId: long): void
	/** 
 Sets the listener for receiving search requests from a client.
             */
	setSearchRequestListener(listener: MediaControllerSearchRequestCallback): void
	/** 
 Unsets search request listener.
             */
	unsetSearchRequestListener(): void
	/** 
 Adds the listener for receiving custom commands from client.
See  to check how to  from client.
             */
	addCommandListener(listener: MediaControllerReceiveCommandCallback): void
	/** 
 Removes the listener, so stop receiving custom commands from clients.
             */
	removeCommandListener(watchId: long): void
	/** 
 Creates  object.
             */
	createPlaylist(name: DOMString): MediaControllerPlaylist
	/** 
 Saves the playlist in a local database.
             */
	savePlaylist(playlist: MediaControllerPlaylist,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Deletes playlist from local database.
             */
	deletePlaylist(playlistName: DOMString,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Sets index and playlist name properties of playback info object.
             */
	updatePlaybackItem(playlistName: DOMString,index: DOMString): void
	/** 
 Retrieves all playlists from a local database.
             */
	getAllPlaylists(successCallback: MediaControllerGetAllPlaylistsSuccessCallback,errorCallback: ErrorCallback): void
}

interface MediaControllerClient {
	/** 
 Retrieves all activated media controller servers.
             */
	findServers(successCallback: MediaControllerServerInfoArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets the latest activated media controller server info.
             */
	getLatestServerInfo(): MediaControllerServerInfo
	/** 
 Adds a listener to be invoked when ability of the media controller server is changed.
             */
	addAbilityChangeListener(listener: MediaControllerAbilityChangeCallback): void
	/** 
 Removes selected .
             */
	removeAbilityChangeListener(watchId: long): void
	/** 
 Retrieves all subscribed media controller servers.
             */
	findSubscribedServers(successCallback: MediaControllerServerInfoArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Sets the media controller client's listener for custom events from the server.
             */
	setCustomEventListener(listener: MediaControllerReceiveCommandCallback): void
	/** 
 Removes the server's events listener.
             */
	unsetCustomEventListener(): void
}

interface MediaControllerServerInfo {
	/** 
 The appId of the media controller server.
             */
	name: ApplicationId;
	/** 
 State of the media controller server.
             */
	state: MediaControllerServerState;
	/** 
 Current playback info.
             */
	playbackInfo: MediaControllerPlaybackInfo;
	/** 
 Playback info of current server info.
             */
	playback: MediaControllerServerInfoPlaybackInfo;
	/** 
 An attribute providing access to the playlist information from the server.
             */
	playlists: MediaControllerPlaylistsInfo;
	/** 
 Server icon URI.
             */
	iconURI: DOMString;
	/** 
 Abilities of the media controller server.
             */
	abilities: MediaControllerAbilitiesInfo;
	/** 
 Object representing features related to subtitles control of a media controller server.
             */
	subtitles: MediaControllerSubtitlesInfo;
	/** 
 Object representing features related to spherical (360°) mode control of a media controller server.
             */
	mode360: MediaControllerMode360Info;
	/** 
 Object representing features related to display mode control of a media controller server.
             */
	displayMode: MediaControllerDisplayModeInfo;
	/** 
 Object representing features related to display rotation control of a media controller server.
             */
	displayRotation: MediaControllerDisplayRotationInfo;
	/** 
 Allows to change playback state of media controller server.
             */
	sendPlaybackState(state: MediaControllerPlaybackState,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Allows to change playback position of media controller server.
             */
	sendPlaybackPosition(position: number,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Allows to change shuffle mode of media controller server.
             */
	sendShuffleMode(mode: boolean,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Allows to change repeat mode of media controller server.
             */
	sendRepeatMode(mode: boolean,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Allows to change repeat state of media controller server.
             */
	sendRepeatState(state: MediaControllerRepeatState,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Sends a search request to the media controller server.
             */
	sendSearchRequest(request: SearchFilter[],replyCallback: MediaControllerSearchRequestReplyCallback,errorCallback: ErrorCallback): void
	/** 
 Allows to send custom command to media controller server.
             */
	sendCommand(command: DOMString,data: Bundle,successCallback: MediaControllerSendCommandSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Adds the listener for a media controller server status change.
             */
	addServerStatusChangeListener(listener: MediaControllerServerStatusChangeCallback): void
	/** 
 Removes the listener, so stop receiving notifications about media controller server status.
             */
	removeServerStatusChangeListener(watchId: long): void
	/** 
 Adds the listener for a media playback info changes.
             */
	addPlaybackInfoChangeListener(listener: MediaControllerPlaybackInfoChangeCallback): void
	/** 
 Removes the listener, so stop receiving notifications about media playback info changes.
             */
	removePlaybackInfoChangeListener(watchId: long): void
	/** 
 Retrieves all playlists saved in local database.
             */
	getAllPlaylists(successCallback: MediaControllerGetAllPlaylistsSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Requests setting new playback item to server.
             */
	sendPlaybackItem(playlistName: DOMString,index: DOMString,state: MediaControllerPlaybackState,position: number): void
	/** 
 Adds listener to be invoked when playlist is updated by server.
             */
	addPlaylistUpdatedListener(listener: MediaControllerPlaylistUpdatedCallback): void
	/** 
 Stops listening for playlist updates and removals.
             */
	removePlaylistUpdatedListener(listenerId: long): void
}

interface MediaControllerPlaybackInfo {
	/** 
 Current playback state.
             */
	state: MediaControllerPlaybackState;
	/** 
 Current playback position.
             */
	position: number;
	/** 
 Current playback age rating.
             */
	ageRating: MediaControllerContentAgeRating;
	/** 
 Current playback content type.
             */
	contentType: MediaControllerContentType;
	/** 
 Current shuffle mode.
             */
	shuffleMode: boolean;
	/** 
 Current repeat mode.
             */
	repeatMode: boolean;
	/** 
 Current repeat state.
             */
	repeatState: MediaControllerRepeatState;
	/** 
 Current playback metadata.
             */
	metadata: MediaControllerMetadata;
	/** 
 Current item index.
             */
	index: DOMString;
	/** 
 Current playlist name.
             */
	playlistName: DOMString;
}

interface MediaControllerServerPlaybackInfo {
	/** 
 Current playback state.
             */
	state: MediaControllerPlaybackState;
	/** 
 Current playback position.
             */
	position: number;
	/** 
 Current playback age rating.
             */
	ageRating: MediaControllerContentAgeRating;
	/** 
 Current playback content type.
             */
	contentType: MediaControllerContentType;
	/** 
 Current shuffle mode.
             */
	shuffleMode: boolean;
	/** 
 Current repeat state.
             */
	repeatState: MediaControllerRepeatState;
	/** 
 Current playback metadata.
             */
	metadata: MediaControllerMetadata;
	/** 
 Current item index.
             */
	index: DOMString;
	/** 
 Current playlist name.
             */
	playlistName: DOMString;
	/** 
 Sets index and playlist name properties of playback info object.
             */
	updatePlaybackItem(playlistName: DOMString,index: DOMString): void
	/** 
 Adds the listener for change requests of a media controller playback info.
             */
	addChangeRequestListener(listener: MediaControllerChangeRequestPlaybackInfoCallback): void
	/** 
 Removes the listener and stops receiving change requests of media controller playback info.
             */
	removeChangeRequestListener(watchId: long): void
}

interface MediaControllerServerInfoPlaybackInfo {
	/** 
 Current playback state.
             */
	state: MediaControllerPlaybackState;
	/** 
 Current playback position.
             */
	position: number;
	/** 
 Current playback age rating.
             */
	ageRating: MediaControllerContentAgeRating;
	/** 
 Current playback content type.
             */
	contentType: MediaControllerContentType;
	/** 
 Current shuffle mode.
             */
	shuffleMode: boolean;
	/** 
 Current repeat state.
             */
	repeatState: MediaControllerRepeatState;
	/** 
 Current playback metadata.
             */
	metadata: MediaControllerMetadata;
	/** 
 Current item index. Value set to  means no playlist set in playback
             */
	index: DOMString;
	/** 
 Current playlist name. Value set to  means no playlist set in playback
             */
	playlistName: DOMString;
	/** 
 Sends request to change the playback state of a media controller server.
             */
	sendPlaybackAction(action: MediaControllerPlaybackAction,replyCallback: MediaControllerSendCommandSuccessCallback): void
	/** 
 Sends request to change the playback position of a media controller server.
             */
	sendPlaybackPosition(position: number,replyCallback: MediaControllerSendCommandSuccessCallback): void
	/** 
 Sends request to change the shuffle mode of a media controller server.
             */
	sendShuffleMode(mode: boolean,replyCallback: MediaControllerSendCommandSuccessCallback): void
	/** 
 Sends request to change the repeat state of a media controller server.
             */
	sendRepeatState(state: MediaControllerRepeatState,replyCallback: MediaControllerSendCommandSuccessCallback): void
	/** 
 Adds the listener for a media playback info changes.
             */
	addPlaybackInfoChangeListener(listener: MediaControllerPlaybackInfoChangeCallback): void
	/** 
 Removes the listener, so stop receiving notifications about media playback info changes.
             */
	removePlaybackInfoChangeListener(watchId: long): void
}

interface MediaControllerPlaylists {
	/** 
 Creates  object.
             */
	createPlaylist(name: DOMString): MediaControllerPlaylist
	/** 
 Saves the playlist in a local database.
             */
	savePlaylist(playlist: MediaControllerPlaylist,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Deletes the playlist from a local database.
             */
	deletePlaylist(playlistName: DOMString,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Retrieves all playlists from a local database.
             */
	getAllPlaylists(successCallback: MediaControllerGetAllPlaylistsSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Returns the playlist with the given name.
             */
	getPlaylist(playlistName: DOMString): MediaControllerPlaylist
}

interface MediaControllerPlaylistsInfo {
	/** 
 Retrieves all playlists saved in local database.
             */
	getAllPlaylists(successCallback: MediaControllerGetAllPlaylistsSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Asks the server to set a new playback item.
             */
	sendPlaybackItem(playlistName: DOMString,index: DOMString,action: MediaControllerPlaybackAction,position: number,replyCallback: MediaControllerSendCommandSuccessCallback): void
	/** 
 Adds listener to be invoked when playlist is updated by server.
             */
	addPlaylistUpdatedListener(listener: MediaControllerPlaylistUpdatedCallback): void
	/** 
 Stops listening for playlist updates.
             */
	removePlaylistUpdatedListener(listenerId: long): void
	/** 
 Returns the playlist with the given name.
             */
	getPlaylist(playlistName: DOMString): MediaControllerPlaylist
}

interface MediaControllerAbilities {
	/** 
 Represents abilities of server's playback actions.
             */
	playback: MediaControllerPlaybackAbilities;
	/** 
 Represents abilities of server's display modes.
             */
	displayMode: MediaControllerDisplayModeAbilities;
	/** 
 Represents display orientations supported by the media controller server.
             */
	displayRotation: MediaControllerDisplayRotationAbilities;
	/** 
 Represents server's ability to change playback position.
             */
	playbackPosition: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to change shuffle mode.
             */
	shuffle: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to change repeat state.
             */
	repeat: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to add/change/remove playlists.
             */
	playlist: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to receive custom commands from the media controller client.
             */
	clientCustom: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to receive search requests from the media controller client.
             */
	search: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to receive requests for subtitles mode change from the media controller client.
             */
	subtitles: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to receive requests for spherical (360°) mode change from the media controller client.
             */
	mode360: MediaControllerAbilitySupport;
}

interface MediaControllerPlaybackAbilities {
	/** 
 Represents server's ability to perform  action.
             */
	play: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	pause: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	stop: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	next: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	prev: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	forward: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	rewind: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	togglePlayPause: MediaControllerAbilitySupport;
	/** 
 Saves the current state of playback abilities to the database.
             */
	saveAbilities(): void
}

interface MediaControllerDisplayModeAbilities {
	/** 
 Represents server's ability to set  mode.
             */
	letterBox: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to set  mode.
             */
	originSize: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to set  mode.
             */
	fullScreen: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to set  mode.
             */
	croppedFull: MediaControllerAbilitySupport;
}

interface MediaControllerDisplayRotationAbilities {
	/** 
 Represents the server's ability to set 0° display orientation.
             */
	rotationNone: MediaControllerAbilitySupport;
	/** 
 Represents the server's ability to set 90° display orientation.
             */
	rotation90: MediaControllerAbilitySupport;
	/** 
 Represents the server's ability to set 180° display orientation.
             */
	rotation180: MediaControllerAbilitySupport;
	/** 
 Represents the server's ability to set 270° display orientation.
             */
	rotation270: MediaControllerAbilitySupport;
}

interface MediaControllerAbilitiesInfo {
	/** 
 Represents abilities of server's playback actions.
             */
	playback: MediaControllerPlaybackAbilitiesInfo;
	/** 
 Represents abilities of server's display modes.
             */
	displayMode: MediaControllerDisplayModeAbilitiesInfo;
	/** 
 Represents server abilities of setting display orientations.
             */
	displayRotation: MediaControllerDisplayRotationAbilitiesInfo;
	/** 
 Represents server's ability to change playback position.
             */
	playbackPosition: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to change shuffle mode.
             */
	shuffle: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to change repeat state.
             */
	repeat: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to add/change/remove playlists.
             */
	playlist: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to receive custom commands from media controller client.
             */
	clientCustom: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to receive search requests from media controller client.
             */
	search: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to receive requests for subtitles mode change from media controller client.
             */
	subtitles: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to receive requests for spherical (360°) mode change from media controller client.
             */
	mode360: MediaControllerAbilitySupport;
	/** 
 Adds a subscription for monitoring status of all abilities of server represented by this object.
             */
	subscribe(): void
	/** 
 Removes a subscription for monitoring status of all abilities of server represented by this object.
             */
	unsubscribe(): void
}

interface MediaControllerPlaybackAbilitiesInfo {
	/** 
 Represents server's ability to perform  action.
             */
	play: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	pause: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	stop: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	next: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	prev: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	forward: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	rewind: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to perform  action.
             */
	togglePlayPause: MediaControllerAbilitySupport;
}

interface MediaControllerDisplayModeAbilitiesInfo {
	/** 
 Represents server's ability to set  mode.
             */
	letterBox: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to set  mode.
             */
	originSize: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to set  mode.
             */
	fullScreen: MediaControllerAbilitySupport;
	/** 
 Represents server's ability to set  mode.
             */
	croppedFull: MediaControllerAbilitySupport;
}

interface MediaControllerDisplayRotationAbilitiesInfo {
	/** 
 Represents the server's ability to set 0° display orientation.
             */
	rotationNone: MediaControllerAbilitySupport;
	/** 
 Represents the server's ability to set 90° display orientation.
             */
	rotation90: MediaControllerAbilitySupport;
	/** 
 Represents the server's ability to set 180° display orientation.
             */
	rotation180: MediaControllerAbilitySupport;
	/** 
 Represents the server's ability to set 270° display orientation.
             */
	rotation270: MediaControllerAbilitySupport;
}

interface MediaControllerSubtitles {
	/** 
 State of subtitles mode on the server. Default value for a newly created server is .
             */
	enabled: boolean;
	/** 
 Adds the listener for change requests of a media controller subtitles mode.
             */
	addChangeRequestListener(listener: MediaControllerEnabledChangeRequestCallback): void
	/** 
 Removes the listener and stops receiving change requests of media controller subtitles mode.
             */
	removeChangeRequestListener(watchId: long): void
}

interface MediaControllerSubtitlesInfo {
	/** 
 State of subtitles mode on the server represented by this object.
             */
	enabled: boolean;
	/** 
 Allows to send change requests for subtitles mode to media controller server.
             */
	sendRequest(enabled: boolean,replyCallback: MediaControllerSendCommandSuccessCallback): void
	/** 
 Adds the listener for changes of a media controller subtitles mode of a media controller server.
             */
	addModeChangeListener(listener: MediaControllerEnabledChangeCallback): void
	/** 
 Removes the listener, so stop receiving notifications about media controller server subtitles mode changes.
             */
	removeModeChangeListener(watchId: long): void
}

interface MediaControllerMode360 {
	/** 
 State of spherical (360°) mode on the server. Default value for a newly created server is .
             */
	enabled: boolean;
	/** 
 Adds the listener for change requests of a media controller spherical (360°) mode.
             */
	addChangeRequestListener(listener: MediaControllerEnabledChangeRequestCallback): void
	/** 
 Removes the listener and stops receiving change requests of media controller spherical (360°) mode.
             */
	removeChangeRequestListener(watchId: long): void
}

interface MediaControllerMode360Info {
	/** 
 State of spherical (360°) mode on the server represented by this object.
             */
	enabled: boolean;
	/** 
 Allows to send change requests for spherical (360°) mode to media controller server.
             */
	sendRequest(enabled: boolean,replyCallback: MediaControllerSendCommandSuccessCallback): void
	/** 
 Adds the listener for changes of a media controller spherical (360°) mode of a media controller server.
             */
	addModeChangeListener(listener: MediaControllerEnabledChangeCallback): void
	/** 
 Removes the listener, so stop receiving notifications about media controller server spherical (360°) mode changes.
             */
	removeModeChangeListener(watchId: long): void
}

interface MediaControllerDisplayMode {
	/** 
 Type of display mode on the server. Default value for a newly created server is .
             */
	type: MediaControllerDisplayModeType;
	/** 
 Adds the listener for change requests of the media controller display mode.
             */
	addChangeRequestListener(listener: MediaControllerDisplayModeChangeRequestCallback): void
	/** 
 Removes the listener and stops receiving change requests of media controller display mode.
             */
	removeChangeRequestListener(watchId: long): void
}

interface MediaControllerDisplayModeInfo {
	/** 
 Type of display mode on the server represented by this object.
             */
	type: MediaControllerDisplayModeType;
	/** 
 Allows to send change requests for display mode to media controller server.
             */
	sendRequest(type: MediaControllerDisplayModeType,replyCallback: MediaControllerSendCommandSuccessCallback): void
	/** 
 Adds the listener for changes of a media controller display mode of a media controller server.
             */
	addModeChangeListener(listener: MediaControllerDisplayModeChangeCallback): void
	/** 
 Removes the listener, so stop receiving notifications about media controller server display mode changes.
             */
	removeModeChangeListener(watchId: long): void
}

interface MediaControllerDisplayRotation {
	/** 
 State of display rotation on the server. Default value for a newly created server is .
             */
	displayRotation: MediaControllerDisplayRotationType;
	/** 
 Adds the listener for change requests of a media controller display rotation.
             */
	addChangeRequestListener(listener: MediaControllerDisplayRotationChangeRequestCallback): void
	/** 
 Removes the listener and stops receiving change requests of media controller display rotation.
             */
	removeChangeRequestListener(watchId: long): void
}

interface MediaControllerClientInfo {
	/** 
 Id of the client application.
             */
	name: ApplicationId;
	/** 
 Sends an event to the client.
             */
	sendEvent(eventName: DOMString,data: Bundle,successCallback: MediaControllerSendCommandSuccessCallback): void
}

interface MediaControllerDisplayRotationInfo {
	/** 
 State of display rotation on the server represented by this object.
             */
	displayRotation: MediaControllerDisplayRotationType;
	/** 
 Allows to send change requests for display rotation change to a media controller server.
             */
	sendRequest(displayRotation: MediaControllerDisplayRotationType,replyCallback: MediaControllerSendCommandSuccessCallback): void
	/** 
 Adds the listener for changes of a display rotation of a media controller server.
             */
	addDisplayRotationChangeListener(listener: MediaControllerDisplayRotationChangeCallback): void
	/** 
 Removes the listener, so stop receiving notifications about media controller server display rotation changes.
             */
	removeDisplayRotationChangeListener(watchId: long): void
}

interface MediaControllerMetadata {
	/** 
 Media title.
             */
	title: DOMString;
	/** 
 Media artist.
             */
	artist: DOMString;
	/** 
 Media album.
             */
	album: DOMString;
	/** 
 Media author.
             */
	author: DOMString;
	/** 
 Media genre.
             */
	genre: DOMString;
	/** 
 Media duration.
             */
	duration: DOMString;
	/** 
 Media date.
             */
	date: DOMString;
	/** 
 Media copyright.
             */
	copyright: DOMString;
	/** 
 Media description.
             */
	description: DOMString;
	/** 
 Media track number.
             */
	trackNum: DOMString;
	/** 
 Media picture.
             */
	picture: DOMString;
	/** 
 Season number. Default value is 0.
             */
	seasonNumber: long;
	/** 
 Season title. Default value is .
             */
	seasonTitle: DOMString;
	/** 
 Episode number. Default value is 0.
             */
	episodeNumber: long;
	/** 
 Episode title. Default value is .
             */
	episodeTitle: DOMString;
	/** 
 Resolution width. Default value is 0. It cannot be changed to less than 0. Setting inappropriate values has no effect on the attribute.
             */
	resolutionWidth: long;
	/** 
 Resolution height. Default value is 0. It cannot be changed to less than 0. Setting inappropriate values has no effect on the attribute.
             */
	resolutionHeight: long;
	/** 
 Saves current state of metadata to the database and sends notification to the listening clients.
             */
	save(): void
}

interface MediaControllerPlaylistItem {
	/** 
 Index of playlist's item. Should be unique within playlist.
             */
	index: DOMString;
	/** 
 Metadata associated with item.
             */
	metadata: MediaControllerMetadata;
}

interface MediaControllerPlaylist {
	/** 
 Name of this playlist.
             */
	name: DOMString;
	/** 
 Adds new item to the playlist.
             */
	addItem(index: DOMString,metadata: MediaControllerMetadataInit): void
	/** 
 Gets all items from playlist.
             */
	getItems(successCallback: MediaControllerGetItemsSuccessCallback,errorCallback: ErrorCallback): void
}

interface SearchFilter {
	/** 
 Specifies filter's content type parameter.
             */
	contentType: MediaControllerContentType;
	/** 
 Specifies filter's search category parameter.
             */
	category: MediaControllerSearchCategory;
	/** 
 Specifies filter's search keyword parameter.
             */
	keyword: DOMString;
	/** 
 Additional application-dependent search parameters.
             */
	extraData: Bundle;
}

interface MediaControllerServerInfoArraySuccessCallback {
	/** 
 Called when all registered media controller servers found.
             */
	onsuccess(servers: MediaControllerServerInfo[]): void
}

interface MediaControllerSendCommandSuccessCallback {
	/** 
 Called when a response to the request is received.
             */
	onsuccess(data: object,code: long): void
}

interface RequestReply {
	/** 
 Response data bundle.
             */
	data: Bundle;
	/** 
 Response status code.
             */
	code: long;
}

interface MediaControllerSearchRequestReplyCallback {
	/** 
 Function called when search request has been processed.
             */
	onreply(reply: RequestReply): void
}

interface MediaControllerSearchRequestCallback {
	/** 
 Function called on the server when it receives a search request from a client.
             */
	onrequest(clientName: ApplicationId,request: SearchFilter[]): RequestReply
}

interface MediaControllerReceiveCommandCallback {
	/** 
 Called when custom command is received by the server or custom event is received by the client.
             */
	onsuccess(senderAppName: ApplicationId,command: DOMString,data: object): RequestReply
}

interface MediaControllerEnabledChangeRequestCallback {
	/** 
 Called when change request is received from client.
             */
	onreply(clientName: ApplicationId,enabled: boolean): RequestReply
}

interface MediaControllerEnabledChangeCallback {
	/** 
 Called when server's attribute is changed.
             */
	onchange(enabled: boolean): void
}

interface MediaControllerDisplayModeChangeRequestCallback {
	/** 
 Called when change request is received from client.
             */
	onreply(clientName: ApplicationId,mode: MediaControllerDisplayModeType): RequestReply
}

interface MediaControllerDisplayModeChangeCallback {
	/** 
 Called when server's display mode is changed.
             */
	onchange(mode: MediaControllerDisplayModeType): void
}

interface MediaControllerDisplayRotationChangeRequestCallback {
	/** 
 Called when change request is received from a client.
             */
	onreply(clientName: ApplicationId,displayRotation: MediaControllerDisplayRotationType): RequestReply
}

interface MediaControllerDisplayRotationChangeCallback {
	/** 
 Called when display rotation is changed.
             */
	onchange(displayRotation: MediaControllerDisplayRotationType): void
}

interface MediaControllerServerStatusChangeCallback {
	/** 
 Called when server status changed.
             */
	onsuccess(status: MediaControllerServerState): void
}

interface MediaControllerPlaybackInfoChangeCallback {
	/** 
 Called when playback state or position is changed.
             */
	onplaybackchanged(state: MediaControllerPlaybackState,position: number): void
	/** 
 Called when shuffle mode is changed.
             */
	onshufflemodechanged(mode: boolean): void
	/** 
 Called when repeat mode is changed.
             */
	onrepeatmodechanged(mode: boolean): void
	/** 
 Called when repeat state is changed.
             */
	onrepeatstatechanged(state: MediaControllerRepeatState): void
	/** 
 Called when playback metadata is changed.
             */
	onmetadatachanged(metadata: MediaControllerMetadata): void
}

interface MediaControllerChangeRequestPlaybackInfoCallback {
	/** 
 Called when client requested playback state changes.
             */
	onplaybackstaterequest(state: MediaControllerPlaybackState,clientName: ApplicationId): void
	/** 
 Called when a client requested the playback state changes by sending the playback action.
             */
	onplaybackactionrequest(action: MediaControllerPlaybackAction,clientName: ApplicationId): RequestReply
	/** 
 Called when client requested playback position changes.
             */
	onplaybackpositionrequest(position: number,clientName: ApplicationId): RequestReply
	/** 
 Called when client requested shuffle mode changes.
             */
	onshufflemoderequest(mode: boolean,clientName: ApplicationId): RequestReply
	/** 
 Called when client requested repeat mode changes.
             */
	onrepeatmoderequest(mode: boolean,clientName: ApplicationId): void
	/** 
 Called when client requested change of repeat state.
             */
	onrepeatstaterequest(state: MediaControllerRepeatState,clientName: ApplicationId): RequestReply
	/** 
 Called when client request change of playback item.
             */
	onplaybackitemrequest(playlistName: DOMString,index: DOMString,action: MediaControllerPlaybackAction,position: number,clientName: ApplicationId): RequestReply
}

interface MediaControllerGetAllPlaylistsSuccessCallback {
	/** 
 Success callback for  function.
             */
	onsuccess(playlists: MediaControllerPlaylist[]): void
}

interface MediaControllerPlaylistUpdatedCallback {
	/** 
 Event triggered when playlist is updated in database.
             */
	onplaylistupdated(serverName: DOMString,playlist: MediaControllerPlaylist): void
	/** 
 Event triggered when playlist is removed from database.
             */
	onplaylistdeleted(serverName: DOMString,playlistName: DOMString): void
}

interface MediaControllerGetItemsSuccessCallback {
	/** 
 Success callback for  function.
             */
	onsuccess(items: MediaControllerPlaylistItem[]): void
}

interface MediaControllerAbilityChangeCallback {
	/** 
 Event triggered when server's playback ability is updated.
             */
	onplaybackabilitychanged(server: MediaControllerServerInfo,abilities: MediaControllerPlaybackAbilitiesInfo): void
	/** 
 Event triggered when server's display mode ability is updated.
             */
	ondisplaymodeabilitychanged(server: MediaControllerServerInfo,abilities: MediaControllerDisplayModeAbilitiesInfo): void
	/** 
 Event triggered when server's display rotation is updated.
             */
	ondisplayrotationabilitychanged(server: MediaControllerServerInfo,abilities: MediaControllerDisplayRotationAbilitiesInfo): void
	/** 
 Event triggered when server's simple ability is updated.
             */
	onsimpleabilitychanged(server: MediaControllerServerInfo,type: MediaControllerSimpleAbility,support: MediaControllerAbilitySupport): void
}

interface MediaKeyManagerObject {
	/** 
 Object representing a media key manager.
             */
	mediakey: MediaKeyManager;
}

interface MediaKeyManager {
	/** 
 Registers a listener to be called when a media key is pressed or released.
             */
	setMediaKeyEventListener(callback: MediaKeyEventCallback): void
	/** 
 Unsubscribes from receiving notification for detecting the media key event.
             */
	unsetMediaKeyEventListener(): void
}

interface MediaKeyEventCallback {
	/** 
 Called when a media key has been pressed.
             */
	onpressed(type: MediaKeyType): void
	/** 
 Called when a media key has been released.
             */
	onreleased(type: MediaKeyType): void
}

interface MessagePortManagerObject {
	/** 
 Object representing a exif manager.
             */
	messageport: MessagePortManager;
}

interface MessagePortManager {
	/** 
 Requests a LocalMessagePort instance to start receiving message from another application.
             */
	requestLocalMessagePort(localMessagePortName: DOMString): LocalMessagePort
	/** 
 Requests a trusted LocalMessagePort instance to receive message from another application.
             */
	requestTrustedLocalMessagePort(localMessagePortName: DOMString): LocalMessagePort
	/** 
 Requests a RemoteMessagePort instance to send message to another application.
             */
	requestRemoteMessagePort(appId: ApplicationId,remoteMessagePortName: DOMString): RemoteMessagePort
	/** 
 Requests a trusted RemoteMessagePort instance to receive message from another application.
             */
	requestTrustedRemoteMessagePort(appId: ApplicationId,remoteMessagePortName: DOMString): RemoteMessagePort
}

interface LocalMessagePort {
	/** 
 The name of the message port name.
             */
	messagePortName: DOMString;
	/** 
 The flag indicating whether the message port is trusted.
             */
	isTrusted: boolean;
	/** 
 Adds a message port listener to receive messages from other applications.
             */
	addMessagePortListener(listener: MessagePortCallback): void
	/** 
 Removes the message port listener.
             */
	removeMessagePortListener(watchId: long): void
}

interface RemoteMessagePort {
	/** 
 The message port name.
             */
	messagePortName: DOMString;
	/** 
 The application ID to connect with.
             */
	appId: ApplicationId;
	/** 
 The flag indicating whether the message port is trusted.
             */
	isTrusted: boolean;
	/** 
 Sends messages to the specified application.
             */
	sendMessage(data: MessagePortDataItem[],localMessagePort: LocalMessagePort): void
}

interface MessagePortCallback {
	/** 
 Called when data is received from other applications via the specified message port name.
             */
	onreceived(data: MessagePortDataItem[],remoteMessagePort: RemoteMessagePort): void
}

interface MessageManagerObject {
	/** 
 Object representing a messaging manager.
             */
	messaging: Messaging;
}

interface Message {
	/** 
 The message identifier.
             */
	id: MessageId;
	/** 
 The identifier of the conversation to which the message belongs.
             */
	conversationId: MessageConvId;
	/** 
 The identifier of the folder to which the message belongs.
             */
	folderId: MessageFolderId;
	/** 
 The type of a given message.
             */
	type: MessageServiceTag;
	/** 
 The timestamp of a message.
             */
	timestamp: Date;
	/** 
 The source address (or source phone number) of a message.
             */
	from: DOMString;
	/** 
 The destination of a message.
             */
	to: DOMString[];
	/** 
 The carbon copy address of a message.
             */
	cc: DOMString[];
	/** 
 The blind carbon copy (bcc) address of a message.
             */
	bcc: DOMString[];
	/** 
 The body of a message.
             */
	body: MessageBody;
	/** 
 The flag indicating the read state for a message.
             */
	isRead: boolean;
	/** 
 The flag indicating whether an attachment(s) exists.
             */
	hasAttachment: boolean;
	/** 
 The flag indicating the priority of a message.
             */
	isHighPriority: boolean;
	/** 
 The subject of a message.
             */
	subject: DOMString;
	/** 
 The original message.
             */
	inResponseTo: MessageId;
	/** 
 The status of a given message.
             */
	messageStatus: DOMString;
	/** 
 The list of the message attachments.
             */
	attachments: MessageAttachment[];
}

interface MessageBody {
	/** 
 The ID of a parent message.
             */
	messageId: MessageId;
	/** 
 The flag indicating whether the message body has been loaded.
             */
	loaded: boolean;
	/** 
 The plain text representation of a message body.
             */
	plainBody: DOMString;
	/** 
 The HTML representation of a message body.
             */
	htmlBody: DOMString;
	/** 
 The list of the inline attachments.
             */
	inlineAttachments: MessageAttachment[];
}

interface MessageAttachment {
	/** 
 The ID of an attachment.
             */
	id: MessageAttachmentId;
	/** 
 The ID of a parent message.
             */
	messageId: MessageId;
	/** 
 The attachment MIME type.
             */
	mimeType: DOMString;
	/** 
 The location path to a loaded attachment file.
             */
	filePath: DOMString;
}

interface Messaging {
	/** 
 Gets the messaging service of a given type for a given account, or all existing services supporting the given type, if  is not given.
             */
	getMessageServices(messageServiceType: MessageServiceTag,successCallback: MessageServiceArraySuccessCallback,errorCallback: ErrorCallback): void
}

interface MessageServiceArraySuccessCallback {
	/** 
 Called when finding message services is successful.
             */
	onsuccess(services: MessageService[]): void
}

interface MessageService {
	/** 
 The unique identifier of this Messaging service.
             */
	id: DOMString;
	/** 
 The tag supported by this messaging service.
             */
	type: MessageServiceTag;
	/** 
 The messaging service name taken from the messaging service.
             */
	name: DOMString;
	/** 
 The  for this messaging service.
             */
	messageStorage: MessageStorage;
	/** 
 Sends a specified message.
             */
	sendMessage(message: Message,successCallback: MessageRecipientsCallback,errorCallback: ErrorCallback,simIndex: long): void
	/** 
 Loads the body for a specified message.
             */
	loadMessageBody(message: Message,successCallback: MessageBodySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Loads a specified message attachment.
             */
	loadMessageAttachment(attachment: MessageAttachment,successCallback: MessageAttachmentSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Synchronizes the service content with an external mail server.
             */
	sync(successCallback: SuccessCallback,errorCallback: ErrorCallback,limit: number): void
	/** 
 Synchronizes the folder content with an external mail server.
             */
	syncFolder(folder: MessageFolder,successCallback: SuccessCallback,errorCallback: ErrorCallback,limit: number): void
	/** 
 Stops sync() and syncFoler() operation.
             */
	stopSync(opId: long): void
}

interface MessageRecipientsCallback {
	/** 
 Called when the message sending is finished.
             */
	onsuccess(recipients: DOMString[]): void
}

interface MessageBodySuccessCallback {
	/** 
 Called when the asynchronous query completes successfully.
             */
	onsuccess(message: Message): void
}

interface MessageAttachmentSuccessCallback {
	/** 
 Called when the asynchronous query completes successfully.
             */
	onsuccess(attachment: MessageAttachment): void
}

interface MessageStorage {
	/** 
 Adds a draft message to  and these messages are stored in the Drafts folder.
             */
	addDraftMessage(message: Message,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Finds messages from .
             */
	findMessages(filter: AbstractFilter,successCallback: MessageArraySuccessCallback,errorCallback: ErrorCallback,sort: SortMode,limit: number,offset: number): void
	/** 
 Removes messages from .
             */
	removeMessages(messages: Message[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Updates messages in .
             */
	updateMessages(messages: Message[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Finds conversations from .
             */
	findConversations(filter: AbstractFilter,successCallback: MessageConversationArraySuccessCallback,errorCallback: ErrorCallback,sort: SortMode,limit: number,offset: number): void
	/** 
 Removes conversations from .
             */
	removeConversations(conversations: MessageConversation[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Queries folders from MessageStorage.
             */
	findFolders(filter: AbstractFilter,successCallback: MessageFolderArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Adds a listener to subscribe to notification for MessageStorage changes.
             */
	addMessagesChangeListener(messagesChangeCallback: MessagesChangeCallback,filter: AbstractFilter): void
	/** 
 Adds a listener to subscribe to notifications for MessageConversation changes.
             */
	addConversationsChangeListener(conversationsChangeCallback: MessageConversationsChangeCallback,filter: AbstractFilter): void
	/** 
 Adds a listener to subscribe to notifications for MessageFolder changes.
             */
	addFoldersChangeListener(foldersChangeCallback: MessageFoldersChangeCallback,filter: AbstractFilter): void
	/** 
 Removes a listener to unsubscribe from receiving message notifications.
             */
	removeChangeListener(watchId: long): void
}

interface MessageArraySuccessCallback {
	/** 
 Called when an asynchronous query completes successfully.
             */
	onsuccess(messages: Message[]): void
}

interface MessageConversationArraySuccessCallback {
	/** 
 Called when an asynchronous query completes successfully.
             */
	onsuccess(conversations: MessageConversation[]): void
}

interface MessageFolderArraySuccessCallback {
	/** 
 Called when an asynchronous query completes successfully.
             */
	onsuccess(folders: MessageFolder[]): void
}

interface MessagesChangeCallback {
	/** 
 Called when messages are added to the MessageStorage.
             */
	messagesadded(addedMessages: Message[]): void
	/** 
 Called when messages are updated in MessageStorage.
             */
	messagesupdated(updatedMessages: Message[]): void
	/** 
 Called when messages are removed from MessageStorage.
             */
	messagesremoved(removedMessages: Message[]): void
}

interface MessageConversationsChangeCallback {
	/** 
 Called when conversations are added to MessageStorage.
             */
	conversationsadded(addedConversations: MessageConversation[]): void
	/** 
 Called when conversations are updated in MessageStorage.
             */
	conversationsupdated(updatedConversations: MessageConversation[]): void
	/** 
 Called when conversations are removed from MessageStorage.
             */
	conversationsremoved(removedConversations: MessageConversation[]): void
}

interface MessageFoldersChangeCallback {
	/** 
 Called when folders are added to .
             */
	foldersadded(addedFolders: MessageFolder[]): void
	/** 
 Called when folders are updated in .
             */
	foldersupdated(updatedFolders: MessageFolder[]): void
	/** 
 Called when folders are removed from .
             */
	foldersremoved(removedFolders: MessageFolder[]): void
}

interface MessageConversation {
	/** 
 The conversation identifier.
             */
	id: MessageConvId;
	/** 
 The type of a given conversation.
             */
	type: MessageServiceTag;
	/** 
 The timestamp of the latest message in a conversation.
             */
	timestamp: Date;
	/** 
 The count of messages in a conversation.
             */
	messageCount: number;
	/** 
 The count of unread messages in a conversation.
             */
	unreadMessages: number;
	/** 
 A preview of the latest message in a conversation.
             */
	preview: DOMString;
	/** 
 The subject of a conversation (applicable for group chats, MMS, email).
             */
	subject: DOMString;
	/** 
 The flag indicating whether the latest message in a conversation has been read.
             */
	isRead: boolean;
	/** 
 The source address (or source phone number) of the latest message in the conversation.
             */
	from: DOMString;
	/** 
 The destination of the latest message in a conversation.
             */
	to: DOMString[];
	/** 
 The carbon copy (cc) address of the latest message in a conversation.
             */
	cc: DOMString[];
	/** 
 The blind carbon copy (bcc) address of the latest message in a conversation.
             */
	bcc: DOMString[];
	/** 
 The identifier of a latest message in a conversation.
             */
	lastMessageId: MessageId;
}

interface MessageFolder {
	/** 
 The folder identifier.
The ID is locally unique and persistent property, assigned by the device or the Web runtime (WRT).
             */
	id: MessageFolderId;
	/** 
 The identifier for the parent folder of a specified folder.
             */
	parentId: MessageFolderId;
	/** 
 The identifier of the service to which a specified folder belongs.
             */
	serviceId: DOMString;
	/** 
 The type of the messages contained within a folder.
             */
	contentType: MessageServiceTag;
	/** 
 The visible name of a folder.
             */
	name: DOMString;
	/** 
 The whole path of a remote folder on the server.
             */
	path: DOMString;
	/** 
 The standard type of a folder.
             */
	type: DOMString;
	/** 
 The flag indicating whether this folder should be synchronized.
             */
	synchronizable: boolean;
}

interface MetadataObject {
	/** 
 Object representing a Metadata manager.
             */
	metadata: MetadataManager;
}

interface MetadataManager {
	/** 
 Creates representation of file for metadata operations.
             */
	createFileHandle(path: Path): MetadataFileHandle
}

interface MetadataFileHandle {
	/** 
  for a path passed to .
             */
	uri: DOMString;
	/** 
 Extracts a metadata of a given type.
             */
	get(type: MetadataType): void
	/** 
 Gets the artwork image included in a media file.
             */
	getArtwork(): Blob
	/** 
 Gets the thumbnail frame of a video file.
             */
	getThumbnailFrame(): Blob
	/** 
 Gets the frame of a video file for a specified time.
             */
	getFrameAtTime(timestamp: number,isAccurate: boolean): Blob
	/** 
 Gets synchronized lyrics saved in multimedia file.
             */
	getSyncLyrics(index: number): MetadataSyncLyrics
	/** 
 Releases all resources related to the handle and marks handle as invalid.
             */
	release(): void
}

interface MetadataSyncLyrics {
	/** 
 Time information about lyrics in milliseconds.
             */
	timestamp: number;
	/** 
 Lyrics stored as simple text.
             */
	lyrics: DOMString;
}

interface MachineLearningPipeline {
	/** 
 Creates a machine learning pipeline.
             */
	createPipeline(description: DOMString,listener: PipelineStateChangeListener): Pipeline
	/** 
 Registers a , which implements a custom transform to the data coming through the pipeline.
             */
	registerCustomFilter(filterName: DOMString,filter: CustomFilter,inputInfo: TensorsInfo,outputInfo: TensorsInfo,errorCallback: ErrorCallback): void
	/** 
 Unregisters a pipeline's .
             */
	unregisterCustomFilter(filterName: DOMString): void
}

interface Pipeline {
	/** 
 The current state of the pipeline.
             */
	state: PipelineState;
	/** 
 Starts the pipeline.
             */
	start(): void
	/** 
 Stops the pipeline.
             */
	stop(): void
	/** 
 Releases the resources allocated by the pipeline.
             */
	dispose(): void
	/** 
 Gets a  object allowing to get and set pipeline node's properties.
             */
	getNodeInfo(name: DOMString): NodeInfo
	/** 
 Gets a  object that allows input to the pipeline.
             */
	getSource(name: DOMString): Source
	/** 
 Gets a  object that allows to select a pipeline branch to be used as a source or sink.
             */
	getSwitch(name: DOMString): Switch
	/** 
 Gets a  object that allows to start and stop streaming data to a branch of a pipeline.
             */
	getValve(name: DOMString): Valve
	/** 
 Registers a  for a given sink. The listener is used to get output data from a pipeline.
             */
	registerSinkListener(sinkName: DOMString,sinkListener: SinkListener): void
	/** 
 Unregisters a sink's .
             */
	unregisterSinkListener(sinkName: DOMString): void
}

interface NodeInfo {
	/** 
 Name of the node.
             */
	name: DOMString;
	/** 
 Retrieves the value of node's property.
             */
	getProperty(name: DOMString,type: PropertyType): Property
	/** 
 Sets the value of node's property.
             */
	setProperty(name: DOMString,type: PropertyType,value: Property): void
}

interface Source {
	/** 
 The information about the format of tensor input expected by the source.
             */
	inputTensorsInfo: TensorsInfo;
	/** 
 Name of the source.
             */
	name: DOMString;
	/** 
 Feeds the source with input data.
             */
	inputData(data: TensorsData): void
}

interface Switch {
	/** 
 Determines the switch type.
             */
	type: SwitchType;
	/** 
 Name of the switch.
             */
	name: DOMString;
	/** 
 Retrieves the list of pad names of the switch.
             */
	getPadList(): void
	/** 
 Selects a pad to be used as a source or sink of the switch node.
             */
	select(padName: DOMString): void
}

interface Valve {
	/** 
 Name of the valve.
             */
	name: DOMString;
	/** 
 State of the valve.
             */
	isOpen: boolean;
	/** 
 Enables or disables the flow of the data through the valve by setting it to open or closed, respectively.
             */
	setOpen(open: boolean): void
}

interface PipelineStateChangeListener {
	/** 
 Called when pipeline state changes.
             */
	onstatechange(newState: PipelineState): void
}

interface SinkListener {
	/** 
 Called when new data arrives to the sink.
             */
	ondata(sinkName: DOMString,data: TensorsData): void
}

interface CustomFilter {
	/** 
 Called when data to be processed arrives to the filter.
             */
	filter(input: TensorsData,output: TensorsData): void
}

interface MachineLearningSingle {
	/** 
 Opens file, loads the neural network model and configures runtime environment with Neural Network Framework and HW information.
Use  method to close the opened model.
             */
	openModel(modelPath: Path,inTensorsInfo: TensorsInfo,outTensorsInfo: TensorsInfo,fwType: NNFWType,hwType: HWType,isDynamicMode: boolean): SingleShot
	/** 
 Opens file asynchronously, loads the neural network model and configures runtime environment with Neural Network Framework and HW information.
Use  method to close opened model.
             */
	openModelAsync(modelPath: Path,successCallback: OpenModelSuccessCallback,errorCallback: ErrorCallback,inTensorsInfo: TensorsInfo,outTensorsInfo: TensorsInfo,fwType: NNFWType,hwType: HWType,isDynamicMode: boolean): void
}

interface SingleShot {
	/** 
 The information (tensor dimension, type, name and so on) of required input data for the given model.
             */
	input: TensorsInfo;
	/** 
 The information (tensor dimension, type, name and so on) of output data for the given model.
             */
	output: TensorsInfo;
	/** 
 Invokes the model with the given input data.
             */
	invoke(inTensorsData: TensorsData): TensorsData
	/** 
 Gets the property value for the given model.
             */
	getValue(name: DOMString): void
	/** 
 Sets the property value for the given model. A model/framework may support changing the model information, such as tensor dimension and data layout.
If model does not support changing the information, this method will raise an exception.
             */
	setValue(name: DOMString,value: DOMString): void
	/** 
 Sets the maximum amount of time to wait for an output from  method, in milliseconds.
             */
	setTimeout(timeout: number): void
	/** 
 Closes the model and releases memory.
             */
	close(): void
}

interface OpenModelSuccessCallback {
	/** 
 Called when the model file is opened successfully.
             */
	onsuccess(singleShot: SingleShot): void
}

interface MachineLearningManagerObject {
	/** 
 Object representing a machine learning manager.
             */
	ml: MachineLearningManager;
}

interface MachineLearningManager {
	/** 
 Provides methods for .
             */
	single: MachineLearningSingle;
	/** 
 Provides methods for .
             */
	pipeline: MachineLearningPipeline;
	/** 
 Checks whether Neural Network Framework with provided configuration is supported.
             */
	checkNNFWAvailability(nnfw: NNFWType,hw: HWType): void
}

interface TensorRawData {
	/** 
 Raw tensor data. Array type inside TensorRawData is deduced by the  of the tensor.
             */
	data: TypedArray;
	/** 
 Size of returned data in bytes.
             */
	size: long;
	/** 
 Shape of raw tensor data - the length (number of elements) of each of the axes of a tensor.
Tensors with rank up to 4 are supported, so length of the shape array will be always 4 and axes not defined by user will be filled with 1.
             */
	shape: long[];
}

interface TensorsData {
	/** 
 Number of tensors in TensorsData object.
             */
	count: number;
	/** 
 Information about tensor.
             */
	tensorsInfo: TensorsInfo;
	/** 
 Gets tensor data at a given index. Data location and size can be provided to limit returned buffer, otherwise whole tensor will be returned.
             */
	getTensorRawData(index: long,location: long[],size: long[]): TensorRawData
	/** 
 Sets tensor data at a given index. Location and size of modified data can be provided.
             */
	setTensorRawData(index: long,buffer: Bytes,location: long[],size: long[]): void
	/** 
 Disposes an object and releases the memory. Object should not be used after calling this method. Using diposed object will trigger .
             */
	dispose(): void
}

interface TensorsInfo {
	/** 
 Number of tensor information already added to object.
             */
	count: number;
	/** 
 Add a Tensor information to the TensorsInfo instance.
             */
	addTensorInfo(name: DOMString,type: TensorType,dimensions: long[]): void
	/** 
 Clones a TensorsInfo object.
             */
	clone(): TensorsInfo
	/** 
 Compares  with TensorsInfo and checks whether it has the same contents or not.
One TensorsInfo is equal to another when they both have the same type and dimensions.
             */
	equals(other: TensorsInfo): void
	/** 
 Gets the dimensions of the tensor at a given index.
             */
	getDimensions(index: long): void
	/** 
 Gets the name of the tensor at a given index.
             */
	getTensorName(index: long): void
	/** 
 Creates a TensorsData instance based on information of TensorsInfo.
Each execution of this method creates a new TensorsData object.
             */
	getTensorsData(): TensorsData
	/** 
 Calculates the byte size of tensor data.
             */
	getTensorSize(index: long): void
	/** 
 Gets the type of the tensor at a given index.
             */
	getTensorType(index: long): TensorType
	/** 
 Sets the dimensions of the tensor at a given index.
             */
	setDimensions(index: long,dimensions: long[]): void
	/** 
 Sets the name of the tensor at a given index.
             */
	setTensorName(index: long,name: DOMString): void
	/** 
 Sets the type of the tensor at a given index.
             */
	setTensorType(index: long,type: TensorType): void
	/** 
 Disposes an object and releases the memory. Object should not be used after calling this method. Using diposed object will trigger .
             */
	dispose(): void
}

interface NetworkBearerSelectionObject {
	/** 
 Object representing a network bearer selection.
             */
	networkbearerselection: NetworkBearerSelection;
}

interface NetworkBearerSelection {
	/** 
 Requests a specific network connection.
             */
	requestRouteToHost(networkType: NetworkType,domainName: DOMString,successCallback: NetworkSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Releases a specific network connection.
             */
	releaseRouteToHost(networkType: NetworkType,domainName: DOMString,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
}

interface NetworkSuccessCallback {
	/** 
 Called when a network is connected successfully.
             */
	onsuccess(): void
	/** 
 Called when a network is disconnected.
             */
	ondisconnected(): void
}

interface NFCManagerObject {
	/** 
 Object representing a nfc manager.
             */
	nfc: NFCManager;
}

interface NFCManager {
	/** 
 Gets the default NFC adapter of the device.
             */
	getDefaultAdapter(): NFCAdapter
	/** 
 Gives priority to the current application for NFC operations.
             */
	setExclusiveMode(mode: boolean): void
}

interface NFCAdapter {
	/** 
 The state of the NFC adapter.
             */
	powered: boolean;
	/** 
 Card emulation mode of the NFC adapter.
             */
	cardEmulationMode: CardEmulationMode;
	/** 
 Active secure element type.
             */
	activeSecureElement: SecureElementType;
	/** 
 Sets the power of an NFC adapter to either an on state or an off state.
             */
	setPowered(state: boolean,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Registers a callback function to invoke when an NFC tag is detected.
             */
	setTagListener(detectCallback: NFCTagDetectCallback,tagFilter: NFCTagType[]): void
	/** 
 Registers a callback function to be invoked when an NFC peer-to-peer target is detected.
             */
	setPeerListener(detectCallback: NFCPeerDetectCallback): void
	/** 
 Unregisters the listener for detecting an NFC tag.
             */
	unsetTagListener(): void
	/** 
 Unregisters the listener for detecting an NFC peer-to-peer target.
             */
	unsetPeerListener(): void
	/** 
 Registers a callback function to invoke when the card emulation mode is changed.
             */
	addCardEmulationModeChangeListener(changeCallback: CardEmulationModeChangeCallback): void
	/** 
 Unsubscribes from receiving notification of card emulation mode changes.
             */
	removeCardEmulationModeChangeListener(watchId: long): void
	/** 
 Registers a callback function to invoke when an external reader tries to access a secure element.
Such an event may indicate initiating a financial transaction using the device.
             */
	addTransactionEventListener(type: SecureElementType,eventCallback: TransactionEventCallback): void
	/** 
 Unsubscribes from receiving notification of transaction events.
             */
	removeTransactionEventListener(watchId: long): void
	/** 
 Registers a callback function to invoke when an active secure element is changed.
             */
	addActiveSecureElementChangeListener(changeCallback: ActiveSecureElementChangeCallback): void
	/** 
 Unsubscribes from receiving notification of active secure element changes.
             */
	removeActiveSecureElementChangeListener(watchId: long): void
	/** 
 Gets the NDEF message cached when the tag is detected.
             */
	getCachedMessage(): NDEFMessage
	/** 
 Gives priority to the current application for NFC transaction events.
             */
	setExclusiveModeForTransaction(mode: boolean): void
	/** 
 Registers a callback function for receiving HCE event.
             */
	addHCEEventListener(eventCallback: HCEEventReceiveCallback): void
	/** 
 Unsubscribes from receiving notification of a HCE event.
             */
	removeHCEEventListener(watchId: long): void
	/** 
 Sends host APDU response to CLF (Contactless Front-end).
             */
	sendHostAPDUResponse(apdu: byte[],successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Allows an application to query whether an application is currently the activated handler for a specific AID and secure element type.
             */
	isActivatedHandlerForAID(type: SecureElementType,aid: AID): void
	/** 
 Allows an application to query whether an application is currently the activated handler for a specific card emulation category and secure element type.
             */
	isActivatedHandlerForCategory(type: SecureElementType,category: CardEmulationCategoryType): void
	/** 
 Registers an AID for a specific category and secure element type.
             */
	registerAID(type: SecureElementType,aid: AID,category: CardEmulationCategoryType): void
	/** 
 Unregisters an AID that was previously registered for a specific card emulation category and secure element type. An application can only remove the AIDs which it registered.
             */
	unregisterAID(type: SecureElementType,aid: AID,category: CardEmulationCategoryType): void
	/** 
 Retrieves AIDs that were previously registered for a specific card emulation category and secure element type. An application can only retrieve the AIDs which it registered.
             */
	getAIDsForCategory(type: SecureElementType,category: CardEmulationCategoryType,successCallback: AIDArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Sets current application as preferred application for NFC card emulation events as long as it is in foreground.
             */
	setPreferredApp(): void
	/** 
 Unsets currently running application as preferred application for NFC card emulation events.
             */
	unsetPreferredApp(): void
}

interface NFCTag {
	/** 
 The type of the NFC tag.
             */
	type: NFCTagType;
	/** 
 An attribute to check if the NFC Tag supports the NDEF format.
             */
	isSupportedNDEF: boolean;
	/** 
 The size of an NDEF message stored in the tag.
             */
	ndefSize: long;
	/** 
 The value is all tag information.
             */
	properties: object;
	/** 
 The value is necessary to check if this tag is connected.
             */
	isConnected: boolean;
	/** 
 Reads the NDEF data from the NFC tag.
             */
	readNDEF(readCallback: NDEFMessageReadCallback,errorCallback: ErrorCallback): void
	/** 
 Writes the NDEF data to the NFC tag.
             */
	writeNDEF(ndefMessage: NDEFMessage,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Accesses the raw format card. The transceive function is the only way to access the raw format card (not formatted).
Each tag type requires its own command to access tags.
This API provides low level access of the tag operation. (Note that you must know each tag technology.)
             */
	transceive(data: byte[],dataCallback: ByteArraySuccessCallback,errorCallback: ErrorCallback): void
}

interface NFCPeer {
	/** 
 The value is necessary to check if this NFC peer-to-peer target is connected.
             */
	isConnected: boolean;
	/** 
 Registers a callback function to be invoked when an NDEF message is received from the connected NFC peer-to-peer target.
             */
	setReceiveNDEFListener(successCallback: NDEFMessageReadCallback): void
	/** 
 Unregisters the listener for receiving NDEF messages from the NFC peer-to-peer target connected.
             */
	unsetReceiveNDEFListener(): void
	/** 
 Sends data to the NFC peer-to-peer target.
             */
	sendNDEF(ndefMessage: NDEFMessage,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
}

interface NDEFMessage {
	/** 
 The number of records in the NDEFMessage.
             */
	recordCount: long;
	/** 
 The array of NDEFRecord objects in the NDEFMessage.
             */
	records: NDEFRecord[];
	/** 
 Gets the serial byte array of the NDEF message.
             */
	toByte(): void
}

interface NDEFRecord {
	/** 
 The value of the record type (TNF value).
             */
	tnf: short;
	/** 
 The specified type in byte array.
             */
	type: byte[];
	/** 
 The record ID.
             */
	id: byte[];
	/** 
 The record payload.
             */
	payload: byte[];
}

interface NDEFRecordText {
	/** 
 The encoded text.
             */
	text: DOMString;
	/** 
 The language code string value, followed by IANA[RFC 3066] (for example, en-US, ko-KR).
             */
	languageCode: DOMString;
	/** 
 The encoding type. By default, this attribute is set to UTF8.
             */
	encoding: NDEFRecordTextEncoding;
}

interface NDEFRecordURI {
	/** 
 The URI string that is stored in the payload.
             */
	uri: DOMString;
}

interface NDEFRecordMedia {
	/** 
 The mime type [RFC 2046] (for example, text/plain, image/jpeg ).
             */
	mimeType: DOMString;
}

interface HCEEventData {
	/** 
 HCE event type.
             */
	eventType: HCEEventType;
	/** 
 The bytes array of APDU
             */
	apdu: byte[];
	/** 
 The length of APDU
             */
	length: long;
}

interface AIDData {
	/** 
 Secure Element type.
             */
	type: SecureElementType;
	/** 
 The AID (Application ID) data, specified in ISO/IEC 7816-4
             */
	aid: AID;
	/** 
 An attribute to indicate whether the registered AID is read-only or not
             */
	readOnly: boolean;
}

interface NFCTagDetectCallback {
	/** 
 The method invoked when a tag is attached.
             */
	onattach(nfcTag: NFCTag): void
	/** 
 The method invoked when the connected tag is detached.
             */
	ondetach(): void
}

interface NFCPeerDetectCallback {
	/** 
 The method invoked when the NFC peer-to-peer target is attached.
             */
	onattach(nfcPeer: NFCPeer): void
	/** 
 The method invoked when the connected NFC peer-to-peer target is detached.
             */
	ondetach(): void
}

interface NDEFMessageReadCallback {
	/** 
 The method invoked when the asynchronous call completes successfully.
             */
	onsuccess(ndefMessage: NDEFMessage): void
}

interface ByteArraySuccessCallback {
	/** 
 The method invoked when the asynchronous call completes successfully.
             */
	onsuccess(data: byte[]): void
}

interface CardEmulationModeChangeCallback {
	/** 
 Called when the card emulation mode is changed.
             */
	onchanged(mode: CardEmulationMode): void
}

interface TransactionEventCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	ondetected(appletId: octet[],data: octet[]): void
}

interface ActiveSecureElementChangeCallback {
	/** 
 Called when the type of an active secure element is changed.
             */
	onchanged(type: SecureElementType): void
}

interface HCEEventReceiveCallback {
	/** 
 Called when HCE event is detected.
             */
	ondetected(data: HCEEventData): void
}

interface AIDArraySuccessCallback {
	/** 
 The method invoked when the asynchronous call completes successfully.
             */
	onsuccess(aids: AIDData[]): void
}

interface NotificationObject {
	/** 
 Object representing a notification manager.
             */
	notification: NotificationManager;
}

interface NotificationManager {
	/** 
 Posts a notification to display.
             */
	post(notification: Notification): void
	/** 
 Updates a previously posted notification.
             */
	update(notification: Notification): void
	/** 
 Removes a previously posted notification.
             */
	remove(id: NotificationId): void
	/** 
 Removes all notifications that have been posted by the current application.
             */
	removeAll(): void
	/** 
 Gets a notification that has previously been posted by the current application. Note that the obtained notification's progressType is              */
	get(id: NotificationId): Notification
	/** 
 Gets a notification that has previously been posted by the current application. Note that the obtained notification's progressType is .
             */
	getNotification(id: NotificationId): Notification
	/** 
 Gets all notifications that have previously been posted by the current application. Note that the obtained notification's progressType is              */
	getAll(): void
	/** 
 Gets all notifications that have previously been posted by the current application. Note that the obtained notification's progressType is .
             */
	getAllNotifications(): void
	/** 
 Plays the custom effect of the service LED that is located to the front of a device.
             */
	playLEDCustomEffect(timeOn: long,timeOff: long,color: DOMString,flags: LEDCustomFlags[]): void
	/** 
 Stops the custom effect of the service LED that is located to the front of a device.
             */
	stopLEDCustomEffect(): void
	/** 
 Saves a notification template to the notification database.
             */
	saveNotificationAsTemplate(name: DOMString,notification: Notification): void
	/** 
 Creates notification based on previously created template.
             */
	createNotificationFromTemplate(name: DOMString): UserNotification
}

interface Notification {
	/** 
 The Notification identifier. Before the notification is posted, this value is undefined.
             */
	id: NotificationId;
	/** 
 The Notification type.
             */
	type: NotificationType;
	/** 
 The time when the notification is posted. Before the notification is posted, this value is undefined.
             */
	postedTime: Date;
	/** 
 The title to display in a notification.
             */
	title: DOMString;
	/** 
 The content to display in a notification.
             */
	content: DOMString;
}

interface StatusNotification {
	/** 
 The status notification type.
             */
	statusType: StatusNotificationType;
	/** 
 The icon path to display in the notification.
             */
	iconPath: DOMString;
	/** 
 The sub icon path to display in the notification.
             */
	subIconPath: DOMString;
	/** 
 The number of events to display in the notification.
             */
	number: long;
	/** 
 Appends lines of the detail information to the notification.
This attribute is available in a simple status notification.
By default, this attribute is initialized with an empty array.
The maximum number of detail information elements in the array is 2.
             */
	detailInfo: NotificationDetailInfo[];
	/** 
 Sets the notification LED indicator color property.
The color is a numerical RGB value(#rrggbb). The format of an RGB value in hexadecimal notation is a "#" immediately followed by exactly six hexadecimal characters(0-9, A-F). The color format is case-insensitive.
The LED indicator color will show that it's a close approximation.
LED will only light on when the screen is off. To turn the LED off, set "#000000" or null to ledColor.
This method has effects when the device has notification LED.
             */
	ledColor: DOMString;
	/** 
 The milliseconds for which the light is on.
The light continuously toggles on (ledOnPeriod) and off (ledOffPeriod).
By default, this attribute is set to 0
             */
	ledOnPeriod: number;
	/** 
 The milliseconds for which the light is off.
By default, this attribute is set to 0.
             */
	ledOffPeriod: number;
	/** 
 The image path to use as the background of the notification.
This attribute is available on simple or thumbnail status notifications.
             */
	backgroundImagePath: DOMString;
	/** 
 The image paths associated with the thumbnail status notification.
By default, this attribute is initialized with an empty array.
The maximum number of thumbnail path elements in the array is 4.
             */
	thumbnails: DOMString[];
	/** 
 The path of a sound file to play when the notification is shown.
             */
	soundPath: DOMString;
	/** 
 Checks whether to vibrate when the notification is shown. By default, this attribute is set to false.
             */
	vibration: boolean;
	/** 
 Holds the application control to launch an application when the notification is selected from the notification tray.
             */
	appControl: ApplicationControl;
	/** 
 Holds the application ID to launch when the notification is selected from the notification tray.
             */
	appId: ApplicationId;
	/** 
 Defines the type for an ongoing notification's progress.
By default, this attribute is set to PERCENTAGE.
             */
	progressType: NotificationProgressType;
	/** 
 Defines the current notification progress value ( or ), depending on the              */
	progressValue: number;
}

interface UserNotification {
	/** 
 The type of notification.
             */
	userType: UserNotificationType;
	/** 
 Defines content-related settings of a notification.
             */
	textContents: NotificationTextContentInfo;
	/** 
 Defines additional image-related settings of a notification.
             */
	images: NotificationImageInfo;
	/** 
 Defines additional thumbnails-related settings of a notification.
             */
	thumbnails: NotificationThumbnailInfo;
	/** 
 Defines additional actions-related settings of a notification.
             */
	actions: NotificationActionInfo;
	/** 
 Defines additional group-content-related settings of a notification.
             */
	groupContents: NotificationGroupContentInfo;
	/** 
 Defines additional LED-related settings of a notification.
             */
	leds: NotificationLedInfo;
}

interface NotificationDetailInfo {
	/** 
 The main content of the detail information.
This attribute is available on simple status notifications.
             */
	mainText: DOMString;
	/** 
 The secondary content of the detail information.
             */
	subText: DOMString;
}

interface PackageManagerObject {
	/** 
 Object representing a package manager.
             */
	package: PackageManager;
}

interface PackageManager {
	/** 
 Installs a package with a specified file on a device.
             */
	install(packageFileURI: DOMString,progressCallback: PackageProgressCallback,errorCallback: ErrorCallback): void
	/** 
 Uninstalls the package with a specified package ID.
             */
	uninstall(id: PackageId,progressCallback: PackageProgressCallback,errorCallback: ErrorCallback): void
	/** 
 Gets information of the installed packages.
             */
	getPackagesInfo(successCallback: PackageInformationArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets information of an installed package.
             */
	getPackageInfo(id: PackageId): PackageInformation
	/** 
 Sets a listener to receive notifications for any changes made to the list of installed packages.
             */
	setPackageInfoEventListener(eventCallback: PackageInformationEventCallback): void
	/** 
 Unsets the listener to stop receiving package notifications.
             */
	unsetPackageInfoEventListener(): void
}

interface PackageInformation {
	/** 
 An attribute to store the identifier of a package.
             */
	id: PackageId;
	/** 
 An attribute to store the package name.
             */
	name: DOMString;
	/** 
 An attribute to store the icon path of a package.
             */
	iconPath: DOMString;
	/** 
 An attribute to store the package version.
             */
	version: DOMString;
	/** 
 An attribute to store the total installed size(package + data) of a package.
             */
	totalSize: long;
	/** 
 An attribute to store the current data size of a package.
             */
	dataSize: long;
	/** 
 An attribute to store the latest installed or updated time of a package.
             */
	lastModified: Date;
	/** 
 An attribute to store the author of a package.
             */
	author: DOMString;
	/** 
 An attribute to store the package description.
             */
	description: DOMString;
	/** 
 An attribute to store the application ID list of a package.
             */
	appIds: ApplicationId[];
}

interface PackageInformationArraySuccessCallback {
	/** 
 Called when the asynchronous call completes successfully.
             */
	onsuccess(informationArray: PackageInformation[]): void
}

interface PackageProgressCallback {
	/** 
 Called while the request is in progress.
             */
	onprogress(id: PackageId,progress: short): void
	/** 
 Called when the request is completed.
             */
	oncomplete(id: PackageId): void
}

interface PackageInformationEventCallback {
	/** 
 Called when a package is installed.
             */
	oninstalled(info: PackageInformation): void
	/** 
 Called when a package is updated.
             */
	onupdated(info: PackageInformation): void
	/** 
 Called when a package is uninstalled.
             */
	onuninstalled(id: PackageId): void
}

interface PlayerUtilManagerObject {
	/** 
 Object representing a player utilities manager.
             */
	playerutil: PlayerUtilManager;
}

interface PlayerUtilManager {
	/** 
 Gets the latency mode of the W3C Player.
             */
	getLatencyMode(): LatencyMode
	/** 
 Sets the latency mode of the W3C Player.
             */
	setLatencyMode(mode: LatencyMode): void
}

interface PowerManagerObject {
	/** 
 Object representing a power manager.
             */
	power: PowerManager;
}

interface PowerManager {
	/** 
 Requests the minimum-state for a power resource.
             */
	request(resource: PowerResource,state: PowerState): void
	/** 
 Releases the power state request for the given resource.
             */
	release(resource: PowerResource): void
	/** 
 Sets the screen state change callback and monitors its state changes.
             */
	setScreenStateChangeListener(listener: ScreenStateChangeCallback): void
	/** 
 Unsets the screen state change callback and stop monitoring it.
             */
	unsetScreenStateChangeListener(): void
	/** 
 Gets the screen brightness level of an application, from 0 to 1.
             */
	getScreenBrightness(): void
	/** 
 Sets the screen brightness level, from 0 to 1.
             */
	setScreenBrightness(brightness: double): void
	/** 
 Checks whether the screen is on.
             */
	isScreenOn(): void
	/** 
 Restores the screen brightness to the system default setting value.
             */
	restoreScreenBrightness(): void
	/** 
 Turns on the screen.
             */
	turnScreenOn(): void
	/** 
 Turns off the screen.
             */
	turnScreenOff(): void
}

interface ScreenStateChangeCallback {
	/** 
 Called on screen state change.
             */
	onchanged(previousState: PowerScreenState,changedState: PowerScreenState): void
}

interface PrivacyPrivilegeManagerObject {
	/** 
 Object representing a privacy privilege manager.
             */
	ppm: PrivacyPrivilegeManager;
}

interface PrivacyPrivilegeManager {
	/** 
 Method allows checking current state of user's permission for using a privilege.
             */
	checkPermission(privilege: DOMString): PermissionType
	/** 
 Method allows checking current state of user's permission for using privileges.
             */
	checkPermissions(privileges: DOMString[]): void
	/** 
 This method allows launching pop-up for asking user to directly grant permission for given privilege.
             */
	requestPermission(privilege: DOMString,successCallback: PermissionSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 This method allows launching pop-up for asking user to directly grant permission for given privileges.
             */
	requestPermissions(privileges: DOMString[],successCallback: PermissionRequestSuccessCallback,errorCallback: ErrorCallback): void
}

interface PrivilegeStatus {
	/** 
 Privilege which was checked against user's permission.
             */
	privilege: DOMString;
	/** 
 State of user's permission for using specified privilege.
             */
	type: PermissionType;
}

interface RequestStatus {
	/** 
 The requested privilege.
             */
	privilege: DOMString;
	/** 
 Result of the action performed by user.
             */
	result: PermissionRequestResult;
}

interface PermissionSuccessCallback {
	/** 
 Called when the permission for using privilege was requested successfully.
             */
	onsuccess(result: PermissionRequestResult,privilege: DOMString): void
}

interface PermissionRequestSuccessCallback {
	/** 
 Called when the permission for privileges was requested successfully.
             */
	onsuccess(result: RequestStatus[]): void
}

interface PreferenceManagerObject {
	/** 
 Object representing a preference manager.
             */
	preference: PreferenceManager;
}

interface PreferenceData {
	/** 
 The key name of the preferences data value.
             */
	key: DOMString;
	/** 
 The value associated with a given key.
             */
	value: PreferenceValueType;
}

interface PreferenceManager {
	/** 
 Gets all preferences data.
             */
	getAll(successCallback: PreferenceGetAllCallback,errorCallback: ErrorCallback): void
	/** 
 Sets the preference value.
             */
	setValue(key: DOMString,value: PreferenceValueType): void
	/** 
 Gets a preference value.
             */
	getValue(key: DOMString): PreferenceValueType
	/** 
 Removes a value with the given key from the preferences.
             */
	remove(key: DOMString): void
	/** 
 Removes all key-value pairs from the preferences.
             */
	removeAll(): void
	/** 
 Checks whether the preference with given key exists.
             */
	exists(key: DOMString): void
	/** 
 Sets the listener to receive notifications about changes of the preference value with the given key.
             */
	setChangeListener(key: DOMString,listener: PreferenceChangeCallback): void
	/** 
 Unsets the listener, so stop receiving notifications about changes of the preference with the given key.
             */
	unsetChangeListener(key: DOMString): void
}

interface PreferenceChangeCallback {
	/** 
 Called when the preference with the given key changed.
             */
	onsuccess(data: PreferenceData): void
}

interface PreferenceGetAllCallback {
	/** 
 Called with all preferences' data as an argument.
             */
	onsuccess(preferences: PreferenceData[]): void
}

interface PushManagerObject {
	/** 
 Object representing a push manager.
             */
	push: PushManager;
}

interface PushManager {
	/** 
 Registers an application to the Tizen push server.
             */
	registerService(appControl: ApplicationControl,successCallback: PushRegisterSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Registers an application to the Tizen push server.
             */
	register(successCallback: PushRegisterSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Unregisters an application from the Tizen push server.
             */
	unregisterService(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Unregisters an application from the Tizen push server.
             */
	unregister(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Connects to the push service and receives push notifications.
             */
	connectService(notificationCallback: PushNotificationCallback): void
	/** 
 Connects to the push service and gets state change events and push notifications.
             */
	connect(stateChangeCallback: PushRegistrationStateChangeCallback,notificationCallback: PushNotificationCallback,errorCallback: ErrorCallback): void
	/** 
 Disconnects the push service and stops receiving push notifications.
             */
	disconnectService(): void
	/** 
 Disconnects the push service and stops receiving push notifications.
             */
	disconnect(): void
	/** 
 Gets the push service registration ID for this application if the registration process is successful.  is returned if the application has not been registered yet.
             */
	getRegistrationId(): PushRegistrationId
	/** 
 Requests to get unread push notifications. As a consequence, the PushNotificationCallback which was set using the  method will be invoked to retrieve the notifications.
             */
	getUnreadNotifications(): void
	/** 
 Gets push messages when the application is launched by the push service.
             */
	getPushMessage(): PushMessage
}

interface PushMessage {
	/** 
 An attribute to store the push notification data.
             */
	appData: DOMString;
	/** 
 An attribute to store the push notification message that may include an alert message to the user.
             */
	alertMessage: DOMString;
	/** 
 An attribute to store the full push notification message.
             */
	message: DOMString;
	/** 
 An attribute to store the date/time when a push notification message is received.
             */
	date: Date;
	/** 
 The name of the sender of the notification.
             */
	sender: DOMString;
	/** 
 The session information of the notification.
             */
	sessionInfo: DOMString;
	/** 
 The request ID assigned by the sender.
             */
	requestId: DOMString;
}

interface PushRegisterSuccessCallback {
	/** 
 Called when a push service registration request is successful.
             */
	onsuccess(id: PushRegistrationId): void
}

interface PushRegistrationStateChangeCallback {
	/** 
 Called when the state of push registration is changed.
             */
	onsuccess(state: PushRegistrationState): void
}

interface PushNotificationCallback {
	/** 
 Called when the push notification message arrives.
             */
	onsuccess(message: PushMessage): void
}

interface SensorServiceManagerObject {
	/** 
 Object representing a sensor service.
             */
	sensorservice: SensorService;
}

interface SensorService {
	/** 
 Gets the default sensor of the device for the given sensor type.
             */
	getDefaultSensor(type: SensorType): Sensor
	/** 
 Gets the available sensor types.
             */
	getAvailableSensors(): void
}

interface Sensor {
	/** 
 Sensor type to monitor the changes.
             */
	sensorType: SensorType;
	/** 
 Starts the sensor.
             */
	start(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Stops the sensor.
             */
	stop(): void
	/** 
 Registers a listener to retrieve sensor data periodically.
             */
	setChangeListener(successCallback: SensorDataSuccessCallback,interval: long,batchLatency: long): void
	/** 
 Unregisters the sensor data change listener.
             */
	unsetChangeListener(): void
	/** 
 Gets hardware information of the sensor.
             */
	getSensorHardwareInfo(successCallback: SensorHardwareInfoSuccessCallback,errorCallback: ErrorCallback): void
}

interface AccelerationSensor {
	/** 
 Gets the current acceleration sensor data. You can refer to  interface.
             */
	getAccelerationSensorData(successCallback: SensorDataSuccessCallback,errorCallback: ErrorCallback): void
}

interface GravitySensor {
	/** 
 Gets the current gravity sensor data. You can refer to the  interface.
             */
	getGravitySensorData(successCallback: SensorDataSuccessCallback,errorCallback: ErrorCallback): void
}

interface GyroscopeSensor {
	/** 
 Gets the current gyroscope sensor data. You can refer to the  interface.
             */
	getGyroscopeSensorData(successCallback: SensorDataSuccessCallback,errorCallback: ErrorCallback): void
}

interface GyroscopeRotationVectorSensor {
	/** 
 Gets the current gyroscope rotation vector sensor data. You can refer to the  interface.
             */
	getGyroscopeRotationVectorSensorData(successCallback: SensorDataSuccessCallback,errorCallback: ErrorCallback): void
}

interface GyroscopeUncalibratedSensor {
	/** 
 Gets the current sensor data.
             */
	getGyroscopeUncalibratedSensorData(successCallback: SensorDataSuccessCallback,errorCallback: ErrorCallback): void
}

interface HRMRawSensor {
	/** 
 Gets the current sensor data.
             */
	getHRMRawSensorData(successCallback: SensorDataSuccessCallback,errorCallback: ErrorCallback): void
}

interface LightSensor {
	/** 
 Gets the current sensor data.
             */
	getLightSensorData(successCallback: SensorDataSuccessCallback,errorCallback: ErrorCallback): void
}

interface LinearAccelerationSensor {
	/** 
 Gets the current sensor data.
             */
	getLinearAccelerationSensorData(successCallback: SensorDataSuccessCallback,errorCallback: ErrorCallback): void
}

interface MagneticSensor {
	/** 
 Gets the current sensor data.
             */
	getMagneticSensorData(successCallback: SensorDataSuccessCallback,errorCallback: ErrorCallback): void
}

interface MagneticUncalibratedSensor {
	/** 
 Gets the current sensor data.
             */
	getMagneticUncalibratedSensorData(successCallback: SensorDataSuccessCallback,errorCallback: ErrorCallback): void
}

interface PressureSensor {
	/** 
 Gets the current sensor data.
             */
	getPressureSensorData(successCallback: SensorDataSuccessCallback,errorCallback: ErrorCallback): void
}

interface ProximitySensor {
	/** 
 Gets the current sensor data.
             */
	getProximitySensorData(successCallback: SensorDataSuccessCallback,errorCallback: ErrorCallback): void
}

interface UltravioletSensor {
	/** 
 Gets the current sensor data.
             */
	getUltravioletSensorData(successCallback: SensorDataSuccessCallback,errorCallback: ErrorCallback): void
}

interface SensorData {
}

interface SensorAccelerationData {
	/** 
 The result of acceleration sensor measurement in the device's X axis in m/s².The value can be between -19.6 and 19.6 inclusive.
             */
	x: double;
	/** 
 The result of acceleration sensor measurement in the device's Y axis in m/s².The value can be between -19.6 and 19.6 inclusive.
             */
	y: double;
	/** 
 The result of acceleration sensor measurement in the device's Z axis in m/s².The value can be between -19.6 and 19.6 inclusive.
             */
	z: double;
}

interface SensorGravityData {
	/** 
 Value of the Earth's gravity in the device's X axis in m/s².The value can be between -9.8 and 9.8 inclusive.
             */
	x: double;
	/** 
 Value of the Earth's gravity in the device's Y axis in m/s².The value can be between -9.8 and 9.8 inclusive.
             */
	y: double;
	/** 
 Value of the Earth's gravity in the device's Z axis in m/s².The value can be between -9.8 and 9.8 inclusive.
             */
	z: double;
}

interface SensorGyroscopeData {
	/** 
 The angular velocity about the device's X axis in °/s.The value can be between -573.0 and 573.0 inclusive.
             */
	x: double;
	/** 
 The angular velocity about the device's Y axis in °/s.The value can be between -573.0 and 573.0 inclusive.
             */
	y: double;
	/** 
 The angular velocity about the device's Z axis in °/s.The value can be between -573.0 and 573.0 inclusive.
             */
	z: double;
}

interface SensorGyroscopeRotationVectorData {
	/** 
 The X direction component of the rotation vector (x * sin(θ/2)).The value can be between -1 and 1 inclusive.
             */
	x: double;
	/** 
 The Y direction component of the rotation vector (y * sin(θ/2)).The value can be between -1 and 1 inclusive.
             */
	y: double;
	/** 
 The Z direction component of the rotation vector (z * sin(θ/2)).The value can be between -1 and 1 inclusive.
             */
	z: double;
	/** 
 The scalar component of the rotation vector (cos(θ/2)).The value can be between -1 and 1 inclusive.
             */
	w: double;
}

interface SensorGyroscopeUncalibratedData {
	/** 
 The result of measurement of angular velocity around the device's X axis, without measurement drift compensation, in °/s.The value can be between -573.0 and 573.0 inclusive.
             */
	x: double;
	/** 
 The result of measurement of angular velocity around the device's Y axis, without measurement drift compensation, in °/s.The value can be between -573.0 and 573.0 inclusive.
             */
	y: double;
	/** 
 The result of measurement of angular velocity around the device's Z axis, without measurement drift compensation, in °/s.The value can be between -573.0 and 573.0 inclusive.
             */
	z: double;
	/** 
 Stated drift of angular velocity measurement result around the device's X axis, in °/s.The value can be between -573.0 and 573.0 inclusive.
             */
	xAxisDrift: double;
	/** 
 Stated drift of angular velocity measurement result around the device's Y axis, in °/s.The value can be between -573.0 and 573.0 inclusive.
             */
	yAxisDrift: double;
	/** 
 Stated drift of angular velocity measurement result around the device's Z axis, in °/s.The value can be between -573.0 and 573.0 inclusive.
             */
	zAxisDrift: double;
}

interface SensorHRMRawData {
	/** 
 HRM sensor light type.
             */
	lightType: DOMString;
	/** 
 HRM sensor light intensity measures the light intensity that is reflected from a blood vessel. The changes in the reported value represent blood volume changes in the microvascular bed of the tissue, and can be used to estimate heart rate.
             */
	lightIntensity: number;
}

interface SensorLightData {
	/** 
 Ambient light level in lux.
             */
	lightLevel: double;
}

interface SensorLinearAccelerationData {
	/** 
 Value of the linear acceleration in the device's X axis in m/s².The value can be between -19.6 and 19.6 inclusive.
             */
	x: double;
	/** 
 Value of the linear acceleration in the device's Y axis in m/s².The value can be between -19.6 and 19.6 inclusive.
             */
	y: double;
	/** 
 Value of the linear acceleration in the device's Z axis in m/s².The value can be between -19.6 and 19.6 inclusive.
             */
	z: double;
}

interface SensorMagneticData {
	/** 
 Ambient magnetic field of the X axis in microtesla (µT).
             */
	x: double;
	/** 
 Ambient magnetic field of the Y axis in microtesla (µT).
             */
	y: double;
	/** 
 Ambient magnetic field of the Z axis in microtesla (µT).
             */
	z: double;
	/** 
 Accuracy of magnetic sensor data.
             */
	accuracy: MagneticSensorAccuracy;
}

interface SensorMagneticUncalibratedData {
	/** 
 The result of measurement of magnetic field strength of the X axis, without measurement bias compensation, in microtesla (µT).
             */
	x: double;
	/** 
 The result of measurement of magnetic field strength of the Y axis, without measurement bias compensation, in microtesla (µT).
             */
	y: double;
	/** 
 The result of measurement of magnetic field strength of the Z axis, without measurement bias compensation, in microtesla (µT).
             */
	z: double;
	/** 
 Stated measurement bias of the value of magnetic field strength of the X axis in microtesla (µT).
             */
	xAxisBias: double;
	/** 
 Stated measurement bias of the value of magnetic field strength of the Y axis in microtesla (µT).
             */
	yAxisBias: double;
	/** 
 Stated measurement bias of the value of magnetic field strength of the Z axis in microtesla (µT).
             */
	zAxisBias: double;
}

interface SensorPressureData {
	/** 
 Pressure in hectopascal (hPa).
             */
	pressure: double;
}

interface SensorProximityData {
	/** 
 Proximity state.
             */
	proximityState: ProximityState;
}

interface SensorUltravioletData {
	/** 
 Ultraviolet index.
             */
	ultravioletLevel: long;
}

interface SensorHardwareInfo {
	/** 
 Name of the sensor.
             */
	name: DOMString;
	/** 
 .             */
	type: SensorType;
	/** 
 Vendor of the sensor.
             */
	vendor: DOMString;
	/** 
 Minimum reading value that can be received from the sensor.The units for the minimum value depends on the sensor type:             */
	minValue: double;
	/** 
 Maximum reading value that can be received from the sensor.The units for the maximum value depends on the sensor type:             */
	maxValue: double;
	/** 
 The smallest change which the sensor can detect.The units of the resolution depends on the sensor type:             */
	resolution: double;
	/** 
 Minimum interval of the sensor which means a period between two events.
             */
	minInterval: long;
	/** 
 Maximum batch count of sensor, batch means storing a sensors event in a hardware FIFO register when processor stay on sleep or suspend status.
             */
	maxBatchCount: long;
}

interface SensorDataSuccessCallback {
	/** 
 Called periodically.
             */
	onsuccess(sensorData: SensorData): void
}

interface SensorHardwareInfoSuccessCallback {
	/** 
 Called when sensor hardware information is successfully retrieved.
             */
	onsuccess(hardwareInfo: SensorHardwareInfo): void
}

interface SEServiceManagerObject {
	/** 
 Object representing a secure element service manager.
             */
	seService: SEService;
}

interface SEService {
	/** 
 Gets all the available Secure Element readers.
             */
	getReaders(successCallback: ReaderArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Registers a callback function that is invoked when an available Secure Element reader is detected.
             */
	registerSEListener(listener: SEChangeListener): void
	/** 
 Unregisters the listener from notifying any detection of an available Secure Element reader.
             */
	unregisterSEListener(id: number): void
	/** 
 Shuts down Secure Elements after releasing all resources.
             */
	shutdown(): void
}

interface Reader {
	/** 
 Boolean value that indicates whether a Secure Element is present on a reader.
             */
	isPresent: boolean;
	/** 
 Gets the reader's name.
             */
	getName(): void
	/** 
 Opens a session on a reader.
             */
	openSession(successCallback: SessionSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Closes all sessions opened on a reader.
             */
	closeSessions(): void
}

interface Session {
	/** 
 Boolean value that indicates whether a session is closed.
             */
	isClosed: boolean;
	/** 
 Opens a basic channel in a session.
The basic channel (defined in the ISO7816-4 specification) is opened by default and its channel ID is .
Once this channel has been opened by an application, it is considered to be "locked" to other applications, and they cannot open any channel, until the basic channel is closed.
Some Secure Elements might always deny opening a basic channel.
             */
	openBasicChannel(aid: byte[],successCallback: ChannelSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Opens a logical channel in a session by the specified applet ID.
The logical channel is defined in the ISO7816-4 specification.
             */
	openLogicalChannel(aid: byte[],successCallback: ChannelSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets the answer to reset(ATR) of a Secure Element.
             */
	getATR(): void
	/** 
 Closes a session.
             */
	close(): void
	/** 
 Closes all channels on this session.
             */
	closeChannels(): void
}

interface Channel {
	/** 
 Boolean value that indicates whether it is a basic channel.
             */
	isBasicChannel: boolean;
	/** 
 Closes a channel.
             */
	close(): void
	/** 
 Transmits an APDU command to a Secure Element. The APDU command is defined in ISO7816-4.
             */
	transmit(command: byte[],successCallback: TransmitSuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets the data as received from the application select command including the status words.
             */
	getSelectResponse(): void
}

interface SEChangeListener {
	/** 
 Called when a Secure Element reader is detected.
             */
	onSEReady(reader: Reader): void
	/** 
 Called when a Secure Element reader is lost.
             */
	onSENotReady(reader: Reader): void
	/** 
 Called when a Secure Element reader has an error.
             */
	onSEError(reader: Reader,error: WebAPIError): void
}

interface ReaderArraySuccessCallback {
	/** 
 Called when an asynchronous call completes successfully.
             */
	onsuccess(readers: Reader[]): void
}

interface SessionSuccessCallback {
	/** 
 Called when an asynchronous call completes successfully.
             */
	onsuccess(session: Session): void
}

interface ChannelSuccessCallback {
	/** 
 Called when an asynchronous call completes successfully.
             */
	onsuccess(channel: Channel): void
}

interface TransmitSuccessCallback {
	/** 
 Called when an asynchronous call completes successfully.
             */
	onsuccess(response: byte[]): void
}

interface SoundManagerObject {
	/** 
 Object representing a sound manager.
             */
	sound: SoundManager;
}

interface SoundManager {
	/** 
 Gets the current sound mode.
             */
	getSoundMode(): SoundModeType
	/** 
 Sets the volume level for a specified sound type.
             */
	setVolume(type: SoundType,volume: double): void
	/** 
 Gets the current volume level for a specified sound type.
             */
	getVolume(type: SoundType): void
	/** 
 Registers a listener to be called when the sound mode is changed.
             */
	setSoundModeChangeListener(callback: SoundModeChangeCallback): void
	/** 
 Unsubscribes from receiving notification about the sound mode change.
             */
	unsetSoundModeChangeListener(): void
	/** 
 Registers a listener to be called when the volume level is changed.
             */
	setVolumeChangeListener(callback: SoundVolumeChangeCallback): void
	/** 
 Unsubscribes from receiving notification when the volume level is changed.
             */
	unsetVolumeChangeListener(): void
	/** 
 Gets a list of connected sound devices.
             */
	getConnectedDeviceList(): void
	/** 
 Gets a list of activated sound devices.
             */
	getActivatedDeviceList(): void
	/** 
 Registers a listener that is to be called when the sound device state is changed.
             */
	addDeviceStateChangeListener(callback: SoundDeviceStateChangeCallback): void
	/** 
 Unsubscribes from receiving notifications when the sound device state is changed.
             */
	removeDeviceStateChangeListener(id: long): void
}

interface SoundDeviceInfo {
	/** 
 The sound device ID
             */
	id: long;
	/** 
 The sound device name
             */
	name: DOMString;
	/** 
 The sound device type
             */
	device: SoundDeviceType;
	/** 
 The sound device I/O type
             */
	direction: SoundIOType;
	/** 
 True if the sound device state is connected
             */
	isConnected: boolean;
	/** 
 True if the sound device state is activated
             */
	isActivated: boolean;
}

interface SoundModeChangeCallback {
	/** 
 Called when the sound mode has changed.
             */
	onsuccess(mode: SoundModeType): void
}

interface SoundVolumeChangeCallback {
	/** 
 Called when the volume level has changed.
             */
	onsuccess(type: SoundType,volume: double): void
}

interface SoundDeviceStateChangeCallback {
	/** 
 Method invoked when the sound device state changes.
             */
	onchanged(info: SoundDeviceInfo): void
}

interface SystemInfoObject {
	/** 
 Object representing a system info module.
             */
	systeminfo: SystemInfo;
}

interface SystemInfo {
	/** 
 Gets the total amount of system memory (in bytes).
             */
	getTotalMemory(): void
	/** 
 Gets the amount of memory that is not in use (in bytes).
             */
	getAvailableMemory(): void
	/** 
 Gets the capabilities of the device.
             */
	getCapabilities(): SystemInfoDeviceCapability
	/** 
 Gets a device capability related to a given key.
             */
	getCapability(key: DOMString): void
	/** 
 Gets the number of system property information provided for a particular system property.
             */
	getCount(property: SystemInfoPropertyId): void
	/** 
 Gets the current value of a specified system property.
             */
	getPropertyValue(property: SystemInfoPropertyId,successCallback: SystemInfoPropertySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets the current values of a specified system property.
             */
	getPropertyValueArray(property: SystemInfoPropertyId,successCallback: SystemInfoPropertyArraySuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Adds a listener to allow tracking changes in one or more system properties.
             */
	addPropertyValueChangeListener(property: SystemInfoPropertyId,successCallback: SystemInfoPropertySuccessCallback,options: SystemInfoOptions,errorCallback: ErrorCallback): void
	/** 
 Adds a listener to allow tracking of changes in one or more values of a system property.
             */
	addPropertyValueArrayChangeListener(property: SystemInfoPropertyId,successCallback: SystemInfoPropertyArraySuccessCallback,options: SystemInfoOptions,errorCallback: ErrorCallback): void
	/** 
 Unsubscribes notifications for property changes.
             */
	removePropertyValueChangeListener(listenerId: number): void
}

interface SystemInfoDeviceCapability {
	/** 
 Indicates whether the device supports Bluetooth.
             */
	bluetooth: boolean;
	/** 
 Indicates whether the device supports NFC.
             */
	nfc: boolean;
	/** 
 Indicates whether the device supports NFC reserved push.
             */
	nfcReservedPush: boolean;
	/** 
 The number of point in Multi-point touch.
             */
	multiTouchCount: number;
	/** 
 Indicates whether the device supports the built-in keyboard.
             */
	inputKeyboard: boolean;
	/** 
 Indicates whether the device supports the built-in keyboard layout.
             */
	inputKeyboardLayout: boolean;
	/** 
 Indicates whether the device supports Wi-Fi.
             */
	wifi: boolean;
	/** 
 Indicates whether the device supports Wi-Fi Direct.
             */
	wifiDirect: boolean;
	/** 
 Indicates whether the device supports OpenGL-ES.
             */
	opengles: boolean;
	/** 
 The device 3DC texture format for OpenGL-ES.
             */
	openglestextureFormat: DOMString;
	/** 
 Indicates whether the device supports OpenGL-ES version 1.1.
             */
	openglesVersion1_1: boolean;
	/** 
 Indicates whether the device supports OpenGL-ES version 2.0.
             */
	openglesVersion2_0: boolean;
	/** 
 Indicates whether the device supports FM radio.
             */
	fmRadio: boolean;
	/** 
 The version of the platform in the  format.
             */
	platformVersion: DOMString;
	/** 
 The version of the Web API in the  format.
             */
	webApiVersion: DOMString;
	/** 
 The version of the Native API in the  format.
             */
	nativeApiVersion: DOMString;
	/** 
 The name of the platform.
             */
	platformName: DOMString;
	/** 
 Indicates whether the device supports camera.
             */
	camera: boolean;
	/** 
 Indicates whether the device supports front camera.
             */
	cameraFront: boolean;
	/** 
 Indicates whether the device supports flash on the front camera.
             */
	cameraFrontFlash: boolean;
	/** 
 Indicates whether the device supports back-side camera.
             */
	cameraBack: boolean;
	/** 
 Indicates whether the device supports flash on the back-side camera.
             */
	cameraBackFlash: boolean;
	/** 
 Indicates whether the device supports GPS or not.
             */
	location: boolean;
	/** 
 Indicates whether the device supports GPS based location feature.
             */
	locationGps: boolean;
	/** 
 Indicates whether the device supports WPS based location feature.
             */
	locationWps: boolean;
	/** 
 Indicates whether the device supports microphone.
             */
	microphone: boolean;
	/** 
 Indicates whether the device supports USB host.
             */
	usbHost: boolean;
	/** 
 Indicates whether the device supports USB accessory.
             */
	usbAccessory: boolean;
	/** 
 Indicates whether the device supports RCA output.
             */
	screenOutputRca: boolean;
	/** 
 Indicates whether the device supports HDMI output.
             */
	screenOutputHdmi: boolean;
	/** 
 The device CPU architecture.
             */
	platformCoreCpuArch: DOMString;
	/** 
 The device FPU architecture.
             */
	platformCoreFpuArch: DOMString;
	/** 
 Indicates whether the device supports VOIP.
             */
	sipVoip: boolean;
	/** 
 Indicates the Tizen ID, not device's unique ID since Tizen 2.3.
             */
	duid: DOMString;
	/** 
 Indicates whether the device supports speech recognition.
             */
	speechRecognition: boolean;
	/** 
 Indicates whether the device supports speech synthesis.
             */
	speechSynthesis: boolean;
	/** 
 Indicates whether the device supports accelerometer.
             */
	accelerometer: boolean;
	/** 
 Indicates whether the device supports accelerometer wake-up feature.
             */
	accelerometerWakeup: boolean;
	/** 
 Indicates whether the device supports barometer.
             */
	barometer: boolean;
	/** 
 Indicates whether the device supports barometer wake-up feature.
             */
	barometerWakeup: boolean;
	/** 
 Indicates whether the device supports gyroscope.
             */
	gyroscope: boolean;
	/** 
 Indicates whether the device supports gyroscope wake-up feature.
             */
	gyroscopeWakeup: boolean;
	/** 
 Indicates whether the device supports magnetometer.
             */
	magnetometer: boolean;
	/** 
 Indicates whether the device supports magnetometer wake-up feature.
             */
	magnetometerWakeup: boolean;
	/** 
 Indicates whether the device supports photometer.
             */
	photometer: boolean;
	/** 
 Indicates whether the device supports photometer wake-up feature.
             */
	photometerWakeup: boolean;
	/** 
 Indicates whether the device supports proximity.
             */
	proximity: boolean;
	/** 
 Indicates whether the device supports proximity wake-up feature.
             */
	proximityWakeup: boolean;
	/** 
 Indicates whether the device supports tiltmeter.
             */
	tiltmeter: boolean;
	/** 
 Indicates whether the device supports tiltmeter wake-up feature.
             */
	tiltmeterWakeup: boolean;
	/** 
 Indicates whether the device supports data encryption.
             */
	dataEncryption: boolean;
	/** 
 Indicates whether the device supports hardware acceleration for 2D/3D graphics.
             */
	graphicsAcceleration: boolean;
	/** 
 Indicates whether the device supports push service.
             */
	push: boolean;
	/** 
 Indicates whether the device supports the telephony feature.
             */
	telephony: boolean;
	/** 
 Indicates whether the device supports the MMS feature.
             */
	telephonyMms: boolean;
	/** 
 Indicates whether the device supports the SMS feature.
             */
	telephonySms: boolean;
	/** 
 Indicates whether the device supports the screen normal size.
             */
	screenSizeNormal: boolean;
	/** 
 Indicates whether the device supports the 480 * 800 screen size.
             */
	screenSize480_800: boolean;
	/** 
 Indicates whether the device supports the 720 * 1280 screen size.
             */
	screenSize720_1280: boolean;
	/** 
 Indicates whether the device supports auto rotation.
             */
	autoRotation: boolean;
	/** 
 Indicates whether the device supports shell app widget (dynamic box).
             */
	shellAppWidget: boolean;
	/** 
 Indicates whether the device supports vision image recognition.
             */
	visionImageRecognition: boolean;
	/** 
 Indicates whether the device supports vision QR code generation.
             */
	visionQrcodeGeneration: boolean;
	/** 
 Indicates whether the device supports vision QR code recognition.
             */
	visionQrcodeRecognition: boolean;
	/** 
 Indicates whether the device supports vision face recognition.
             */
	visionFaceRecognition: boolean;
	/** 
 Indicates whether the device supports secure element.
             */
	secureElement: boolean;
	/** 
 Indicates whether the device supports native OSP API.
             */
	nativeOspCompatible: boolean;
	/** 
 Represents the profile of the current device.
             */
	profile: SystemInfoProfile;
}

interface SystemInfoPropertySuccessCallback {
	/** 
 Function invoked when the asynchronous call completes successfully.
             */
	onsuccess(property: SystemInfoProperty): void
}

interface SystemInfoPropertyArraySuccessCallback {
	/** 
 Function invoked when the asynchronous call completes successfully.
             */
	onsuccess(properties: SystemInfoProperty[]): void
}

interface SystemInfoProperty {
}

interface SystemInfoBattery {
	/** 
 An attribute to specify the remaining level of an internal battery, scaled from  to :
             */
	level: double;
	/** 
 Indicates whether the battery source is currently charging.
             */
	isCharging: boolean;
	/** 
 Estimated time to discharge, in minutes.
             */
	timeToDischarge: long;
	/** 
 Estimated time to finish charging battery, in minutes.
             */
	timeToFullCharge: long;
}

interface SystemInfoCpu {
	/** 
 An attribute to indicate the current CPU load, as a number between  and , representing the minimum and maximum values allowed on this system.
             */
	load: double;
}

interface SystemInfoStorage {
	/** 
 The array of storage units connected to this device.
             */
	units: SystemInfoStorageUnit[];
}

interface SystemInfoStorageUnit {
	/** 
 The type of a storage device. The value is one of the constants defined for this type.
             */
	type: DOMString;
	/** 
 The total amount of space available on the user's storage (excluding system-reserved), in bytes.
             */
	capacity: number;
	/** 
 The amount of space currently available on the user's storage, in bytes.
             */
	availableCapacity: number;
	/** 
 An attribute to indicate whether a device can be removed or not.
             */
	isRemovable: boolean;
	/** 
 True if this unit can be removed from the system (such as an sdcard unplugged), false otherwise.
             */
	isRemoveable: boolean;
}

interface SystemInfoDisplay {
	/** 
 The total number of addressable pixels in the horizontal direction of a rectangular entity
(such as Camera, Display, Image, Video, ...) when held in its default orientation.
             */
	resolutionWidth: number;
	/** 
 The total number of addressable pixels in the vertical direction of a rectangular element
(such as Camera, Display, Image, Video, ...) when held in its default orientation.
             */
	resolutionHeight: number;
	/** 
 Resolution of this device, along its width, in dots per inch.
             */
	dotsPerInchWidth: number;
	/** 
 Resolution of this device, along its height, in dots per inch.
             */
	dotsPerInchHeight: number;
	/** 
 The display's physical width in millimeters.
             */
	physicalWidth: double;
	/** 
 The display's physical height in millimeters.
             */
	physicalHeight: double;
	/** 
 The current brightness of a display ranging between  to .
             */
	brightness: double;
}

interface SystemInfoDeviceOrientation {
	/** 
 Represents the status of the current device orientation.
             */
	status: SystemInfoDeviceOrientationStatus;
	/** 
 Indicates whether the device is in autorotation.
             */
	isAutoRotation: boolean;
}

interface SystemInfoBuild {
	/** 
 Represents the model name of the current device.
             */
	model: DOMString;
	/** 
 Represents the manufacturer of the device.
             */
	manufacturer: DOMString;
	/** 
 Represents the build version information of the device.
             */
	buildVersion: DOMString;
}

interface SystemInfoLocale {
	/** 
 Indicates the current language setting in the (LANGUAGE)_(REGION) syntax.
             */
	language: DOMString;
	/** 
 Indicates the current country setting in the (LANGUAGE)_(REGION) syntax.
             */
	country: DOMString;
}

interface SystemInfoNetwork {
	/** 
 Represents the network type of the current data network.
             */
	networkType: SystemInfoNetworkType;
}

interface SystemInfoWifiNetwork {
	/** 
 Represents the status (ON or OFF) of the Wi-Fi interface.
             */
	status: DOMString;
	/** 
 Represents the SSID of the Wi-Fi network.
             */
	ssid: DOMString;
	/** 
 Represents the IPv4 address of the Wi-Fi network.
             */
	ipAddress: DOMString;
	/** 
 Represents the IPv6 address of the Wi-Fi network.
             */
	ipv6Address: DOMString;
	/** 
 Represents the MAC address of the Wi-Fi interface.
             */
	macAddress: DOMString;
	/** 
 This connection's signal strength, as a normalized value between 0 (no signal detected) and 1 (the level is at its maximum value).
             */
	signalStrength: double;
}

interface SystemInfoEthernetNetwork {
	/** 
 Represents the cable status (ATTACHED or DETACHED) of the Ethernet interface.
             */
	cable: DOMString;
	/** 
 Represents the status (DEACTIVATED, DISCONNECTED or CONNECTED) of the Ethernet interface.
             */
	status: DOMString;
	/** 
 Represents the IPv4 address of the Ethernet network.
             */
	ipAddress: DOMString;
	/** 
 Represents the IPv6 address of the Ethernet network.
             */
	ipv6Address: DOMString;
	/** 
 Represents the MAC address of the Ethernet interface.
             */
	macAddress: DOMString;
}

interface SystemInfoCellularNetwork {
	/** 
 Represents the status (ON or OFF) of the cellular network.
             */
	status: DOMString;
	/** 
 Represents an Access Point Name of the cellular network.
             */
	apn: DOMString;
	/** 
 Represents the IPv4 address of the cellular network.
             */
	ipAddress: DOMString;
	/** 
 Represents the IPv6 address of the cellular network.
             */
	ipv6Address: DOMString;
	/** 
 Represents Mobile Country Code (MCC) of the cellular network.
             */
	mcc: number;
	/** 
 Represents Mobile Network Code (MNC) of the cellular network. MNC is used in combination with MCC (also known as a "MCC / MNC tuple") to uniquely
identify a mobile phone operator/carrier using the GSM, CDMA, iDEN, TETRA and UMTS public land mobile networks and some satellite mobile networks.
             */
	mnc: number;
	/** 
 Represents Cell ID.
             */
	cellId: number;
	/** 
 Represents Location Area Code.
             */
	lac: number;
	/** 
 Indicates whether the connection is set up while the device is roaming.
             */
	isRoaming: boolean;
	/** 
 Indicates whether the device is in flight mode.
             */
	isFlightMode: boolean;
	/** 
 Represents the International Mobile Equipment Identity (IMEI).
             */
	imei: DOMString;
}

interface SystemInfoNetProxyNetwork {
	/** 
 Represents the status (ON or OFF) of the net_proxy network.
             */
	status: DOMString;
}

interface SystemInfoSIM {
	/** 
 Represents the SIM card state.
             */
	state: SystemInfoSimState;
	/** 
 Represents the Operator Name String (ONS) of Common PCN Handset Specification (CPHS) in SIM card.
             */
	operatorName: DOMString;
	/** 
 Represents the SIM card subscriber number.
             */
	msisdn: DOMString;
	/** 
 Represents the Integrated Circuit Card ID.
             */
	iccid: DOMString;
	/** 
 Represents the Mobile Country Code (MCC) of SIM provider.
             */
	mcc: number;
	/** 
 Represents the Mobile Network Code (MNC) of SIM provider.
             */
	mnc: number;
	/** 
 Represents the Mobile Subscription Identification Number (MSIN) of SIM provider.
             */
	msin: DOMString;
	/** 
 Represents the Service Provider Name (SPN) of SIM card.
             */
	spn: DOMString;
}

interface SystemInfoPeripheral {
	/** 
 Represents the video out status.
             */
	isVideoOutputOn: boolean;
}

interface SystemInfoMemory {
	/** 
 Represents the low memory state.
             */
	status: SystemInfoLowMemoryStatus;
}

interface SystemInfoCameraFlash {
	/** 
 Brightness level of the camera flash (0~1).
             */
	brightness: double;
	/** 
 Specifies camera to which this flash belongs.
             */
	camera: DOMString;
	/** 
 Number of brightness levels supported by the flash (other than 0 brightness).
             */
	levels: long;
	/** 
 Sets the brightness value of the flash that is located next to the camera.
             */
	setBrightness(brightness: double): void
}

interface SystemInfoADS {
	/** 
 Represents the unique id of advertisement service. It is used to distinguish each device.
             */
	id: DOMString;
}

interface SystemSettingObject {
	/** 
 Object representing a system settings manager.
             */
	systemsetting: SystemSettingManager;
}

interface SystemSettingManager {
	/** 
 Sets the property of a device.
             */
	setProperty(type: SystemSettingType,value: DOMString,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Gets the value of the property of a device.
             */
	getProperty(type: SystemSettingType,successCallback: SystemSettingSuccessCallback,errorCallback: ErrorCallback): void
}

interface SystemSettingSuccessCallback {
	/** 
 Called when the value of the system setting property is successfully retrieved.
             */
	onsuccess(value: DOMString): void
}

interface TimeManagerObject {
	/** 
 Object representing a time manager.
             */
	time: TimeUtil;
}

interface TimeUtil {
	/** 
 Gets the current date/time.
             */
	getCurrentDateTime(): TZDate
	/** 
 Gets the identifier of the local system timezone.
             */
	getLocalTimezone(): void
	/** 
 Gets synchronously the identifiers of the timezones supported by the device.
             */
	getAvailableTimezones(): void
	/** 
 Gets the date format according to the system's locale settings.
             */
	getDateFormat(shortformat: boolean): void
	/** 
 Gets the time format according to the system's locale settings.
             */
	getTimeFormat(): void
	/** 
 Checks whether the given year is a leap year.
             */
	isLeapYear(year: long): void
	/** 
 Sets a listener to receive notification of changes to the time/date on a device.
             */
	setDateTimeChangeListener(changeCallback: SuccessCallback): void
	/** 
 Unsets the listener to stop receiving notification of changes to the time/date on a device.
             */
	unsetDateTimeChangeListener(): void
	/** 
 Sets a listener to receive notification of changes to the time zone on a device.
             */
	setTimezoneChangeListener(changeCallback: SuccessCallback): void
	/** 
 Unsets the listener to stop receiving notification of changes to the time zone on a device.
             */
	unsetTimezoneChangeListener(): void
}

interface TZDate {
	/** 
 Gets the day of the month (from 1-31).
             */
	getDate(): void
	/** 
 Sets the day of the month (from 1-31).
             */
	setDate(date: long): void
	/** 
 Gets the day of the week (from 0-6). 0 denotes Sunday, 1 denotes Monday and so on.
             */
	getDay(): void
	/** 
 Gets the year.
             */
	getFullYear(): void
	/** 
 Sets the year.
             */
	setFullYear(year: long): void
	/** 
 Gets the hour (0-23).
             */
	getHours(): void
	/** 
 Sets the hour (0-23).
             */
	setHours(hours: long): void
	/** 
 Gets the milliseconds (from 0-999).
             */
	getMilliseconds(): void
	/** 
 Sets the milliseconds (from 0-999).
             */
	setMilliseconds(ms: long): void
	/** 
 Gets the minutes (from 0-59).
             */
	getMinutes(): void
	/** 
 Sets the minutes.
             */
	setMinutes(minutes: long): void
	/** 
 Gets the month (from 0-11). Note: January is denoted as 0, February as 1, and so on till December, which is denoted as 11.
             */
	getMonth(): void
	/** 
 Sets the month (from 0-11).
             */
	setMonth(month: long): void
	/** 
 Gets the seconds (from 0-59).
             */
	getSeconds(): void
	/** 
 Sets the seconds (from 0-59).
             */
	setSeconds(seconds: long): void
	/** 
 Gets the day of the month, according to universal time (from 1-31).
             */
	getUTCDate(): void
	/** 
 Sets the day of the month, according to universal time (from 1-31).
             */
	setUTCDate(date: long): void
	/** 
 Gets the day of the week, according to universal time (from 0-6).
             */
	getUTCDay(): void
	/** 
 Gets the year, according to universal time.
             */
	getUTCFullYear(): void
	/** 
 Sets the year, according to universal time.
             */
	setUTCFullYear(year: long): void
	/** 
 Gets the hour, according to universal time (0-23).
             */
	getUTCHours(): void
	/** 
 Sets the hour, according to universal time (0-23).
             */
	setUTCHours(hours: long): void
	/** 
 Gets the milliseconds, according to universal time (from 0-999).
             */
	getUTCMilliseconds(): void
	/** 
 Sets the milliseconds, according to universal time (from 0-999).
             */
	setUTCMilliseconds(ms: long): void
	/** 
 Gets the minutes, according to universal time (from 0-59).
             */
	getUTCMinutes(): void
	/** 
 Sets the minutes, according to universal time (from 0-59).
             */
	setUTCMinutes(minutes: long): void
	/** 
 Gets the month, according to universal time (from 0-11). Note: January is denoted as 0, February as 1 and so on till December, which is denoted as 11.
             */
	getUTCMonth(): void
	/** 
 Sets the month, according to universal time (from 0-11).
             */
	setUTCMonth(month: long): void
	/** 
 Gets the seconds, according to universal time (from 0-59).
             */
	getUTCSeconds(): void
	/** 
 Sets the seconds, according to universal time (from 0-59).
             */
	setUTCSeconds(seconds: long): void
	/** 
 Gets the timezone identifier.
             */
	getTimezone(): void
	/** 
 Gets a copy of the TZDate converted to a given time zone.
             */
	toTimezone(tzid: DOMString): TZDate
	/** 
 Gets a copy of the TZDate converted to the local time zone.
             */
	toLocalTimezone(): TZDate
	/** 
 Gets a copy of the TZDate converted to Coordinated Universal Time (UTC).
             */
	toUTC(): TZDate
	/** 
 Calculates the difference with another TZDate object.
             */
	difference(other: TZDate): TimeDuration
	/** 
 Checks whether the TZDate is equal to another.
             */
	equalsTo(other: TZDate): void
	/** 
 Checks whether the TZDate is earlier than another.
             */
	earlierThan(other: TZDate): void
	/** 
 Checks whether the TZDate is later than another.
             */
	laterThan(other: TZDate): void
	/** 
 Gets a new date by adding a duration to the current TZDate object.
             */
	addDuration(duration: TimeDuration): TZDate
	/** 
 Gets the date portion of a TZDate object as a string, using locale conventions.
             */
	toLocaleDateString(): void
	/** 
 Gets the time portion of a TZDate object as a string, using locale conventions.
             */
	toLocaleTimeString(): void
	/** 
 Converts a TZDate object to a string, using locale conventions.
             */
	toLocaleString(): void
	/** 
 Gets the date portion of a TZDate object as a string.
             */
	toDateString(): void
	/** 
 Gets the time portion of a TZDate object as a string.
             */
	toTimeString(): void
	/** 
 Converts a TZDate object to a string.
             */
	toString(): void
	/** 
 Determines the time zone abbreviation to be used at a particular date in the time zone.
             */
	getTimezoneAbbreviation(): void
	/** 
 Gets the number of seconds from Coordinated Universal Time (UTC) offset for the timezone.
             */
	secondsFromUTC(): void
	/** 
 Checks whether Daylight Saving Time(DST) is active for this TZDate.
             */
	isDST(): void
	/** 
 Gets the date of the previous daylight saving time transition for the timezone.
             */
	getPreviousDSTTransition(): TZDate
	/** 
 Gets the date of the next daylight saving time transition for the timezone.
             */
	getNextDSTTransition(): TZDate
}

interface TimeDuration {
	/** 
 The duration length.
             */
	length: long long;
	/** 
 The duration unit (milliseconds, seconds, minutes, hours, or days).
             */
	unit: TimeDurationUnit;
	/** 
 Calculates the difference between two TimeDuration objects.
             */
	difference(other: TimeDuration): TimeDuration
	/** 
 Checks whether the TimeDuration is equal to another.
             */
	equalsTo(other: TimeDuration): void
	/** 
 Checks whether the TimeDuration is lower than another.
             */
	lessThan(other: TimeDuration): void
	/** 
 Checks whether the TimeDuration is greater than another.
             */
	greaterThan(other: TimeDuration): void
}

interface TizenObject {
	/** 
 Object representing a tizen manager.
             */
	tizen: Tizen;
}

interface Bundle {
	/** 
 Gets value stored under given key.
             */
	get(key: DOMString): void
	/** 
 Inserts the key-value pair.
             */
	set(key: DOMString,value: any): void
	/** 
 Gets type of the value for a given key.
             */
	typeOf(key: DOMString): BundleValueType
	/** 
 Calls the callback function for each item stored in the bundle.
             */
	forEach(callback: BundleItemCallback): void
	/** 
 Converts bundle to JSON-compatible object.
             */
	toJSON(): void
	/** 
 Returns string representation of the bundle's data.
             */
	toString(): void
}

interface AbstractFilter {
}

interface AttributeFilter {
	/** 
 The name of the object attribute used for filtering.
             */
	attributeName: DOMString;
	/** 
 The match flag used for attribute-based filtering.
             */
	matchFlag: FilterMatchFlag;
	/** 
 The value used for matching.
             */
	matchValue: any;
}

interface AttributeRangeFilter {
	/** 
 The name of the object attribute used for filtering.
             */
	attributeName: DOMString;
	/** 
 Objects with an attribute that is greater than or equal to  will match.
             */
	initialValue: any;
	/** 
 Objects with an attribute that is strictly lower than or equal to  will match.
             */
	endValue: any;
}

interface CompositeFilter {
	/** 
 The composite filter type.
             */
	type: CompositeFilterType;
	/** 
 The list of filters in the composite filter.
             */
	filters: AbstractFilter[];
}

interface SortMode {
	/** 
 The name of the object attribute used for sorting.
             */
	attributeName: DOMString;
	/** 
 The type of the sorting.
             */
	order: SortModeOrder;
}

interface SimpleCoordinates {
	/** 
 Latitude.
             */
	latitude: double;
	/** 
 Longitude.
             */
	longitude: double;
}

interface WebAPIException {
	/** 
 16-bit error code.
             */
	code: number;
	/** 
 An error type. The name attribute must return the value it had been initialized with.
             */
	name: DOMString;
	/** 
 An error message that describes the details of an encountered error.
             */
	message: DOMString;
}

interface WebAPIError {
	/** 
 16-bit error code.
             */
	code: number;
	/** 
 An error type. The name attribute must return the value it had been initialized with.
             */
	name: DOMString;
	/** 
 An error message that describes the details of the error encountered.
             */
	message: DOMString;
}

interface SuccessCallback {
	/** 
 Method invoked when the asynchronous call completes successfully.
             */
	onsuccess(): void
}

interface ErrorCallback {
	/** 
 Method that is invoked when an error occurs.
             */
	onerror(error: WebAPIError): void
}

interface BundleItemCallback {
	/** 
 Callback for  method.
             */
	onitem(key: DOMString,value: any,type: BundleValueType): void
}

interface VoiceControlClientManagerObject {
	/** 
 Object representing a voice control manager.
             */
	voicecontrol: VoiceControlClientManager;
}

interface VoiceControlClientManager {
	/** 
 Requests voice control Client instance.
             */
	getVoiceControlClient(): VoiceControlClient
}

interface VoiceControlClient {
	/** 
 Gets current language.
             */
	getCurrentLanguage(): void
	/** 
 Sets command list.
             */
	setCommandList(list: VoiceControlCommand[],type: VoiceControlCommandType): void
	/** 
 Unsets command list.
             */
	unsetCommandList(type: VoiceControlCommandType): void
	/** 
 Registers a listener for getting recognition result.
             */
	addResultListener(listener: VoiceControlResultCallback): void
	/** 
 Unregisters the listener.
             */
	removeResultListener(id: long): void
	/** 
 Registers a callback function to be called when current language is changed.
             */
	addLanguageChangeListener(listener: VoiceControlLanguageChangeCallback): void
	/** 
 Unregisters the callback function.
             */
	removeLanguageChangeListener(id: long): void
	/** 
 Releases all resources.
             */
	release(): void
}

interface VoiceControlCommand {
	/** 
 The command text
             */
	command: DOMString;
	/** 
 The type of the command processing
             */
	type: VoiceControlCommandType;
}

interface VoiceControlLanguageChangeCallback {
	/** 
 Called when default language is changed.
             */
	onlanguagechanged(previous: DOMString,current: DOMString): void
}

interface VoiceControlResultCallback {
	/** 
 Called when client gets the recognition result.
             */
	onresult(event: VoiceControlResultEvent,list: VoiceControlCommand[],results: DOMString): void
}

interface WebSettingObject {
	/** 
 Object representing a web settings manager.
             */
	websetting: WebSettingManager;
}

interface WebSettingManager {
	/** 
 Sets the custom user agent string for your Web application.
             */
	setUserAgentString(userAgent: DOMString,successCallback: SuccessCallback,errorCallback: ErrorCallback): void
	/** 
 Removes all the cookies saved for the Web view in your Web application.
             */
	removeAllCookies(successCallback: SuccessCallback,errorCallback: ErrorCallback): void
}

interface WidgetServiceManagerObject {
	/** 
 Object representing a widget service manager.
             */
	widgetservice: WidgetServiceManager;
}

interface WidgetServiceManager {
	/** 
 Retrieves a Widget object with a given .
             */
	getWidget(widgetId: WidgetId): Widget
	/** 
 Retrieves a list of all widgets. If package id is provided returned list contains widgets included in a given package only.
             */
	getWidgets(successCallback: WidgetArraySuccessCallback,errorCallback: ErrorCallback,packageId: PackageId): void
	/** 
 Returns the primary widget ID of the specified package or application.
             */
	getPrimaryWidgetId(id: union): WidgetId
	/** 
 Returns the size corresponding to the given sizeType.
             */
	getSize(sizeType: WidgetSizeType): WidgetSize
}

interface Widget {
	/** 
 Widget ID.
             */
	id: WidgetId;
	/** 
 Main application ID.
             */
	applicationId: ApplicationId;
	/** 
 Setup application ID.
             */
	setupApplicationId: ApplicationId;
	/** 
 The ID of the package this widget was installed with.
             */
	packageId: PackageId;
	/** 
  if the widget should be hidden in the list of widgets.
             */
	noDisplay: boolean;
	/** 
 Returns a name of the widget in a given locale.
             */
	getName(locale: DOMString): void
	/** 
 Retrieves Widget instances (elements that have been added to the screen). Widget instance as opposed to the widget interface (which is abstract of application), is a specified application.
             */
	getInstances(successCallback: WidgetInstancesCallback,errorCallback: ErrorCallback): void
	/** 
 Returns object representing widget information related to a given sizeType.
             */
	getVariant(sizeType: WidgetSizeType): WidgetVariant
	/** 
 Retrieves Widget Variants representing all of the supported widget size types.
             */
	getVariants(successCallback: WidgetVariantsCallback,errorCallback: ErrorCallback): void
	/** 
 Registers a callback which will be called whenever any of this widget instances state changes.
             */
	addStateChangeListener(listener: WidgetChangeCallback): void
	/** 
 Unregisters a callback registered under the given watchId.
             */
	removeStateChangeListener(watchId: long): void
}

interface WidgetSize {
	/** 
 The horizontal dimension of the area in pixels.
             */
	width: long;
	/** 
 The vertical dimension of the area in pixels.
             */
	height: long;
}

interface WidgetVariant {
	/** 
 The WidgetSizeType this WidgetVariant describes.
             */
	sizeType: WidgetSizeType;
	/** 
 Pixel width.
             */
	width: long;
	/** 
 Pixel height.
             */
	height: long;
	/** 
 The preview image path.
             */
	previewImagePath: DOMString;
	/** 
  if the widget was designed to receive mouse events.
             */
	needsMouseEvents: boolean;
	/** 
  if the widget expects the system to show touch effect.
             */
	needsTouchEffect: boolean;
	/** 
  if the widget expects the system to draw a frame.
             */
	needsFrame: boolean;
}

interface WidgetInstance {
	/** 
 The Widget this instance belongs to.
             */
	widget: Widget;
	/** 
 ID of the widget instance, this value is volatile and may change after reboot.
             */
	id: WidgetInstanceId;
	/** 
 Changes the interval between automatic update of the widget instance data. Minimum value is  second.
             */
	changeUpdatePeriod(seconds: double): void
	/** 
 Sends a new content data to the Widget Instance.
             */
	sendContent(data: Object,updateIfPaused: boolean): void
	/** 
 Retrieves content data from the Widget Instance.
             */
	getContent(successCallback: WidgetContentCallback,errorCallback: ErrorCallback): void
}

interface WidgetArraySuccessCallback {
	/** 
 Called when the array of  objects is retrieved successfully.
             */
	onsuccess(widgets: Widget[]): void
}

interface WidgetInstancesCallback {
	/** 
 Called when the array of  objects is retrieved successfully.
             */
	onsuccess(instances: WidgetInstance[]): void
}

interface WidgetVariantsCallback {
	/** 
 Called when the array of  objects is retrieved successfully.
             */
	onsuccess(instances: WidgetVariant[]): void
}

interface WidgetContentCallback {
	/** 
 Called when the content of the widget instance is retrieved successfully.
             */
	onsuccess(data: Object): void
}

interface WidgetChangeCallback {
	/** 
 Called when the instance state was changed.
             */
	onchange(instance: WidgetInstance,event: WidgetStateType): void
}

type DOMString = string
type long = number
type boolean = Boolean
type double = number
type ResourceType = DOMString
type RawData = DOMString
type CalendarId = DOMString
type MessageConvId = DOMString
type AID = DOMString
type WidgetId = DOMString
type SyncProfileId = DOMString
type ApplicationContextId = DOMString
type ApplicationId = DOMString
type BluetoothLESolicitationUUID = DOMString
type Path = DOMString
type InputDeviceKeyName = DOMString
type MessageId = DOMString
type PackageId = DOMString
type BluetoothUUID = DOMString
type PersonId = DOMString
type CalendarTaskId = DOMString
type ContentId = DOMString
type ContentDirectoryId = DOMString
type PlaylistId = DOMString
type ResourceInterface = DOMString
type MessageAttachmentId = DOMString
type MessageFolderId = DOMString
type NotificationId = DOMString
type BluetoothAddress = DOMString
type WidgetInstanceId = DOMString
type PushRegistrationId = DOMString
type ContactGroupId = DOMString
type ContactId = DOMString
type AddressBookId = DOMString
type AlarmId = DOMString

interface Tizen extends AccountManagerObject,AlarmManagerObject,ApplicationManagerObject,ArchiveManagerObject,BadgeManagerObject,BluetoothManagerObject,CalendarManagerObject,CallHistoryObject,ContactManagerObject,ContentManagerObject,DataControlManagerObject,DataSynchronizationManagerObject,DownloadManagerObject,ExifManagerObject,FeedbackManagerObject,FileSystemManagerObject,FMRadioObject,HumanActivityMonitorManagerObject,InputDeviceManagerObject,IotconObject,KeyManagerObject,MediaControllerObject,MediaKeyManagerObject,MessagePortManagerObject,MessageManagerObject,MetadataObject,MachineLearningManagerObject,NetworkBearerSelectionObject,NFCManagerObject,NotificationObject,PackageManagerObject,PlayerUtilManagerObject,PowerManagerObject,PrivacyPrivilegeManagerObject,PreferenceManagerObject,PushManagerObject,SensorServiceManagerObject,SEServiceManagerObject,SoundManagerObject,SystemInfoObject,SystemSettingObject,TimeManagerObject,VoiceControlClientManagerObject,WebSettingObject,WidgetServiceManagerObject{
}

declare var tizen: Tizen;

