﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
Namespace kunden.ah

    <Persistent("Adelholzener.exp_fotoauswertung_pivot_src")>
    Partial Public Class adelholzener_exp_fotoauswertung_pivot_src
        Inherits XPLiteObject
        Dim fid As Integer
        <Key()>
        Public Property id() As Integer
            Get
                Return fid
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("id", fid, value)
            End Set
        End Property
        Dim fadm_name As String
        Public Property adm_name() As String
            Get
                Return fadm_name
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("adm_name", fadm_name, value)
            End Set
        End Property
        Dim fgebiet As Integer
        Public Property gebiet() As Integer
            Get
                Return fgebiet
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("gebiet", fgebiet, value)
            End Set
        End Property
        Dim fschluessel As String
        <Size(3)>
        Public Property schluessel() As String
            Get
                Return fschluessel
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("schluessel", fschluessel, value)
            End Set
        End Property
        Dim fkategorie As String
        Public Property kategorie() As String
            Get
                Return fkategorie
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("kategorie", fkategorie, value)
            End Set
        End Property
        Dim fcid As Integer
        Public Property cid() As Integer
            Get
                Return fcid
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("cid", fcid, value)
            End Set
        End Property
        Dim fbesuchsdatum As DateTime
        Public Property besuchsdatum() As DateTime
            Get
                Return fbesuchsdatum
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("besuchsdatum", fbesuchsdatum, value)
            End Set
        End Property
        Dim fbesuchsdatum_auswertung As String
        <Size(50)>
        Public Property besuchsdatum_auswertung() As String
            Get
                Return fbesuchsdatum_auswertung
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("besuchsdatum_auswertung", fbesuchsdatum_auswertung, value)
            End Set
        End Property
        Dim fcopydate As DateTime
        Public Property copydate() As DateTime
            Get
                Return fcopydate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("copydate", fcopydate, value)
            End Set
        End Property
        Dim fjahr As Integer
        <Persistent("Jahr")>
        Public Property jahr() As Integer
            Get
                Return fjahr
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("jahr", fjahr, value)
            End Set
        End Property
        Dim fmonat As Integer
        <Persistent("Monat")>
        Public Property monat() As Integer
            Get
                Return fmonat
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("monat", fmonat, value)
            End Set
        End Property
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
    End Class

End Namespace
