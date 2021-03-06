﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SilverlightApplication2.Web
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="PinguinDatabase")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertPion(Pion instance);
    partial void UpdatePion(Pion instance);
    partial void DeletePion(Pion instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["PinguinDatabaseConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Pion> Pions
		{
			get
			{
				return this.GetTable<Pion>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Pion")]
	public partial class Pion : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _Column;
		
		private int _Row;
		
		private int _SpelerID;
		
		private int _Ijsschots;
		
		private int _LobbyID;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnColumnChanging(int value);
    partial void OnColumnChanged();
    partial void OnRowChanging(int value);
    partial void OnRowChanged();
    partial void OnSpelerIDChanging(int value);
    partial void OnSpelerIDChanged();
    partial void OnIjsschotsChanging(int value);
    partial void OnIjsschotsChanged();
    partial void OnLobbyIDChanging(int value);
    partial void OnLobbyIDChanged();
    #endregion
		
		public Pion()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Column]", Storage="_Column", DbType="Int NOT NULL")]
		public int Column
		{
			get
			{
				return this._Column;
			}
			set
			{
				if ((this._Column != value))
				{
					this.OnColumnChanging(value);
					this.SendPropertyChanging();
					this._Column = value;
					this.SendPropertyChanged("Column");
					this.OnColumnChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Row", DbType="Int NOT NULL")]
		public int Row
		{
			get
			{
				return this._Row;
			}
			set
			{
				if ((this._Row != value))
				{
					this.OnRowChanging(value);
					this.SendPropertyChanging();
					this._Row = value;
					this.SendPropertyChanged("Row");
					this.OnRowChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SpelerID", DbType="Int NOT NULL")]
		public int SpelerID
		{
			get
			{
				return this._SpelerID;
			}
			set
			{
				if ((this._SpelerID != value))
				{
					this.OnSpelerIDChanging(value);
					this.SendPropertyChanging();
					this._SpelerID = value;
					this.SendPropertyChanged("SpelerID");
					this.OnSpelerIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Ijsschots", DbType="Int NOT NULL")]
		public int Ijsschots
		{
			get
			{
				return this._Ijsschots;
			}
			set
			{
				if ((this._Ijsschots != value))
				{
					this.OnIjsschotsChanging(value);
					this.SendPropertyChanging();
					this._Ijsschots = value;
					this.SendPropertyChanged("Ijsschots");
					this.OnIjsschotsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LobbyID", DbType="Int NOT NULL")]
		public int LobbyID
		{
			get
			{
				return this._LobbyID;
			}
			set
			{
				if ((this._LobbyID != value))
				{
					this.OnLobbyIDChanging(value);
					this.SendPropertyChanging();
					this._LobbyID = value;
					this.SendPropertyChanged("LobbyID");
					this.OnLobbyIDChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
