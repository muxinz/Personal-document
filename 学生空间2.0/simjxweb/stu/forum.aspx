<%@ Page Title="" Language="C#" MasterPageFile="~/stu/LX.master" AutoEventWireup="true" CodeFile="forum.aspx.cs" Inherits="stu_forum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style=" font-size: 14px;
    height: 58px;
    line-height: 58px;
    margin-top: 10px;
    border-bottom: 1px solid #d0d6d9;
    border-bottom-width: 1px;
    border-bottom-style: solid;
    border-bottom-color: rgb(208, 214, 217);">
      
       <div style="float: left;">
          
        <a href="forum.aspx" style="color: red;border-bottom: 2px solid #f01400;
                border-bottom-width: 2px;
                border-bottom-style: solid;
                border-bottom-color: rgb(240, 20, 0)">最新发帖</a>
                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
        </div> 
         <div style="float: left;margin-left:20px;">
         <a href="forum_a.aspx" style="color: red;border-bottom: 2px solid #f01400;
                border-bottom-width: 2px;
                border-bottom-style: solid;
                border-bottom-color: rgb(240, 20, 0)">最近回帖</a>
             </div>
        <div style="float: right;">
            <div style="position: relative;margin-right: 10px;">
                <span class="cursor: pointer">全部帖子</span>
            </div>

        </div>
    </div>
    <div style=" padding: 0 0 0 64px;
                 padding-top: 0px;
                 padding-right: 0px;
                 padding-bottom: 0px;
                 padding-left: 64px;">
        <div style="position: relative;
                    padding-left: 37px;
                    border-left: 1px solid #d3d7da;
                    border-left-width: 1px;
                    border-left-style: solid;
                    border-left-color: rgb(211, 215, 218);">
         <div style="content: '';
                     position: absolute;
                     width: 1px;
                     height: 20px;
                     left: -1px;
                     top: 0;
                     background-color: #fff;"></div>
            <span style="position: absolute;left: 0;top: 20px;margin-left: -64px;line-height: 16px;font-size: 12px;color: #8a8c8f;">
               
                <b style="font-size: 18px;font-weight: 400;"><asp:Label ID="YEAR" runat="server" ></asp:Label></b>
                <em style="display: block;margin-top: 2px;"><asp:Label ID="DAY" runat="server" Text=""></asp:Label></em>
            </span>
            <asp:DataList ID="DataList1" runat="server">
                <ItemTemplate>
       <div>   
            <div style="padding: 30px 0;padding-top: 30px;padding-right: 0px;padding-bottom: 30px;padding-left: 0px;position: relative;border-bottom: 1px solid #eff1f0;border-bottom-width: 1px; border-bottom-style: solid;border-bottom-color: rgb(239, 241, 240);">
                
                <h3 style="font-size: 18px;color: #12171b;height: 29px;line-height: 29px;position: relative;">
                   <asp:Image ID="Image3" runat="server" Height="36px" Width="45px" ImageUrl="images/order1.jpg"/>&nbsp;
                    <a href="#" target="">
                        <asp:Label ID="friendstuname" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                    </a>
                        <span style="font-size: 14px;color: #787d82;">
                           （ <asp:Label ID="friendlogname" runat="server" Text='<%#Eval("stulogname")%>'></asp:Label>）</span>
                </h3>
                <a href="#" target=""> 
                <div style="padding: 10px 0 22px;padding-top: 10px;padding-right: 0px;padding-bottom: 22px;padding-left: 0px;height: 21px;">
                    <span style="color: #f01400;">
                        <asp:Label ID="time" runat="server" Text='<%#Eval("datatime") %>'></asp:Label></span>
                    <span style="font-size: 14px;color: #787d82;"> 
                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("tittle")%>'></asp:Label></span>

            </div>
                <div style="background-color:WhiteSmoke">
                    <asp:Image ID="Image1" runat="server" width="250" height="150"  ImageUrl='<%#Eval("imagepath") %>'/>
                    <%--<asp:Image ID="Image2" runat="server" width="350" height="150"  ImageUrl="forum_detail.aspx"/>--%>
                    <div style="width:350px;height:150px;float:right;word-break:break-all">
                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("detail") %>' Wid="200px"></asp:Label></div>
                </div>
               </a>
            </div>
           </div>
                    </ItemTemplate>
            </asp:DataList>
                    </div>   
              </div>

</asp:Content>

