﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ch.darkink.docker_volume_watcher {
    public partial class DockerVolumeWatcher : ServiceBase {

        private DockerMonitor m_Monitor;

        public DockerVolumeWatcher() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            eventLog.WriteEntry($"Service starting - v{typeof(DockerVolumeWatcher).Assembly.GetName().Version}", EventLogEntryType.Information);

            m_Monitor = new DockerMonitor();
            m_Monitor.Start();
        }

        protected override void OnStop() {
            eventLog.WriteEntry($"Service stopping", EventLogEntryType.Information);

            if (m_Monitor != null) {
                m_Monitor.Stop();
                m_Monitor = null;
            }
        }
    }
}
