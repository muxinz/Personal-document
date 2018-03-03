<%@ Page Title="" Language="C#" MasterPageFile="~/stu/LX.master" AutoEventWireup="true" CodeFile="forum_a.aspx.cs" Inherits="stu_forum_a" %>

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
            
                    </div>   
              </div>

</asp:Content>

