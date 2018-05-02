<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="stu_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="用户名"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" Height="22px" Width="270px"></asp:TextBox>
    
    </div>
        <asp:Label ID="Label2" runat="server" Text="密码"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" style="margin-left: 17px" Width="275px"></asp:TextBox>
        <asp:Image ID="Image1" runat="server" Height="33px" Width="110px"  />
        <p>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="获取验证码" />
            <asp:TextBox ID="TextBox3" runat="server" style="margin-left: 62px" Width="276px"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="登录" Width="113px" />
        </p>
    </form>
</body>
</html>
