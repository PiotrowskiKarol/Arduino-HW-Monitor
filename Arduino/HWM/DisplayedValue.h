#ifndef DISPLAYED_VALUE_H
#define DISPLAYED_VALUE_H

class DisplayedValue
{
  private:
    String value;
    bool value_changed;

  public:
    DisplayedValue();
    void setValue(String);
    void setValue(int);
    String getValue();
    bool valueChanged();
};

#endif