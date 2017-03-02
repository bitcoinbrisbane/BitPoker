#include <stdexcept>

extern "C" { __declspec(dllexport) void CreateRng(int port); }
extern "C" { __declspec(dllexport) int Fill(unsigned char *b, int size); }
extern "C" { __declspec(dllexport) void DeleteRng(); }
