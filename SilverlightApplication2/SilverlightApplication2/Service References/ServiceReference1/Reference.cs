﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.ServiceReference, version 5.0.61118.0
// 
namespace SilverlightApplication2.ServiceReference1 {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Pion", Namespace="http://schemas.datacontract.org/2004/07/SilverlightApplication2.Web")]
    public partial class Pion : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int ColumnField;
        
        private int IDField;
        
        private int IjsschotsField;
        
        private int LobbyIDField;
        
        private int RowField;
        
        private int SpelerIDField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Column {
            get {
                return this.ColumnField;
            }
            set {
                if ((this.ColumnField.Equals(value) != true)) {
                    this.ColumnField = value;
                    this.RaisePropertyChanged("Column");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Ijsschots {
            get {
                return this.IjsschotsField;
            }
            set {
                if ((this.IjsschotsField.Equals(value) != true)) {
                    this.IjsschotsField = value;
                    this.RaisePropertyChanged("Ijsschots");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int LobbyID {
            get {
                return this.LobbyIDField;
            }
            set {
                if ((this.LobbyIDField.Equals(value) != true)) {
                    this.LobbyIDField = value;
                    this.RaisePropertyChanged("LobbyID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Row {
            get {
                return this.RowField;
            }
            set {
                if ((this.RowField.Equals(value) != true)) {
                    this.RowField = value;
                    this.RaisePropertyChanged("Row");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SpelerID {
            get {
                return this.SpelerIDField;
            }
            set {
                if ((this.SpelerIDField.Equals(value) != true)) {
                    this.SpelerIDField = value;
                    this.RaisePropertyChanged("SpelerID");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IService1/DoWork", ReplyAction="http://tempuri.org/IService1/DoWorkResponse")]
        System.IAsyncResult BeginDoWork(System.AsyncCallback callback, object asyncState);
        
        void EndDoWork(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IService1/AddPionToGameSte", ReplyAction="http://tempuri.org/IService1/AddPionToGameSteResponse")]
        System.IAsyncResult BeginAddPionToGameSte(SilverlightApplication2.ServiceReference1.Pion p, System.AsyncCallback callback, object asyncState);
        
        void EndAddPionToGameSte(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : SilverlightApplication2.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<SilverlightApplication2.ServiceReference1.IService1>, SilverlightApplication2.ServiceReference1.IService1 {
        
        private BeginOperationDelegate onBeginDoWorkDelegate;
        
        private EndOperationDelegate onEndDoWorkDelegate;
        
        private System.Threading.SendOrPostCallback onDoWorkCompletedDelegate;
        
        private BeginOperationDelegate onBeginAddPionToGameSteDelegate;
        
        private EndOperationDelegate onEndAddPionToGameSteDelegate;
        
        private System.Threading.SendOrPostCallback onAddPionToGameSteCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> DoWorkCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> AddPionToGameSteCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult SilverlightApplication2.ServiceReference1.IService1.BeginDoWork(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginDoWork(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void SilverlightApplication2.ServiceReference1.IService1.EndDoWork(System.IAsyncResult result) {
            base.Channel.EndDoWork(result);
        }
        
        private System.IAsyncResult OnBeginDoWork(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((SilverlightApplication2.ServiceReference1.IService1)(this)).BeginDoWork(callback, asyncState);
        }
        
        private object[] OnEndDoWork(System.IAsyncResult result) {
            ((SilverlightApplication2.ServiceReference1.IService1)(this)).EndDoWork(result);
            return null;
        }
        
        private void OnDoWorkCompleted(object state) {
            if ((this.DoWorkCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.DoWorkCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void DoWorkAsync() {
            this.DoWorkAsync(null);
        }
        
        public void DoWorkAsync(object userState) {
            if ((this.onBeginDoWorkDelegate == null)) {
                this.onBeginDoWorkDelegate = new BeginOperationDelegate(this.OnBeginDoWork);
            }
            if ((this.onEndDoWorkDelegate == null)) {
                this.onEndDoWorkDelegate = new EndOperationDelegate(this.OnEndDoWork);
            }
            if ((this.onDoWorkCompletedDelegate == null)) {
                this.onDoWorkCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnDoWorkCompleted);
            }
            base.InvokeAsync(this.onBeginDoWorkDelegate, null, this.onEndDoWorkDelegate, this.onDoWorkCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult SilverlightApplication2.ServiceReference1.IService1.BeginAddPionToGameSte(SilverlightApplication2.ServiceReference1.Pion p, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginAddPionToGameSte(p, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void SilverlightApplication2.ServiceReference1.IService1.EndAddPionToGameSte(System.IAsyncResult result) {
            base.Channel.EndAddPionToGameSte(result);
        }
        
        private System.IAsyncResult OnBeginAddPionToGameSte(object[] inValues, System.AsyncCallback callback, object asyncState) {
            SilverlightApplication2.ServiceReference1.Pion p = ((SilverlightApplication2.ServiceReference1.Pion)(inValues[0]));
            return ((SilverlightApplication2.ServiceReference1.IService1)(this)).BeginAddPionToGameSte(p, callback, asyncState);
        }
        
        private object[] OnEndAddPionToGameSte(System.IAsyncResult result) {
            ((SilverlightApplication2.ServiceReference1.IService1)(this)).EndAddPionToGameSte(result);
            return null;
        }
        
        private void OnAddPionToGameSteCompleted(object state) {
            if ((this.AddPionToGameSteCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.AddPionToGameSteCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void AddPionToGameSteAsync(SilverlightApplication2.ServiceReference1.Pion p) {
            this.AddPionToGameSteAsync(p, null);
        }
        
        public void AddPionToGameSteAsync(SilverlightApplication2.ServiceReference1.Pion p, object userState) {
            if ((this.onBeginAddPionToGameSteDelegate == null)) {
                this.onBeginAddPionToGameSteDelegate = new BeginOperationDelegate(this.OnBeginAddPionToGameSte);
            }
            if ((this.onEndAddPionToGameSteDelegate == null)) {
                this.onEndAddPionToGameSteDelegate = new EndOperationDelegate(this.OnEndAddPionToGameSte);
            }
            if ((this.onAddPionToGameSteCompletedDelegate == null)) {
                this.onAddPionToGameSteCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnAddPionToGameSteCompleted);
            }
            base.InvokeAsync(this.onBeginAddPionToGameSteDelegate, new object[] {
                        p}, this.onEndAddPionToGameSteDelegate, this.onAddPionToGameSteCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override SilverlightApplication2.ServiceReference1.IService1 CreateChannel() {
            return new Service1ClientChannel(this);
        }
        
        private class Service1ClientChannel : ChannelBase<SilverlightApplication2.ServiceReference1.IService1>, SilverlightApplication2.ServiceReference1.IService1 {
            
            public Service1ClientChannel(System.ServiceModel.ClientBase<SilverlightApplication2.ServiceReference1.IService1> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginDoWork(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("DoWork", _args, callback, asyncState);
                return _result;
            }
            
            public void EndDoWork(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("DoWork", _args, result);
            }
            
            public System.IAsyncResult BeginAddPionToGameSte(SilverlightApplication2.ServiceReference1.Pion p, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = p;
                System.IAsyncResult _result = base.BeginInvoke("AddPionToGameSte", _args, callback, asyncState);
                return _result;
            }
            
            public void EndAddPionToGameSte(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("AddPionToGameSte", _args, result);
            }
        }
    }
}
