// OpenGlCppCode.cpp: определ€ет экспортированные функции дл€ приложени€ DLL.
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
	(*device)[(HWND)handle] = make_shared<DX::Directx11>((HWND)handle);
}

CPP_API void PrepareScene(int handle, int w, int h, double* objs, int countLayers)
{
	auto finded = device->find((HWND)handle);

	if (finded != device->end())
	{
		finded->second->CreateTarget(w, h);
		finded->second->ClearAll();

		vector<array<vector<int>, 3>> lines;
		vector<array<double, 3>> xyz;
		vector<array<double, 3>> normals;

			xyz.resize(8);
			// низ
			xyz[0] = { -0.25, -0.25, -0.25 };
			xyz[1] = { 0.25, -0.25, -0.25 };
			xyz[2] = { -0.25, 0.25, -0.25 };
			xyz[3] = { 0.25, 0.25, -0.25 };
			// верх	 
			xyz[4] = { -0.25, -0.25, 0.25 };
			xyz[5] = { 0.25, -0.25, 0.25 };
			xyz[6] = { -0.25, 0.25, 0.25 };
			xyz[7] = { 0.25, 0.25, 0.25 };

			// нижн€€ грань грань (4 точки)
			array<vector<int>, 3> line1;
			line1[0] = { 0, 1 };
			line1[1] = { 255, 0, 0 };
			line1[2] = { 255, 0, 0 };
			lines.push_back(line1);

			array<vector<int>, 3> line2;
			line2[0] = { 2, 3 };
			line2[1] = { 255, 0, 0 };
			line2[2] = { 255, 0, 0 };
			lines.push_back(line2);

			array<vector<int>, 3> line3;
			line3[0] = { 2, 0 };
			line3[1] = { 255, 0, 0 };
			line3[2] = { 255, 0, 0 };
			lines.push_back(line3);

			array<vector<int>, 3> line4;
			line4[0] = { 3, 1 };
			line4[1] = { 255, 0, 0 };
			line4[2] = { 255, 0, 0 };
			lines.push_back(line4);


			// верхн€€ грань грань
			array<vector<int>, 3> line5;
			line5[0] = { 4, 5 };
			line5[1] = { 255, 0, 0 };
			line5[2] = { 255, 0, 0 };
			lines.push_back(line5);

			array<vector<int>, 3> line6;
			line6[0] = { 6, 7 };
			line6[1] = { 255, 0, 0 };
			line6[2] = { 255, 0, 0 };
			lines.push_back(line6);

			array<vector<int>, 3> line7;
			line7[0] = { 4, 6 };
			line7[1] = { 255, 0, 0 };
			line7[2] = { 255, 0, 0 };
			lines.push_back(line7);

			array<vector<int>, 3> line8;
			line8[0] = { 5, 7 };
			line8[1] = { 255, 0, 0 };
			line8[2] = { 255, 0, 0 };
			lines.push_back(line8);

			// линии соедин€ющие грани
			array<vector<int>, 3> line9;
			line9[0] = { 0, 4 };
			line9[1] = { 255, 0, 0 };
			line9[2] = { 255, 0, 0 };
			lines.push_back(line9);

			array<vector<int>, 3> line10;
			line10[0] = { 2, 6 };
			line10[1] = { 255, 0, 0 };
			line10[2] = { 255, 0, 0 };
			lines.push_back(line10);

			array<vector<int>, 3> line11;
			line11[0] = { 1, 5 };
			line11[1] = { 255, 0, 0 };
			line11[2] = { 255, 0, 0 };
			lines.push_back(line11);

			array<vector<int>, 3> line12;
			line12[0] = { 3, 7 };
			line12[1] = { 255, 0, 0 };
			line12[2] = { 255, 0, 0 };
			lines.push_back(line12);

			normals.resize(24);
			normals[0] = { 0, 0, -1 };
			normals[1] = { 0, 0, -1 };
			normals[2] = { 0, 0, -1 };
			normals[3] = { 0, 0, -1 };
			normals[4] = { 0, 0, 1 };
			normals[5] = { 0, 0, 1 };
			normals[6] = { 0, 0, 1 };
			normals[7] = { 0, 0, 1 };
			normals[8] = { 0, 1, 0 };
			normals[9] = { 0, 1, 0 };
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

			auto unit = finded->second->CreateTriangleColorUnit(lines, xyz, normals);
			finded->second->AddToSaved(unit);
			finded->second->RenderSavedData();
			finded->second->EndRender();
	}
}
CPP_API void RenderScene(int handle, double posX, double posY)
{
	auto finded = device->find((HWND)handle);
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

		myViewMatrix *= DirectX::XMMatrixScaling(0.9f, 0.9f, 0.9f);
		myViewMatrix *= DirectX::XMMatrixScaling(0.7f, 0.7f, 0.7f);

		DirectX::XMStoreFloat4x4(&(finded->second->ModelViewMatrix), myViewMatrix);
		DirectX::XMStoreFloat4x4(&(finded->second->ProjectionMatrix), DirectX::XMMatrixTranslation(0.0f, 0.0f, 0.0f));

		finded->second->RenderSavedData();
		finded->second->EndRender();
	}
	counter++;
}

