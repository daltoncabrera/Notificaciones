using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace ccpsd.notificaciones.client
{
    public  class Svc
    {
     
        private SettingDlg m_Dlg = null;

        public SettingDlg SettingDlg
        {
            get { return m_Dlg; }
        }

        public Svc()
        {
            
        }

        public void Start()
        {
            CloseDlg();
            m_Dlg = SettingDlg.StartUIThread();
        }

        protected void Stop()
        {
            CloseDlg();
        }

    

        protected void CloseDlg()
        {
            if (m_Dlg != null)
            {
                try
                {
                    m_Dlg.Close();
                    m_Dlg.Dispose();
                }
                catch { }
                m_Dlg = null;
            }
        }
    }
}
