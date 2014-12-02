using System;
using System.Collections.Generic;
using GleamTech.FileUltimate;
using GleamTech.FileUltimate.Util;
using GleamTech.FileUltimate.Web;

namespace GleamTech.FileVista
{
    public partial class DefaultPage : ApplicationPage
    {
        protected string HeaderBackgroundImageUrl, UserImageUrl, MenuDownImageUrl;
        protected string IsAdministrator, UserTitle, UserFullName, IsGroupManager, CanLogout, ResourceBasePath, ActionBasePath;
        protected bool ChangePassword;

        protected override void PageLoad(object sender, EventArgs e)
        {
            if (ApplicationContext.LicenseMode == LicenseMode.NotSet
                || (ApplicationContext.LicenseMode == LicenseMode.Trial && !User.IsTrialReminded))
                Response.Redirect(ApplicationContext.GetAbsolutePath("showdialog.aspx?dialog=License"));

            if (User.ForceChangePassword
                || (!User.PasswordNeverExpires && (DateTime.Now - User.PasswordModifiedTime).Days >= int.Parse(ApplicationContext.Settings["MaximumPasswordAge"])))
                Response.Redirect(ApplicationContext.GetAbsolutePath("showdialog.aspx?dialog=ChangePassword"));

            UserTitle = Language.GetString("FileVista.Label.LoggedInAs", User.Name);
            UserFullName = string.IsNullOrEmpty(User.FullName) ? User.Name : User.FullName;
            IsAdministrator = (User.IsInRole("Administrator")) ? "true" : "false";
            IsGroupManager = (User.IsInRole("GroupManager")) ? "true" : "false";
            CanLogout = (User.LoggedInByExternal || User.LoggedInByWindows) ? "false" : "true";
            ActionBasePath = ApplicationContext.GetActionAbsolutePath();
            ResourceBasePath = ApplicationContext.ResourceBasePath;

            fileManager.DisableHeaderIncludes = true;
            fileManager.FullViewport = true;
            fileManager.ClientLoading = "fileManagerLoading";
            fileManager.UploadMethods = UploadMethodList.Parse(ApplicationContext.Settings["UploadMethods"]);
            //fileManager.TemporaryFolder = ApplicationManager.Configuration["TemporaryFolder"];
            if (!string.IsNullOrEmpty(ApplicationContext.Settings["MaxZipFileSize"]))
                fileManager.MaxZipFileSize = ByteSizeValue.Parse(ApplicationContext.Settings["MaxZipFileSize"] + ApplicationContext.Settings["MaxZipFileSizeUnit"]);
            fileManager.ShowFileExtensions = (ApplicationContext.Settings["ShowFileExtensions"] == "1");
            fileManager.ShowHiddenFilesAndFolders = (ApplicationContext.Settings["ShowHiddenFilesAndFolders"] == "1");
            fileManager.ShowSystemFilesAndFolders = (ApplicationContext.Settings["ShowSystemFilesAndFolders"] == "1");
            fileManager.ShowSystemTypeDescriptions = (ApplicationContext.Settings["ShowSystemTypeDescriptions"] == "1");
            fileManager.SortRootFolders = true;

            foreach (var rootFolder in User.RootFolders.Values)
            {
                var fileManagerRootFolder = new FileManagerRootFolder
                {
                    Name = rootFolder.Name,
                    Location = rootFolder.Location
                };
                fileManager.RootFolders.Add(fileManagerRootFolder);

                foreach (var accessControl in rootFolder.AccessControls.Values)
                {
                    fileManagerRootFolder.AccessControls.Add(new FileManagerAccessControl
                    {
                        Path = accessControl.Path,
                        AllowedPermissions = (FileManagerPermissions)accessControl.AllowedPermissions,
                        DeniedPermissions = (FileManagerPermissions)accessControl.DeniedPermissions,
                        AllowedFileTypes = (accessControl.RestrictFileTypes.HasValue && accessControl.RestrictFileTypes.Value)
                            ? accessControl.AllowedFileTypes
                            : null,
                        DeniedFileTypes = (accessControl.RestrictFileTypes.HasValue && accessControl.RestrictFileTypes.Value)
                            ? accessControl.DeniedFileTypes
                            : null,
                        Quota = (accessControl.LimitDiskUsage.HasValue && accessControl.LimitDiskUsage.Value)
                            ? accessControl.Quota
                            : null
                    });
                }
            }

            fileManager.Expanded += ApplicationContext.FileManagerExpanded;
            fileManager.Listed += ApplicationContext.FileManagerListed;
            fileManager.Failed += ApplicationContext.FileManagerFailed;
            fileManager.Created += ApplicationContext.FileManagerCreated;
            fileManager.Deleted += ApplicationContext.FileManagerDeleted;
            fileManager.Renamed += ApplicationContext.FileManagerRenamed;
            fileManager.Copied += ApplicationContext.FileManagerCopied;
            fileManager.Moved += ApplicationContext.FileManagerMoved;
            fileManager.Compressed += ApplicationContext.FileManagerCompressed;
            fileManager.Extracted += ApplicationContext.FileManagerExtracted;
            fileManager.Uploaded += ApplicationContext.FileManagerUploaded;
            fileManager.Downloaded += ApplicationContext.FileManagerDownloaded;

            ApplicationContext.BindFileManagerState(User, fileManager.StateId);
        }

        protected override void PagePreRender(object sender, EventArgs e)
        {
            string combinedCssResourceKey = ApplicationContext.MapCombinedResourcePath("default-combined.css");
            if (ResourceHandler.GetCombinedResource(combinedCssResourceKey) == null)
            {
                CombinedResource cssResource = new CombinedResource(combinedCssResourceKey);
                foreach (CombinedResourceItem item in ResourceHandler.GetCombinedResource(FileManager.CombinedCssResourceKey).Items)
                    cssResource.AddResource(item);
                cssResource.AddResource(ApplicationContext.ResourceStore, @"Css\Common.css");
                ResourceHandler.RegisterCombinedResource(cssResource);
            }
            IncludeCssFile(ResourceHandler.GetResourceUrl(combinedCssResourceKey));

            string combinedJavaScriptResourceKey = ApplicationContext.MapCombinedResourcePath("default-combined.js");
            if (ResourceHandler.GetCombinedResource(combinedJavaScriptResourceKey) == null)
            {
                CombinedResource javaScriptResource = new CombinedResource(combinedJavaScriptResourceKey);
                foreach (CombinedResourceItem item in ResourceHandler.GetCombinedResource(FileManager.CombinedJavaScriptResourceKey).Items)
                    javaScriptResource.AddResource(item);
                javaScriptResource.AddResource(ApplicationContext.ResourceStore, @"JavaScript\Default.js");
                javaScriptResource.AddResource(ApplicationContext.ResourceStore, @"UI\Header.ContextMenus.xml", stream => XmlToJson.Convert(stream, "headerContextMenusData"));
                ResourceHandler.RegisterCombinedResource(javaScriptResource);
            }
            IncludeJavaScriptFile(ResourceHandler.GetResourceUrl(combinedJavaScriptResourceKey));

            IncludeJavaScriptFile(ResourceHandler.GetResourceUrl(string.Format(ApplicationContext.CombinedLanguageResourceKey, Language.Name)), true);

            HeaderBackgroundImageUrl = ResourceHandler.GetResourceUrl(ApplicationContext.ResourceStore, @"Images\headerbackground.png");
            UserImageUrl = ResourceHandler.GetResourceUrl(ApplicationContext.ResourceStore, @"Images\Icons16\user.png");
            MenuDownImageUrl = ResourceHandler.GetResourceUrl(ApplicationContext.ResourceStore, @"Images\menudown.png");
        }
    }
}
