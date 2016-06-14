// OpenGlCppCode.cpp: определяет экспортированные функции для приложения DLL.
//

#include "stdafx.h"
#include <exception>
#include <map>
#include <memory>

#include "D3DRender.h"

#pragma comment(lib, "D3d11.lib")
#pragma comment(lib, "D2d1.lib")
#pragma comment(lib, "Dwrite.lib")

#define CPP_EXPORTS_API
#ifdef CPP_EXPORTS_API
#define CPP_API extern "C" __declspec(dllexport)
#else
#define CPP_API __declspec(dllimport)
#endif
using namespace std; 
extern map<HWND, shared_ptr<DX::Directx11>>* device = NULL;
CPP_API void InitDirectX(int handle)
{
	(*device)[(HWND) handle] = make_shared<DX::Directx11>((HWND)handle);
}

CPP_API void PrepareScene(int handle,int w,int h)
{
	auto finded = device->find((HWND) handle);
	
	if (finded != device->end())
	{
		finded->second->CreateTarget(w, h);
		finded->second->ClearAll();
		vector<array<array<int, 3>, 4>> triangles;

		array<array<int, 3>, 4> triangle1;
		triangle1[0] = { 0, 2, 1 };
		triangle1[1] = { 255, 0, 0 };
		triangle1[2] = { 255, 0, 0 };
		triangle1[3] = { 255, 0, 0 };
		triangles.push_back(triangle1);

		array<array<int, 3>, 4> triangle2;
		triangle2[0] = { 2, 1, 3 };
		triangle2[1] = { 255, 0, 0 };
		triangle2[2] = { 255, 0, 0 };
		triangle2[3] = { 255, 0, 0 };
		triangles.push_back(triangle2);

		array<array<int, 3>, 4> triangle3;
		triangle3[0] = { 4, 5, 6 };
		triangle3[1] = { 0, 255, 0 };
		triangle3[2] = { 0, 255, 0 };
		triangle3[3] = { 0, 255, 0 };
		triangles.push_back(triangle3);

		array<array<int, 3>, 4> triangle4;
		triangle4[0] = { 5, 6, 7 };
		triangle4[1] = { 0, 255, 0 };
		triangle4[2] = { 0, 255, 0 };
		triangle4[3] = { 0, 255, 0 };
		triangles.push_back(triangle4);

		array<array<int, 3>, 4> triangle5;
		triangle5[0] = { 8, 9, 10 };
		triangle5[1] = { 0, 0, 255 };
		triangle5[2] = { 0, 0, 255 };
		triangle5[3] = { 0, 0, 255 };
		triangles.push_back(triangle5);

		array<array<int, 3>, 4> triangle6;
		triangle6[0] = { 9, 10, 11 };
		triangle6[1] = { 0, 0, 255 };
		triangle6[2] = { 0, 0, 255 };
		triangle6[3] = { 0, 0, 255 };
		triangles.push_back(triangle6);

		array<array<int, 3>, 4> triangle7;
		triangle7[0] = { 12, 13, 14 };
		triangle7[1] = { 255, 255, 0 };
		triangle7[2] = { 255, 255, 0 };
		triangle7[3] = { 255, 255, 0 };
		triangles.push_back(triangle7);

		array<array<int, 3>, 4> triangle8;
		triangle8[0] = { 13, 14, 15 };
		triangle8[1] = { 255, 255, 0 };
		triangle8[2] = { 255, 255, 0 };
		triangle8[3] = { 255, 255, 0 };
		triangles.push_back(triangle8);

		array<array<int, 3>, 4> triangle9;
		triangle9[0] = { 16, 17, 18 };
		triangle9[1] = { 255, 0, 255 };
		triangle9[2] = { 255, 0, 255 };
		triangle9[3] = { 255, 0, 255 };
		triangles.push_back(triangle9);

		array<array<int, 3>, 4> triangle10;
		triangle10[0] = { 17, 18, 19 };
		triangle10[1] = { 255, 0, 255 };
		triangle10[2] = { 255, 0, 255 };
		triangle10[3] = { 255, 0, 255 };
		triangles.push_back(triangle10);

		array<array<int, 3>, 4> triangle11;
		triangle11[0] = { 20, 21, 22 };
		triangle11[1] = { 0, 255, 255 };
		triangle11[2] = { 0, 255, 255 };
		triangle11[3] = { 0, 255, 255 };
		triangles.push_back(triangle11);

		array<array<int, 3>, 4> triangle12;
		triangle12[0] = { 21, 22, 23 };
		triangle12[1] = { 0, 255, 255 };
		triangle12[2] = { 0, 255, 255 };
		triangle12[3] = { 0, 255, 255 };
		triangles.push_back(triangle12);
		
		double a = 0.7;
		double b = 0.7;
		double c = 0.7;

		vector<array<double, 3>> vertex;
		vertex.resize(8);
		vertex[0] = { -a / 2, -b / 2, 0.5 - c / 2 };
		vertex[1] = { a / 2, -b / 2, 0.5 - c / 2 };
		vertex[2] = { -a / 2, b / 2, 0.5 - c / 2 };
		vertex[3] = { a / 2, b / 2, 0.5 - c / 2 };
		vertex[4] = { -a / 2, -b / 2, 0.5 + c / 2 };
		vertex[5] = { a / 2, -b / 2, 0.5 + c / 2 };
		vertex[6] = { -a / 2, b / 2, 0.5 + c / 2 };
		vertex[7] = { a / 2, b / 2, 0.5 + c / 2 };


		vector<array<double, 3>> xyz;
		xyz.resize(24);
		xyz[0] = vertex[0];
		xyz[1] = vertex[1];
		xyz[2] = vertex[2];
		xyz[3] = vertex[3];
		xyz[4] = vertex[4];
		xyz[5] = vertex[5];
		xyz[6] = vertex[6];
		xyz[7] = vertex[7];
		xyz[8] = vertex[2];
		xyz[9] = vertex[6];
		xyz[10] = vertex[3];
		xyz[11] = vertex[7];
		xyz[12] = vertex[0];
		xyz[13] = vertex[4];
		xyz[14] = vertex[1];
		xyz[15] = vertex[5];
		xyz[16] = vertex[0];
		xyz[17] = vertex[4];
		xyz[18] = vertex[2];
		xyz[19] = vertex[6];
		xyz[20] = vertex[1];
		xyz[21] = vertex[5];
		xyz[22] = vertex[3];
		xyz[23] = vertex[7];


		vector<array<double, 3>> normals;
		normals.resize(24);
		normals[0] = { 0, 0, -1 };
		normals[1] = { 0, 0, -1 };
		normals[2] = { 0, 0, -1 };
		normals[3] = { 0, 0, -1 };
		normals[4] = { 0, 0, 1 };
		normals[5] = { 0, 0, 1 };
		normals[6] = { 0, 0, 1 };
		normals[7] =  { 0, 0, 1 };
		normals[8] =  { 0, 1, 0 };
		normals[9] =  { 0, 1, 0 };
		normals[10] = { 0, 1, 0 };
		normals[11] = { 0, 1, 0 };
		normals[12] = { 0, -1, 0 };
		normals[13] = { 0, -1, 0 };
		normals[14] = { 0, -1, 0 };
		normals[15] = { 0, -1, 0 };
		normals[16] = { -1, 0, 0 };
		normals[17] = { -1, 0, 0 };
		normals[18] = { -1, 0, 0 };
		normals[19] = { -1, 0, 0 };
		normals[20] = { 1, 0, 0 };
		normals[21] = { 1, 0, 0 };
		normals[22] = { 1, 0, 0 };
		normals[23] = { 1, 0, 0 };
		
		finded->second->RenderStart();

		auto unit = finded->second->CreateTriangleColorUnit(triangles, xyz, normals);
		finded->second->AddToSaved(unit);
		finded->second->RenderSavedData();
		finded->second->EndRender();
		
	}
}
CPP_API void RenderScene(int handle, double posX, double posY)
{
	auto finded = device->find((HWND) handle);
	static float counter = 0;
	if (finded != device->end())
	{
		finded->second->RenderStart();
		DirectX::XMMATRIX myViewMatrix = DirectX::XMMatrixTranslation(0.0f, 0.0f, -0.5f);
		if (posX > 260 && posX < 610 && posY > 247 && posY < 494)
		{
			myViewMatrix *= DirectX::XMMatrixRotationZ(0.04*counter);
		}
		else if (posX > 610 && posY > 247)
			myViewMatrix *= DirectX::XMMatrixRotationX(0.04*counter);
		else if (posX < 610 && posY < 247)
			myViewMatrix *= DirectX::XMMatrixRotationY(0.04*counter);
		else if (posX > 610 && posY < 247)
		{
			myViewMatrix *= DirectX::XMMatrixRotationX(0.04*counter);
			myViewMatrix *= DirectX::XMMatrixRotationY(0.04*counter);
		}
		else
		{
			myViewMatrix *= DirectX::XMMatrixRotationX(-0.04*counter);
			myViewMatrix *= DirectX::XMMatrixRotationY(-0.04*counter);
		}

		myViewMatrix *= DirectX::XMMatrixScaling(1.2f, 1.2f, 1.2f);
		DirectX::XMStoreFloat4x4(&(finded->second->ModelViewMatrix), myViewMatrix);
		DirectX::XMStoreFloat4x4(&(finded->second->ProjectionMatrix), DirectX::XMMatrixTranslation(0.0f, 0.0f, 0.5f));

		finded->second->RenderSavedData();
		finded->second->EndRender();
	}
	counter++;
}
