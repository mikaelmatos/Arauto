﻿using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arauto
{
    public partial class Browser : Form
    {
        int IdConta = -1;
        string URL = "";
        public Browser(int IdConta, string uRL = "")
        {
            this.IdConta = IdConta;
            InitializeComponent();
            URL = uRL;
        }

        private async void Browser_Load(object sender, EventArgs e)
        {
            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebView2UserData0" + IdConta);

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            await webView21.EnsureCoreWebView2Async(environment);

            if (URL == "")
            {
                webView21.CoreWebView2.Navigate("https://myaccount.google.com/?utm_source=OGB&utm_medium=app");
            }
            else
            {
                webView21.CoreWebView2.Navigate(URL);
            }

            webView21.CoreWebView2.NavigationCompleted += async (sender, e) =>
            {
                string script = "window.location.href";
                string result = await webView21.CoreWebView2.ExecuteScriptAsync(script);

                textBox1.Text = result.Trim('"').Trim('"').Trim().Trim('"').Trim();

                int indice = 0;
                script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                result = await webView21.CoreWebView2.ExecuteScriptAsync(script);
                bool buscarPerfil = true;

                while (buscarPerfil)
                {
                    script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                    result = await webView21.CoreWebView2.ExecuteScriptAsync(script);
                    if (result.Contains("usercontent"))
                    {
                        buscarPerfil = false;
                        break;
                    }
                    indice++;
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.Navigate(textBox1.Text);
        }
    }
}
