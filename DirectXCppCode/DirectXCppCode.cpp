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

CPP_API void PrepareScene(int handle, int w, int h, int arrayCount, int* countFe, double* points, bool isColored)
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

		if (!isColored)
		{
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
		}
		else  // закрашиваем
		{
			array<array<int, 3>, 7> colored;
			colored[0] = { 255, 0, 0 };     // red
			colored[1] = { 0, 255, 0 };	  // green
			colored[2] = { 0, 0, 255 };     // blue
			colored[3] = { 128, 0, 128 };   // pink
			colored[4] = { 128, 128, 128 }; // grey
			colored[5] = { 0, 128, 128 };	  // teal
			colored[6] = { 255, 0, 255 };   // purple

			for (int j = 0, k = 0; j < areas.size(); j++, k++)   // countFe
			{
				if (k > 6)
				{
					k = 0;
				}

				for (int i = 0; i < areas[j].size(); i++)
				{
					vector<array<array<int, 3>, 4>> triangles;

					array<array<int, 3>, 4> triangle1;
					triangle1[0] = { 0, 2, 1 };
					triangle1[1] = colored[k];
					triangle1[2] = colored[k];
					triangle1[3] = colored[k];
					triangles.push_back(triangle1);

					array<array<int, 3>, 4> triangle2;
					triangle2[0] = { 2, 1, 3 };
					triangle2[1] = colored[k];
					triangle2[2] = colored[k];
					triangle2[3] = colored[k];
					triangles.push_back(triangle2);

					array<array<int, 3>, 4> triangle3;
					triangle3[0] = { 4, 5, 6 };
					triangle3[1] = colored[k];
					triangle3[2] = colored[k];
					triangle3[3] = colored[k];
					triangles.push_back(triangle3);

					array<array<int, 3>, 4> triangle4;
					triangle4[0] = { 5, 6, 7 };
					triangle4[1] = colored[k];
					triangle4[2] = colored[k];
					triangle4[3] = colored[k];
					triangles.push_back(triangle4);

					array<array<int, 3>, 4> triangle5;
					triangle5[0] = { 8, 9, 10 };
					triangle5[1] = colored[k];
					triangle5[2] = colored[k];
					triangle5[3] = colored[k];
					triangles.push_back(triangle5);

					array<array<int, 3>, 4> triangle6;
					triangle6[0] = { 9, 10, 11 };
					triangle6[1] = colored[k];
					triangle6[2] = colored[k];
					triangle6[3] = colored[k];
					triangles.push_back(triangle6);

					array<array<int, 3>, 4> triangle7;
					triangle7[0] = { 12, 13, 14 };
					triangle7[1] = colored[k];
					triangle7[2] = colored[k];
					triangle7[3] = colored[k];
					triangles.push_back(triangle7);

					array<array<int, 3>, 4> triangle8;
					triangle8[0] = { 13, 14, 15 };
					triangle8[1] = colored[k];
					triangle8[2] = colored[k];
					triangle8[3] = colored[k];
					triangles.push_back(triangle8);

					array<array<int, 3>, 4> triangle9;
					triangle9[0] = { 16, 17, 18 };
					triangle9[1] = colored[k];
					triangle9[2] = colored[k];
					triangle9[3] = colored[k];
					triangles.push_back(triangle9);

					array<array<int, 3>, 4> triangle10;
					triangle10[0] = { 17, 18, 19 };
					triangle10[1] = colored[k];
					triangle10[2] = colored[k];
					triangle10[3] = colored[k];
					triangles.push_back(triangle10);

					array<array<int, 3>, 4> triangle11;
					triangle11[0] = { 20, 21, 22 };
					triangle11[1] = colored[k];
					triangle11[2] = colored[k];
					triangle11[3] = colored[k];
					triangles.push_back(triangle11);

					array<array<int, 3>, 4> triangle12;
					triangle12[0] = { 21, 22, 23 };
					triangle12[1] = colored[k];
					triangle12[2] = colored[k];
					triangle12[3] = colored[k];
					triangles.push_back(triangle12);
		

					vector<array<double, 3>> vertex;
					vertex.resize(8);
					vertex[0] = areas[j][i][0];
					vertex[1] = areas[j][i][1];
					vertex[2] = areas[j][i][2];
					vertex[3] = areas[j][i][3];
					vertex[4] = areas[j][i][4];
					vertex[5] = areas[j][i][5];
					vertex[6] = areas[j][i][6];
					vertex[7] = areas[j][i][7];


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
				}
			}
		}

		finded->second->RenderSavedData(isColored);
		finded->second->EndRender();
	}
}
CPP_API void RenderScene(int handle, double stepChanger, int changerX, int changerY, int changerZ, double coefDepth, bool isColored)
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
		//DirectX::XMMATRIX myViewMatrix = DirectX::XMMatrixTranslation(-0.5f, -0.5f, 0.0f);
		DirectX::XMMATRIX myViewMatrix = DirectX::XMMatrixTranslation(0.0f, 0.0f, -0.5f);


		myViewMatrix *= DirectX::XMMatrixRotationX(stepChanger*changerX);
		myViewMatrix *= DirectX::XMMatrixRotationZ(stepChanger*changerY);
		myViewMatrix *= DirectX::XMMatrixRotationY(stepChanger*changerZ);
		myViewMatrix *= DirectX::XMMatrixScaling(coefDepth, coefDepth, coefDepth);
		
		DirectX::XMStoreFloat4x4(&(finded->second->ModelViewMatrix), myViewMatrix);
		DirectX::XMStoreFloat4x4(&(finded->second->ProjectionMatrix), DirectX::XMMatrixTranslation(0.0f, 0.0f, 0.5f));

		finded->second->RenderSavedData(isColored);
		finded->second->EndRender();
	}
	//counter++;
}

