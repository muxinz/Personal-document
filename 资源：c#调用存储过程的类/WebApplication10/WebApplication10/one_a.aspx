<%@ Page Title="" Language="C#" MasterPageFile="~/one.Master" AutoEventWireup="true" CodeBehind="one_a.aspx.cs" Inherits="WebApplication10.one_a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    aaa
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-color:aqua;height:150px;">bbb<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <asp:TextBox ID="TextBox2" runat="server" Height="19px"></asp:TextBox>
        <asp:TextBox ID="TextBox1" runat="server" Height="102px"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
        <a href ="one_b.aspx">转到下一个界面</a>
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Button" />
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
    </div>
</asp:Content>
