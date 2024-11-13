#include <Arduino.h>
#include "SerialController.h"
#include "ScreenModelData.h"

SerialController::SerialController() {
}

void SerialController::init() {
  receivedData = 0;
  Serial.begin(9600);
  Serial.setTimeout(10);
}

void SerialController::readData() {
  if (Serial.available() > 0) {
    String str = Serial.readString();
    str.trim();
    Serial.print("Received message: ");
    Serial.println(str);
    receivedData++;

    if (str.charAt(0) == 'C') {
      //executeCommand(str.substring(2));
      ScreenModelData model = ScreenModelData(str.substring(2));
    } else if (str.charAt(0) == 'D') {
      //Serial.println("Received data...");
      //drawCPU(str.substring(2));
    }
  }
}


