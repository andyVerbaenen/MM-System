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
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="ServiceReference1.Service1")]
    public interface Service1 {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:Service1/RolDobbelsteen", ReplyAction="urn:Service1/RolDobbelsteenResponse")]
        System.IAsyncResult BeginRolDobbelsteen(System.AsyncCallback callback, object asyncState);
        
        int EndRolDobbelsteen(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:Service1/MakeMap", ReplyAction="urn:Service1/MakeMapResponse")]
        System.IAsyncResult BeginMakeMap(int rows, int columns, System.AsyncCallback callback, object asyncState);
        
        void EndMakeMap(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:Service1/MijnTestString", ReplyAction="urn:Service1/MijnTestStringResponse")]
        System.IAsyncResult BeginMijnTestString(System.AsyncCallback callback, object asyncState);
        
        string EndMijnTestString(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Service1Channel : Pinguin.ServiceReference1.Service1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RolDobbelsteenCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public RolDobbelsteenCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public int Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MijnTestStringCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public MijnTestStringCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class Service1Client : System.ServiceModel.ClientBase<Pinguin.ServiceReference1.Service1>, Pinguin.ServiceReference1.Service1 {
        
        private BeginOperationDelegate onBeginRolDobbelsteenDelegate;
        
        private EndOperationDelegate onEndRolDobbelsteenDelegate;
        
        private System.Threading.SendOrPostCallback onRolDobbelsteenCompletedDelegate;
        
        private BeginOperationDelegate onBeginMakeMapDelegate;
        
        private EndOperationDelegate onEndMakeMapDelegate;
        
        private System.Threading.SendOrPostCallback onMakeMapCompletedDelegate;
        
        private BeginOperationDelegate onBeginMijnTestStringDelegate;
        
        private EndOperationDelegate onEndMijnTestStringDelegate;
        
        private System.Threading.SendOrPostCallback onMijnTestStringCompletedDelegate;
        
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
        
        public event System.EventHandler<RolDobbelsteenCompletedEventArgs> RolDobbelsteenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> MakeMapCompleted;
        
        public event System.EventHandler<MijnTestStringCompletedEventArgs> MijnTestStringCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Pinguin.ServiceReference1.Service1.BeginRolDobbelsteen(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginRolDobbelsteen(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        int Pinguin.ServiceReference1.Service1.EndRolDobbelsteen(System.IAsyncResult result) {
            return base.Channel.EndRolDobbelsteen(result);
        }
        
        private System.IAsyncResult OnBeginRolDobbelsteen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((Pinguin.ServiceReference1.Service1)(this)).BeginRolDobbelsteen(callback, asyncState);
        }
        
        private object[] OnEndRolDobbelsteen(System.IAsyncResult result) {
            int retVal = ((Pinguin.ServiceReference1.Service1)(this)).EndRolDobbelsteen(result);
            return new object[] {
                    retVal};
        }
        
        private void OnRolDobbelsteenCompleted(object state) {
            if ((this.RolDobbelsteenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.RolDobbelsteenCompleted(this, new RolDobbelsteenCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void RolDobbelsteenAsync() {
            this.RolDobbelsteenAsync(null);
        }
        
        public void RolDobbelsteenAsync(object userState) {
            if ((this.onBeginRolDobbelsteenDelegate == null)) {
                this.onBeginRolDobbelsteenDelegate = new BeginOperationDelegate(this.OnBeginRolDobbelsteen);
            }
            if ((this.onEndRolDobbelsteenDelegate == null)) {
                this.onEndRolDobbelsteenDelegate = new EndOperationDelegate(this.OnEndRolDobbelsteen);
            }
            if ((this.onRolDobbelsteenCompletedDelegate == null)) {
                this.onRolDobbelsteenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnRolDobbelsteenCompleted);
            }
            base.InvokeAsync(this.onBeginRolDobbelsteenDelegate, null, this.onEndRolDobbelsteenDelegate, this.onRolDobbelsteenCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Pinguin.ServiceReference1.Service1.BeginMakeMap(int rows, int columns, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginMakeMap(rows, columns, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void Pinguin.ServiceReference1.Service1.EndMakeMap(System.IAsyncResult result) {
            base.Channel.EndMakeMap(result);
        }
        
        private System.IAsyncResult OnBeginMakeMap(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int rows = ((int)(inValues[0]));
            int columns = ((int)(inValues[1]));
            return ((Pinguin.ServiceReference1.Service1)(this)).BeginMakeMap(rows, columns, callback, asyncState);
        }
        
        private object[] OnEndMakeMap(System.IAsyncResult result) {
            ((Pinguin.ServiceReference1.Service1)(this)).EndMakeMap(result);
            return null;
        }
        
        private void OnMakeMapCompleted(object state) {
            if ((this.MakeMapCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.MakeMapCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void MakeMapAsync(int rows, int columns) {
            this.MakeMapAsync(rows, columns, null);
        }
        
        public void MakeMapAsync(int rows, int columns, object userState) {
            if ((this.onBeginMakeMapDelegate == null)) {
                this.onBeginMakeMapDelegate = new BeginOperationDelegate(this.OnBeginMakeMap);
            }
            if ((this.onEndMakeMapDelegate == null)) {
                this.onEndMakeMapDelegate = new EndOperationDelegate(this.OnEndMakeMap);
            }
            if ((this.onMakeMapCompletedDelegate == null)) {
                this.onMakeMapCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnMakeMapCompleted);
            }
            base.InvokeAsync(this.onBeginMakeMapDelegate, new object[] {
                        rows,
                        columns}, this.onEndMakeMapDelegate, this.onMakeMapCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Pinguin.ServiceReference1.Service1.BeginMijnTestString(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginMijnTestString(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string Pinguin.ServiceReference1.Service1.EndMijnTestString(System.IAsyncResult result) {
            return base.Channel.EndMijnTestString(result);
        }
        
        private System.IAsyncResult OnBeginMijnTestString(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((Pinguin.ServiceReference1.Service1)(this)).BeginMijnTestString(callback, asyncState);
        }
        
        private object[] OnEndMijnTestString(System.IAsyncResult result) {
            string retVal = ((Pinguin.ServiceReference1.Service1)(this)).EndMijnTestString(result);
            return new object[] {
                    retVal};
        }
        
        private void OnMijnTestStringCompleted(object state) {
            if ((this.MijnTestStringCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.MijnTestStringCompleted(this, new MijnTestStringCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void MijnTestStringAsync() {
            this.MijnTestStringAsync(null);
        }
        
        public void MijnTestStringAsync(object userState) {
            if ((this.onBeginMijnTestStringDelegate == null)) {
                this.onBeginMijnTestStringDelegate = new BeginOperationDelegate(this.OnBeginMijnTestString);
            }
            if ((this.onEndMijnTestStringDelegate == null)) {
                this.onEndMijnTestStringDelegate = new EndOperationDelegate(this.OnEndMijnTestString);
            }
            if ((this.onMijnTestStringCompletedDelegate == null)) {
                this.onMijnTestStringCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnMijnTestStringCompleted);
            }
            base.InvokeAsync(this.onBeginMijnTestStringDelegate, null, this.onEndMijnTestStringDelegate, this.onMijnTestStringCompletedDelegate, userState);
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
        
        protected override Pinguin.ServiceReference1.Service1 CreateChannel() {
            return new Service1ClientChannel(this);
        }
        
        private class Service1ClientChannel : ChannelBase<Pinguin.ServiceReference1.Service1>, Pinguin.ServiceReference1.Service1 {
            
            public Service1ClientChannel(System.ServiceModel.ClientBase<Pinguin.ServiceReference1.Service1> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginRolDobbelsteen(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("RolDobbelsteen", _args, callback, asyncState);
                return _result;
            }
            
            public int EndRolDobbelsteen(System.IAsyncResult result) {
                object[] _args = new object[0];
                int _result = ((int)(base.EndInvoke("RolDobbelsteen", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginMakeMap(int rows, int columns, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[2];
                _args[0] = rows;
                _args[1] = columns;
                System.IAsyncResult _result = base.BeginInvoke("MakeMap", _args, callback, asyncState);
                return _result;
            }
            
            public void EndMakeMap(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("MakeMap", _args, result);
            }
            
            public System.IAsyncResult BeginMijnTestString(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("MijnTestString", _args, callback, asyncState);
                return _result;
            }
            
            public string EndMijnTestString(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("MijnTestString", _args, result)));
                return _result;
            }
        }
    }
}
