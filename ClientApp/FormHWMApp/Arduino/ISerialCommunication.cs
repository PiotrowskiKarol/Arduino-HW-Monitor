namespace FormHWPApp.Arduino
{
    public interface ISerialCommunication
    {
        void startCommunication();
        void stopCommunication();
        void sendData(string text);
        void readData();
    }
}