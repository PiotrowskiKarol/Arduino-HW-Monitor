#include <map>

class AbstractModelData {
protected:
  // Maximum buffer size for char array
  static const int MAX_BUFFER_SIZE = 50;

public:
  // Virtual destructor (best practice for abstract classes)
  virtual ~AbstractModelData() {}

  virtual std::map<String, String> stringToDictionary(String input) {
    std::map<String, String> resultMap;

    // Split the string by ':'
    int start = 0;
    int end = input.indexOf(':');

    while (start < input.length()) {
      // Get the substring between start and end (or to the end of string)
      String pair;
      if (end == -1) {        pair = input.substring(start);
      } else {
        pair = input.substring(start, end);
      }

      // Find the '=' in this pair
      int equalPos = pair.indexOf('=');
      if (equalPos != -1) {
        String key = pair.substring(0, equalPos);
        String value = pair.substring(equalPos + 1);
        resultMap[key] = value;
      }

      // Move to next pair
      if (end == -1) break;
      start = end + 1;
      end = input.indexOf(':', start);
    }

    return resultMap;
  }

  virtual void extractArrayFromString(String input, String result[], int size) {
  

    // Remove the curly braces
    input = input.substring(1, input.length() - 1);

    // Split the string by comma
    int start = 0;
    int end = input.indexOf(',');

    for (int i = 0; i < size; i++) {
      if (end == -1) {
        result[i] = input.substring(start);
      } else {
        result[i] = input.substring(start, end);
      }

      // Move to next element
      if (end == -1) break;
      start = end + 1;
      end = input.indexOf(',', start);
    }
  }
};