USE [ChristianBeautyDb]
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([Id], [RoleName]) VALUES (1, N'Admin')
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Password], [UserRoleId]) VALUES (1, N'admin', N'123', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId]) VALUES (8, N'برس', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId]) VALUES (9, N'بچه گانه', 8)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId]) VALUES (10, N'بزرگسال', 8)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId]) VALUES (11, N'مژه مصنوعی', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId]) VALUES (12, N'ناخن مصنوعی', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId]) VALUES (13, N'علی میگه ساب ادد کن', 11)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId]) VALUES (14, N'علی میگه واس اون یکی ام بزار', 12)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId]) VALUES (15, N'کلیپس', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId]) VALUES (16, N'آینه', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId]) VALUES (17, N'شونه', NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Materials] ON 

INSERT [dbo].[Materials] ([Id], [Name]) VALUES (2, N'چوبی')
INSERT [dbo].[Materials] ([Id], [Name]) VALUES (3, N'آهنی')
INSERT [dbo].[Materials] ([Id], [Name]) VALUES (4, N'پلاستیکی')
SET IDENTITY_INSERT [dbo].[Materials] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [IsAvailable], [ProductCode], [Color], [Description], [Size], [Weight], [Features], [OtherDetails], [DigikalLink], [BasalamLink], [CategoryId], [MaterialId], [IsEnable], [CreatedDate], [IsTopSeller]) VALUES (18, N'برس G12', 0, N'1123', NULL, N'این برس عالیع', NULL, NULL, N'#چوبی#خوش_ساخت', N'جزئیات محصول', N'wwwsadasdsad', NULL, 8, 2, 1, CAST(N'2023-07-18T17:06:07.8885835' AS DateTime2), 1)
INSERT [dbo].[Products] ([Id], [Name], [IsAvailable], [ProductCode], [Color], [Description], [Size], [Weight], [Features], [OtherDetails], [DigikalLink], [BasalamLink], [CategoryId], [MaterialId], [IsEnable], [CreatedDate], [IsTopSeller]) VALUES (19, N'برس G13', 0, N'1124', NULL, N'مژه مصنوعی با کیفیت عالی', 11, CAST(112.00 AS Decimal(18, 2)), N'#چوبی#خوش_ساخت', N'بشسیسشیشسیسشیسش', N'wwwsadasdsad', N'sadsadsadsdsa', 11, 3, 1, CAST(N'2023-07-18T17:11:45.6278440' AS DateTime2), 1)
INSERT [dbo].[Products] ([Id], [Name], [IsAvailable], [ProductCode], [Color], [Description], [Size], [Weight], [Features], [OtherDetails], [DigikalLink], [BasalamLink], [CategoryId], [MaterialId], [IsEnable], [CreatedDate], [IsTopSeller]) VALUES (20, N'برس G14', 0, N'1123', NULL, N'این برس عالیع', 12, CAST(112.00 AS Decimal(18, 2)), N'#چوبی#خوش_ساخت', N'بشسیسشیشسیسشیسش', N'wwwsadasdsad', N'sadsadsadsdsa', 11, 2, 1, CAST(N'2023-07-18T17:12:35.6129304' AS DateTime2), 1)
INSERT [dbo].[Products] ([Id], [Name], [IsAvailable], [ProductCode], [Color], [Description], [Size], [Weight], [Features], [OtherDetails], [DigikalLink], [BasalamLink], [CategoryId], [MaterialId], [IsEnable], [CreatedDate], [IsTopSeller]) VALUES (21, N'برس G15', 0, N'1123', NULL, N'این برس عالیع', 12, CAST(23.00 AS Decimal(18, 2)), N'چوبی', N'بشسیسشیشسیسشیسش', N'wwwsadasdsad', N'sadsadsadsdsa', 8, 3, 1, CAST(N'2023-07-18T17:13:13.1741284' AS DateTime2), 0)
INSERT [dbo].[Products] ([Id], [Name], [IsAvailable], [ProductCode], [Color], [Description], [Size], [Weight], [Features], [OtherDetails], [DigikalLink], [BasalamLink], [CategoryId], [MaterialId], [IsEnable], [CreatedDate], [IsTopSeller]) VALUES (22, N'برس G16', 0, N'1123', NULL, N'این برس عالیع', 1, CAST(112.00 AS Decimal(18, 2)), N'چوبی', N'بشسیسشیشسیسشیسش', N'wwwsadasdsad', N'sadsadsadsdsa', 9, 2, 1, CAST(N'2023-07-18T17:13:52.6201260' AS DateTime2), 1)
INSERT [dbo].[Products] ([Id], [Name], [IsAvailable], [ProductCode], [Color], [Description], [Size], [Weight], [Features], [OtherDetails], [DigikalLink], [BasalamLink], [CategoryId], [MaterialId], [IsEnable], [CreatedDate], [IsTopSeller]) VALUES (23, N'برس G19', 0, N'1123', NULL, N'این برس عالیع', 21, CAST(32.00 AS Decimal(18, 2)), N'چوبی', N'بشسیسشیشسیسشیسش', N'wwwsadasdsad', N'sadsadsadsdsa', 9, 2, 1, CAST(N'2023-07-18T17:14:27.7980864' AS DateTime2), 0)
INSERT [dbo].[Products] ([Id], [Name], [IsAvailable], [ProductCode], [Color], [Description], [Size], [Weight], [Features], [OtherDetails], [DigikalLink], [BasalamLink], [CategoryId], [MaterialId], [IsEnable], [CreatedDate], [IsTopSeller]) VALUES (24, N'آرین', 0, N'1123', NULL, N'این برس عالیع', 12, CAST(12.00 AS Decimal(18, 2)), N'23', N'بشسیسشیشسیسشیسش', N'wwwsadasdsad', N'sadsadsadsdsa', 11, 2, 1, CAST(N'2023-07-18T17:14:58.5540006' AS DateTime2), 1)
INSERT [dbo].[Products] ([Id], [Name], [IsAvailable], [ProductCode], [Color], [Description], [Size], [Weight], [Features], [OtherDetails], [DigikalLink], [BasalamLink], [CategoryId], [MaterialId], [IsEnable], [CreatedDate], [IsTopSeller]) VALUES (25, N'برس G121', 0, N'1123', NULL, N'این برس عالیع', 112, CAST(112.00 AS Decimal(18, 2)), N'چوبی', N'بشسیسشیشسیسشیسش', N'wwwsadasdsad', N'sadsadsadsdsa', 10, 2, 1, CAST(N'2023-07-18T17:15:45.3542687' AS DateTime2), 1)
INSERT [dbo].[Products] ([Id], [Name], [IsAvailable], [ProductCode], [Color], [Description], [Size], [Weight], [Features], [OtherDetails], [DigikalLink], [BasalamLink], [CategoryId], [MaterialId], [IsEnable], [CreatedDate], [IsTopSeller]) VALUES (26, N'برس G163', 0, N'1123', NULL, N'این برس عالیع', 21, CAST(112.00 AS Decimal(18, 2)), N'چوبی', N'بشسیسشیشسیسشیسش', N'wwwsadasdsad', N'sadsadsadsdsa', 9, 2, 1, CAST(N'2023-07-18T17:16:19.7742886' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Galleries] ON 

INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (37, N'112152176_20230718203607946.jpg', 18)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (38, N'112152183_20230718203608000.jpg', 18)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (39, N'112152188_20230718203608009.jpg', 18)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (40, N'1666412_20230718204145861.jpg', 19)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (41, N'1666461_20230718204145930.jpg', 19)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (42, N'1666493_20230718204145938.jpg', 19)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (43, N'578685_20230718204235953.jpg', 20)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (44, N'578747_20230718204235963.jpg', 20)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (45, N'578862_20230718204235980.jpg', 20)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (46, N'120458989_20230718204313181.jpg', 21)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (47, N'120458978_20230718204313197.jpg', 21)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (48, N'120458993_20230718204313212.jpg', 21)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (49, N'120458996_20230718204352626.jpg', 22)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (50, N'120484408_20230718204352642.jpg', 22)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (51, N'120458989_20230718204352657.jpg', 22)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (52, N'1287963_20230718204427833.jpg', 23)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (53, N'1288020_20230718204427901.jpg', 23)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (54, N'1288107_20230718204427966.jpg', 23)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (55, N'شسیس_20230718204458576.jpg', 24)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (56, N'120495738_20230718204458642.jpg', 24)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (57, N'1207س34995_20230718204458706.jpg', 24)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (58, N'120495738_20230718204545360.jpg', 25)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (59, N'شسیس_20230718204545389.jpg', 25)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (60, N'1207س34995_20230718204545405.jpg', 25)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (61, N'1207س34995_20230718204619782.jpg', 26)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (62, N'شسیس_20230718204619799.jpg', 26)
INSERT [dbo].[Galleries] ([Id], [ImageName], [ProductId]) VALUES (63, N'شسیس_20230718204619810.jpg', 26)
SET IDENTITY_INSERT [dbo].[Galleries] OFF
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230605161733_AddUserAndUserRoleModel', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230605161905_AddProductAndGalleryAndCategoryAndMaterial', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230615152650_ChangeColorToNullable', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230623163153_AddIsEnableToProduct', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230623163508_AddCreatedDateToProduct', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230711174800_addBlogs', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230713061818_addReadingTimeTOBlogTable', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230713071254_addCreateDateToBlogTable', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230717163018_AddTableLoyalty', N'6.0.0')
SET IDENTITY_INSERT [dbo].[LoyaltyClubUser] ON 

INSERT [dbo].[LoyaltyClubUser] ([Id], [MobileNumber], [Date]) VALUES (1, N'09032387023', CAST(N'2023-07-17T20:14:21.9295678' AS DateTime2))
INSERT [dbo].[LoyaltyClubUser] ([Id], [MobileNumber], [Date]) VALUES (2, N'09183571833', CAST(N'2023-07-17T20:15:38.3755603' AS DateTime2))
INSERT [dbo].[LoyaltyClubUser] ([Id], [MobileNumber], [Date]) VALUES (3, N'09183571833', CAST(N'2023-07-17T20:15:42.4928436' AS DateTime2))
INSERT [dbo].[LoyaltyClubUser] ([Id], [MobileNumber], [Date]) VALUES (4, N'09183571833', CAST(N'2023-07-17T20:15:48.7936247' AS DateTime2))
INSERT [dbo].[LoyaltyClubUser] ([Id], [MobileNumber], [Date]) VALUES (5, N'09032387022', CAST(N'2023-07-17T20:19:16.5128496' AS DateTime2))
INSERT [dbo].[LoyaltyClubUser] ([Id], [MobileNumber], [Date]) VALUES (6, N'09032387022', CAST(N'2023-07-17T20:19:45.8635612' AS DateTime2))
INSERT [dbo].[LoyaltyClubUser] ([Id], [MobileNumber], [Date]) VALUES (7, N'09052387023', CAST(N'2023-07-17T20:20:44.7685538' AS DateTime2))
INSERT [dbo].[LoyaltyClubUser] ([Id], [MobileNumber], [Date]) VALUES (8, N'09129527288', CAST(N'2023-07-17T20:21:32.8432932' AS DateTime2))
INSERT [dbo].[LoyaltyClubUser] ([Id], [MobileNumber], [Date]) VALUES (9, N'09183571822', CAST(N'2023-07-17T20:22:24.0470920' AS DateTime2))
INSERT [dbo].[LoyaltyClubUser] ([Id], [MobileNumber], [Date]) VALUES (10, N'09129527288', CAST(N'2023-07-18T20:46:45.1992926' AS DateTime2))
SET IDENTITY_INSERT [dbo].[LoyaltyClubUser] OFF
