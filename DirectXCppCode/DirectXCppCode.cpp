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

CPP_API void PrepareScene(int handle, int w, int h, int arrayCount, int* countFe, double* points)
{
	auto finded = device->find((HWND)handle);

	std::vector<std::array<double, 3>> normals;
	normals.resize(6);
	normals[0] = { 0, 0, -1 };
	normals[1] = { 0, 0, 1 };
	normals[2] = { 0, 1, 0 };
	normals[3] = { 0, -1, 0 };
	normals[4] = { -1, 0, 0 };
	normals[5] = { 1, 0, 0 };

	array<vector<int>, 7> color;
	color[0] = { 255, 0, 0 };     // red
	color[1] = { 0, 255, 0 };	  // green
	color[2] = { 0, 0, 255 };     // blue
	color[3] = { 128, 0, 128 };   // pink
	color[4] = { 128, 128, 128 }; // grey
	color[5] = { 0, 128, 128 };	  // teal
	color[6] = { 255, 0, 255 };   // purple

	int countKe = 0;  // общее кол-во всех  Ё
	for (int i = 0; i < arrayCount; i++)
	{
		countKe += countFe[i];
	}

	vector<vector<array<double, 3>>> elements;  // все точки  Ё
	for (auto i = 0; i < countKe; i++)
	{
		vector<array<double, 3>> element;
		for (auto j = 0; j < 8; j++)
		{
			array<double, 3> point;
			for (auto k = 0; k < 3; k++)
			{
				point[k] = points[j * 3 + 24 * i + k];
			}
			element.push_back(point);
		}
		elements.push_back(element);
	}


	vector<vector<vector<array<double, 3>>>> areas;  // все области с  Ё
	areas.resize(arrayCount);
	int elem = 0;
	for (int i = 0; i < arrayCount; i++)
	{
		vector<vector<array<double, 3>>> area;
		area.resize(countFe[i]);
		for (int j = 0; j < countFe[i]; j++)
		{
			area[j] = elements[j + elem];
		}
		areas.push_back(area);
		elem += countFe[i];
	}


	if (finded != device->end())
	{
		finded->second->CreateTarget(w, h);
		finded->second->ClearAll();

		for (int j = 0, k = 0; j < areas.size(); j++, k++)   // countFe
		{
			if (k > 6)
			{
				k = 0;
			}

			for (int i = 0; i < areas[j].size(); i++)
			{
				vector<array<vector<int>, 3>> lines;
				// нижн€€ грань грань (4 точки)
				array<vector<int>, 3> line1;
				line1[0] = { 0, 1 };
				line1[1] = color[k];
				line1[2] = color[k];
				lines.push_back(line1);

				array<vector<int>, 3> line2;
				line2[0] = { 2, 3 };
				line2[1] = color[k];
				line2[2] = color[k];
				lines.push_back(line2);

				array<vector<int>, 3> line3;
				line3[0] = { 2, 0 };
				line3[1] = color[k];
				line3[2] = color[k];
				lines.push_back(line3);

				array<vector<int>, 3> line4;
				line4[0] = { 3, 1 };
				line4[1] = color[k];
				line4[2] = color[k];
				lines.push_back(line4);


				// верхн€€ грань грань
				array<vector<int>, 3> line5;
				line5[0] = { 4, 5 };
				line5[1] = color[k];
				line5[2] = color[k];
				lines.push_back(line5);

				array<vector<int>, 3> line6;
				line6[0] = { 6, 7 };
				line6[1] = color[k];
				line6[2] = color[k];
				lines.push_back(line6);

				array<vector<int>, 3> line7;
				line7[0] = { 4, 6 };
				line7[1] = color[k];
				line7[2] = color[k];
				lines.push_back(line7);

				array<vector<int>, 3> line8;
				line8[0] = { 5, 7 };
				line8[1] = color[k];
				line8[2] = color[k];
				lines.push_back(line8);

				// линии соедин€ющие грани
				array<vector<int>, 3> line9;
				line9[0] = { 0, 4 };
				line9[1] = color[k];
				line9[2] = color[k];
				lines.push_back(line9);

				array<vector<int>, 3> line10;
				line10[0] = { 2, 6 };
				line10[1] = color[k];
				line10[2] = color[k];
				lines.push_back(line10);

				array<vector<int>, 3> line11;
				line11[0] = { 1, 5 };
				line11[1] = color[k];
				line11[2] = color[k];
				lines.push_back(line11);

				array<vector<int>, 3> line12;
				line12[0] = { 3, 7 };
				line12[1] = color[k];
				line12[2] = color[k];
				lines.push_back(line12);

				finded->second->RenderStart();

				auto unit = finded->second->CreateLineColorUnit(lines, areas[j][i], normals);
				finded->second->AddToSaved(unit);
			}
		}
		finded->second->RenderSavedData();
		finded->second->EndRender();
	}
}
CPP_API void RenderScene(int handle, double stepChanger, int changerX, int changerY, int changerZ, double coefDepth)
{
	auto finded = device->find((HWND)handle);
	//static float counter = 0;

	/*if (coefDepth > 1)
	{
		coefDepth = 0.9;
	}
	else if (coefDepth < 0.1)
	{
		coefDepth = 0.1;
	}*/

	if (finded != device->end())
	{
		finded->second->RenderStart();
		DirectX::XMMATRIX myViewMatrix = DirectX::XMMatrixTranslation(-0.5f, -0.5f, 0.0f);

		myViewMatrix *= DirectX::XMMatrixRotationX(stepChanger*changerX);
		myViewMatrix *= DirectX::XMMatrixRotationZ(stepChanger*changerY);
		myViewMatrix *= DirectX::XMMatrixRotationY(stepChanger*changerZ);
		myViewMatrix *= DirectX::XMMatrixScaling(coefDepth, coefDepth, coefDepth);

	/*auto finded = device->find((HWND)handle);
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
		myViewMatrix *= DirectX::XMMatrixScaling(0.7f, 0.7f, 0.7f);*/

		DirectX::XMStoreFloat4x4(&(finded->second->ModelViewMatrix), myViewMatrix);
		DirectX::XMStoreFloat4x4(&(finded->second->ProjectionMatrix), DirectX::XMMatrixTranslation(0.0f, 0.0f, 0.5f));

		finded->second->RenderSavedData();
		finded->second->EndRender();
	}
	//counter++;
}

