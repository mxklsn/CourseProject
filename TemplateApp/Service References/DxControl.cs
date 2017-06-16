using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TemplateApp
{
    public partial class DxControl : UserControl
    {
        bool initialized=false;
        bool enablePaint;
        public bool EnablePaint
        {
            get { return enablePaint; }
            set { 
                    enablePaint = value;  
                    if(enablePaint)
                    {
                        if (!initialized)
                        {
                            DllImportFunctions.InitDirectX((int)Handle);
                            DllImportFunctions.PrepareScene((int)Handle, Width, Height,
                                TemplateApp.Form1.CountArray,
                                TemplateApp.Form1.CountFe, 
                                TemplateApp.Form1.Points,
                                TemplateApp.Form1.isColored);
                            initialized = true;
                        }
                    }
                }
        }
        public DxControl()
        {
            InitializeComponent();
        }
        protected override void OnPaintBackground(PaintEventArgs pevent) { }

        private void DxControl_Paint(object sender, PaintEventArgs e)
        {

            if (initialized)
            {
                DllImportFunctions.RenderScene((int)Handle,
                    TemplateApp.Form1.StepChanger,
                    TemplateApp.Form1.ChangerX,
                    TemplateApp.Form1.ChangerY,
                    TemplateApp.Form1.ChangerZ,
                    TemplateApp.Form1.CoefDepth,
                    TemplateApp.Form1.isColored);


                if (TemplateApp.Form1.DxRefresh)
                {
                    DllImportFunctions.InitDirectX((int)Handle);
                    DllImportFunctions.PrepareScene((int)Handle, Width, Height,
                        TemplateApp.Form1.CountArray,
                        TemplateApp.Form1.CountFe,
                        TemplateApp.Form1.Points,
                        TemplateApp.Form1.isColored);
                    TemplateApp.Form1.DxRefresh = false;
                }
            }
        }

        private void DxControl_SizeChanged(object sender, EventArgs e)
        {
            if (initialized) EnablePaint = true;
            Invalidate();
        }

        private void DxControl_Load(object sender, EventArgs e)
        {

        }        

    }
}
public class DllImportFunctions
{
    [DllImport("DirectXCppCode.Dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "InitDirectX")]
    public static extern void InitDirectX(int hwnd);

    [DllImport("DirectXCppCode.Dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "PrepareScene")]
    public static extern void PrepareScene(int hdc, int w, int h, int countArray, Int32[] countFe, Double[] arrayLayers, bool isColored);

    [DllImport("DirectXCppCode.Dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "RenderScene")]
    public static extern void RenderScene(int hdc, double step, int x, int y, int z, double scale, bool isColored);

}
