// Win32Random.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Win32Random.h"
#include "rng.h"

RandomFromTrueRNG *g_rng;

__declspec(dllexport) void CreateRng(int port)
{
	g_rng = new RandomFromTrueRNG(port);
}

__declspec(dllexport) int Fill(unsigned char * b, int size)
{
	return g_rng->fill(b, size);
}

__declspec(dllexport) void DeleteRng()
{
	delete g_rng;
}