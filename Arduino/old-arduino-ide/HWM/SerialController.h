#ifndef SERIAL_CONTROLLER_H
#define SERIAL_CONTROLLER_H

class SerialController
{
  public:
    int receivedData;

  public:
    SerialController();
    void init();
    void readData();
  //   void setValue(String);
  //   void setValue(int);
  //   String getValue();
  //   bool valueChanged();
};

#endif