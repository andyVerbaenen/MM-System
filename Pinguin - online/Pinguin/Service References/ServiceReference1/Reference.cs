﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.ServiceReference, version 4.0.60310.0
// 
namespace Pinguin.ServiceReference1 {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Speler", Namespace="http://schemas.datacontract.org/2004/07/WCFServiceWebRole1")]
    public partial class Speler : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<int> _GelijkField;
        
        private System.Nullable<int> _GewonnnenField;
        
        private int _IDField;
        
        private int _LobbyField;
        
        private string _NickNameField;
        
        private System.Nullable<int> _PuntenField;
        
        private string _ReadyField;
        
        private System.Nullable<int> _VerlorenField;
        
        private string _WachtwoordField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> _Gelijk {
            get {
                return this._GelijkField;
            }
            set {
                if ((this._GelijkField.Equals(value) != true)) {
                    this._GelijkField = value;
                    this.RaisePropertyChanged("_Gelijk");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> _Gewonnnen {
            get {
                return this._GewonnnenField;
            }
            set {
                if ((this._GewonnnenField.Equals(value) != true)) {
                    this._GewonnnenField = value;
                    this.RaisePropertyChanged("_Gewonnnen");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int _ID {
            get {
                return this._IDField;
            }
            set {
                if ((this._IDField.Equals(value) != true)) {
                    this._IDField = value;
                    this.RaisePropertyChanged("_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int _Lobby {
            get {
                return this._LobbyField;
            }
            set {
                if ((this._LobbyField.Equals(value) != true)) {
                    this._LobbyField = value;
                    this.RaisePropertyChanged("_Lobby");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string _NickName {
            get {
                return this._NickNameField;
            }
            set {
                if ((object.ReferenceEquals(this._NickNameField, value) != true)) {
                    this._NickNameField = value;
                    this.RaisePropertyChanged("_NickName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> _Punten {
            get {
                return this._PuntenField;
            }
            set {
                if ((this._PuntenField.Equals(value) != true)) {
                    this._PuntenField = value;
                    this.RaisePropertyChanged("_Punten");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string _Ready {
            get {
                return this._ReadyField;
            }
            set {
                if ((object.ReferenceEquals(this._ReadyField, value) != true)) {
                    this._ReadyField = value;
                    this.RaisePropertyChanged("_Ready");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> _Verloren {
            get {
                return this._VerlorenField;
            }
            set {
                if ((this._VerlorenField.Equals(value) != true)) {
                    this._VerlorenField = value;
                    this.RaisePropertyChanged("_Verloren");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string _Wachtwoord {
            get {
                return this._WachtwoordField;
            }
            set {
                if ((object.ReferenceEquals(this._WachtwoordField, value) != true)) {
                    this._WachtwoordField = value;
                    this.RaisePropertyChanged("_Wachtwoord");
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
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IService1/GetSpelers", ReplyAction="http://tempuri.org/IService1/GetSpelersResponse")]
        System.IAsyncResult BeginGetSpelers(System.AsyncCallback callback, object asyncState);
        
        System.Collections.ObjectModel.ObservableCollection<Pinguin.ServiceReference1.Speler> EndGetSpelers(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        System.IAsyncResult BeginGetData(int value, System.AsyncCallback callback, object asyncState);
        
        string EndGetData(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : Pinguin.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetSpelersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetSpelersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.Collections.ObjectModel.ObservableCollection<Pinguin.ServiceReference1.Speler> Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.Collections.ObjectModel.ObservableCollection<Pinguin.ServiceReference1.Speler>)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<Pinguin.ServiceReference1.IService1>, Pinguin.ServiceReference1.IService1 {
        
        private BeginOperationDelegate onBeginGetSpelersDelegate;
        
        private EndOperationDelegate onEndGetSpelersDelegate;
        
        private System.Threading.SendOrPostCallback onGetSpelersCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetDataDelegate;
        
        private EndOperationDelegate onEndGetDataDelegate;
        
        private System.Threading.SendOrPostCallback onGetDataCompletedDelegate;
        
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
        
        public event System.EventHandler<GetSpelersCompletedEventArgs> GetSpelersCompleted;
        
        public event System.EventHandler<GetDataCompletedEventArgs> GetDataCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Pinguin.ServiceReference1.IService1.BeginGetSpelers(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetSpelers(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Collections.ObjectModel.ObservableCollection<Pinguin.ServiceReference1.Speler> Pinguin.ServiceReference1.IService1.EndGetSpelers(System.IAsyncResult result) {
            return base.Channel.EndGetSpelers(result);
        }
        
        private System.IAsyncResult OnBeginGetSpelers(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((Pinguin.ServiceReference1.IService1)(this)).BeginGetSpelers(callback, asyncState);
        }
        
        private object[] OnEndGetSpelers(System.IAsyncResult result) {
            System.Collections.ObjectModel.ObservableCollection<Pinguin.ServiceReference1.Speler> retVal = ((Pinguin.ServiceReference1.IService1)(this)).EndGetSpelers(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetSpelersCompleted(object state) {
            if ((this.GetSpelersCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetSpelersCompleted(this, new GetSpelersCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetSpelersAsync() {
            this.GetSpelersAsync(null);
        }
        
        public void GetSpelersAsync(object userState) {
            if ((this.onBeginGetSpelersDelegate == null)) {
                this.onBeginGetSpelersDelegate = new BeginOperationDelegate(this.OnBeginGetSpelers);
            }
            if ((this.onEndGetSpelersDelegate == null)) {
                this.onEndGetSpelersDelegate = new EndOperationDelegate(this.OnEndGetSpelers);
            }
            if ((this.onGetSpelersCompletedDelegate == null)) {
                this.onGetSpelersCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetSpelersCompleted);
            }
            base.InvokeAsync(this.onBeginGetSpelersDelegate, null, this.onEndGetSpelersDelegate, this.onGetSpelersCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Pinguin.ServiceReference1.IService1.BeginGetData(int value, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetData(value, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string Pinguin.ServiceReference1.IService1.EndGetData(System.IAsyncResult result) {
            return base.Channel.EndGetData(result);
        }
        
        private System.IAsyncResult OnBeginGetData(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int value = ((int)(inValues[0]));
            return ((Pinguin.ServiceReference1.IService1)(this)).BeginGetData(value, callback, asyncState);
        }
        
        private object[] OnEndGetData(System.IAsyncResult result) {
            string retVal = ((Pinguin.ServiceReference1.IService1)(this)).EndGetData(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetDataCompleted(object state) {
            if ((this.GetDataCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetDataCompleted(this, new GetDataCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetDataAsync(int value) {
            this.GetDataAsync(value, null);
        }
        
        public void GetDataAsync(int value, object userState) {
            if ((this.onBeginGetDataDelegate == null)) {
                this.onBeginGetDataDelegate = new BeginOperationDelegate(this.OnBeginGetData);
            }
            if ((this.onEndGetDataDelegate == null)) {
                this.onEndGetDataDelegate = new EndOperationDelegate(this.OnEndGetData);
            }
            if ((this.onGetDataCompletedDelegate == null)) {
                this.onGetDataCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetDataCompleted);
            }
            base.InvokeAsync(this.onBeginGetDataDelegate, new object[] {
                        value}, this.onEndGetDataDelegate, this.onGetDataCompletedDelegate, userState);
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
        
        protected override Pinguin.ServiceReference1.IService1 CreateChannel() {
            return new Service1ClientChannel(this);
        }
        
        private class Service1ClientChannel : ChannelBase<Pinguin.ServiceReference1.IService1>, Pinguin.ServiceReference1.IService1 {
            
            public Service1ClientChannel(System.ServiceModel.ClientBase<Pinguin.ServiceReference1.IService1> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginGetSpelers(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("GetSpelers", _args, callback, asyncState);
                return _result;
            }
            
            public System.Collections.ObjectModel.ObservableCollection<Pinguin.ServiceReference1.Speler> EndGetSpelers(System.IAsyncResult result) {
                object[] _args = new object[0];
                System.Collections.ObjectModel.ObservableCollection<Pinguin.ServiceReference1.Speler> _result = ((System.Collections.ObjectModel.ObservableCollection<Pinguin.ServiceReference1.Speler>)(base.EndInvoke("GetSpelers", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginGetData(int value, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = value;
                System.IAsyncResult _result = base.BeginInvoke("GetData", _args, callback, asyncState);
                return _result;
            }
            
            public string EndGetData(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("GetData", _args, result)));
                return _result;
            }
        }
    }
}