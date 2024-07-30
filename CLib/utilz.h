#pragma once

#ifdef TOMMY_DLL_BUILD
	#define TOMMY_UTILZ_API __declspec(dllexport)
#else 
	#define TOMMY_UTILZ_API __declspec(dllimport)
#endif

void TOMMY_UTILZ_API log(const char* msg);
