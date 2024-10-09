#ifndef FPS_COUNTER_H
#define FPS_COUNTER_H

class FPSCounter
{
  private:
    unsigned long millis_fps_time_stamp;
    int procced_loops;
    int fps;
    bool valueUpdated;

  public:
    FPSCounter();  // Constructor
    void countFps();  // Method to count FPS
    int getFps(); // Method to retrieve the FPS value
    bool fpsChanged();  // Check if FPS value changed since last getFps() method call
};

#endif