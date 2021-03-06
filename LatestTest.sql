USE [master]
GO
/****** Object:  Database [SmartMeterData.sql]    Script Date: 8/8/2020 7:43:10 AM ******/
CREATE DATABASE [SmartMeterData.sql]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SmartMeterData.sql', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SmartMeterData.sql.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SmartMeterData.sql_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SmartMeterData.sql_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SmartMeterData.sql].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SmartMeterData.sql] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET ARITHABORT OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SmartMeterData.sql] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SmartMeterData.sql] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SmartMeterData.sql] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SmartMeterData.sql] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SmartMeterData.sql] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET RECOVERY FULL 
GO
ALTER DATABASE [SmartMeterData.sql] SET  MULTI_USER 
GO
ALTER DATABASE [SmartMeterData.sql] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SmartMeterData.sql] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SmartMeterData.sql] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SmartMeterData.sql] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SmartMeterData.sql', N'ON'
GO
USE [SmartMeterData.sql]
GO
/****** Object:  StoredProcedure [dbo].[Delete_Billings]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Delete_Billings]
							    @BillingsId int 
                               AS
	                            BEGIN
	                               set nocount on
	                               DELETE FROM dbo.billingsInfos 
	                               WHERE Billings_Id = @BillingsId
	                            END

GO
/****** Object:  StoredProcedure [dbo].[Delete_Devices]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Delete_Devices]
							@device_Id int 

AS
	BEGIN
	   set nocount on
	   DELETE FROM dbo.deviceInfos 
	   WHERE Device_ID = @device_Id
	END

GO
/****** Object:  StoredProcedure [dbo].[GetAllBillings_BySubscriberId]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllBillings_BySubscriberId]
                                    @Subscriber_ID  INT
									
					AS
					Begin                                

							Select bi.Billings_Id,bi.DEviceId,bi.TIME_OF_TRANSACTION,bi.STAKEHOLDER_ID,bi.SUBSCRIBER_ID,bi.USAGE_DURATION,bi.AMOUNT_CONSUMPTION_PER_HR as EnergyConsumedFor30Days,di.TypeOfHouseStatus ,(bi.AMOUNT_CONSUMPTION_PER_HR * ti.[TarrifAmount] ) as AmountToPay ,di.Device_Name,di.Lga,di.Bus_Stop From dbo.billingsInfos bi inner join dbo.deviceInfos di 
							On di.Device_ID = bi.DEviceId inner join dbo.tarrifInfos ti On (ti.Tarrif_Id = bi.Tarrif_Id And ti.STAKEHOLDER_ID = di.STAKEHOLDER_ID) 
										
							WHERE bi.SUBSCRIBER_ID = @Subscriber_ID
					End
GO
/****** Object:  StoredProcedure [dbo].[GetAllBillings_BySubscriberId_CurrentMonth]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllBillings_BySubscriberId_CurrentMonth]
                                    @Subscriber_ID  INT
									
					AS
					Begin                                

							Select bi.Billings_Id,bi.DEviceId,bi.BillingMonth,bi.TIME_OF_TRANSACTION,bi.STAKEHOLDER_ID,bi.SUBSCRIBER_ID,bi.USAGE_DURATION,bi.AMOUNT_CONSUMPTION_PER_HR as EnergyConsumedFor30Days,di.TypeOfHouseStatus ,(bi.AMOUNT_CONSUMPTION_PER_HR * ti.[TarrifAmount] ) as AmountToPay ,di.Device_Name,di.Lga,di.Bus_Stop From dbo.billingsInfos bi inner join dbo.deviceInfos di 
							On di.Device_ID = bi.DEviceId inner join dbo.tarrifInfos ti On (ti.Tarrif_Id = bi.Tarrif_Id And ti.STAKEHOLDER_ID = di.STAKEHOLDER_ID) 
										
							WHERE bi.SUBSCRIBER_ID = @Subscriber_ID AND bi.BillingMonth = (Select(DateName(MONTH,GETDATE())) as BillingMonth)
					End
GO
/****** Object:  StoredProcedure [dbo].[GetAllBillings_BySubscriberIdMonth]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllBillings_BySubscriberIdMonth]
                                    @Subscriber_ID  INT
									,@billingMonth  VARCHAR(50)
									
	AS
	Begin   
	


	--SET @billingMonth = (Select(DateName(MONTH,GETDATE())))

							Select Top(3) bi.Billings_Id,bi.DEviceId,bi.BillingMonth,bi.TIME_OF_TRANSACTION,bi.STAKEHOLDER_ID,bi.SUBSCRIBER_ID,bi.USAGE_DURATION,bi.AMOUNT_CONSUMPTION_PER_HR as EnergyConsumedFor30Days,di.TypeOfHouseStatus ,(bi.AMOUNT_CONSUMPTION_PER_HR * ti.[TarrifAmount] ) as AmountToPay ,di.Device_Name,di.Lga,di.Bus_Stop From dbo.billingsInfos bi inner join dbo.deviceInfos di 
							On di.Device_ID = bi.DEviceId inner join dbo.tarrifInfos ti On (ti.Tarrif_Id = bi.Tarrif_Id And ti.STAKEHOLDER_ID = di.STAKEHOLDER_ID) 
										
							WHERE (bi.SUBSCRIBER_ID = @Subscriber_ID AND bi.BillingMonth = @billingMonth )
							ORDER BY bi.Billings_Id desc
					End
GO
/****** Object:  StoredProcedure [dbo].[GetAllBillings_FirstThreeMonth]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllBillings_FirstThreeMonth]
									@Subscriber_ID  INT
	AS
	Begin   
	


	--SET @billingMonth = (Select(DateName(MONTH,GETDATE())))

							Select Top(3) bi.Billings_Id,bi.DEviceId,bi.BillingMonth,bi.TIME_OF_TRANSACTION,bi.STAKEHOLDER_ID,bi.SUBSCRIBER_ID,bi.USAGE_DURATION,bi.AMOUNT_CONSUMPTION_PER_HR as EnergyConsumedFor30Days,di.TypeOfHouseStatus ,(bi.AMOUNT_CONSUMPTION_PER_HR * ti.[TarrifAmount] ) as AmountToPay ,di.Device_Name,di.Lga,di.Bus_Stop From dbo.billingsInfos bi inner join dbo.deviceInfos di 
							On di.Device_ID = bi.DEviceId inner join dbo.tarrifInfos ti On (ti.Tarrif_Id = bi.Tarrif_Id And ti.STAKEHOLDER_ID = di.STAKEHOLDER_ID) 
										
							WHERE (bi.SUBSCRIBER_ID = @Subscriber_ID  )
							ORDER BY bi.Billings_Id desc
					End
GO
/****** Object:  StoredProcedure [dbo].[GetBillings_ById]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBillings_ById]
							    @BillingsId int 
                                AS
                                BEGIN
                                    set nocount on
                                        Select DEviceId,SUBSCRIBER_ID,STAKEHOLDER_ID,USAGE_DURATION,MONTHLY_DURATION_PER_HR,AMOUNT_CONSUMPTION_PER_HR,TIME_OF_TRANSACTION
	                                    From dbo.billingsInfos
                                    WHERE Billings_Id = @BillingsId
                                END

GO
/****** Object:  StoredProcedure [dbo].[GetBillingswithId]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBillingswithId]
	                        @Billing_Id  INT
									
					AS
					Begin                                

							Select bi.Billings_Id,bi.DEviceId,bi.TIME_OF_TRANSACTION,bi.STAKEHOLDER_ID,bi.SUBSCRIBER_ID,bi.USAGE_DURATION,bi.AMOUNT_CONSUMPTION_PER_HR as EnergyConsumedFor30Days,di.TypeOfHouseStatus ,(bi.AMOUNT_CONSUMPTION_PER_HR * ti.[TarrifAmount] ) as AmountToPay ,di.Device_Name,di.Lga,di.Bus_Stop From dbo.billingsInfos bi inner join dbo.deviceInfos di 
							On di.Device_ID = bi.DEviceId inner join dbo.tarrifInfos ti On (ti.Tarrif_Id = bi.Tarrif_Id And ti.STAKEHOLDER_ID = di.STAKEHOLDER_ID) 
										
							WHERE bi.Billings_Id = @Billing_Id
					End
GO
/****** Object:  StoredProcedure [dbo].[GetDevices_ById]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDevices_ById]
							@Device_Id int 
AS
BEGIN
   set nocount on
   SELECT Device_ID,Subscriber_ID,STAKEHOLDER_ID,Imei_Number,Device_Name,[Address],Bus_Stop,[State],Country,Lga,Verify_Address,Delivery_Flag,Flag_Operation,Transaction_Date 
   FROM dbo.deviceInfos 
   WHERE Device_ID = @Device_Id
END

GO
/****** Object:  StoredProcedure [dbo].[GetPayments_BySubscriberId]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPayments_BySubscriberId]
                               @Subscriber_ID  INT
									
AS
	BEGIN      
		SELECT bi.Billings_Id,bi.STAKEHOLDER_ID,bi.SUBSCRIBER_ID,bi.USAGE_DURATION,(bi.AMOUNT_CONSUMPTION_PER_HR * ti.[TarrifAmount] ) AS AmountToPay ,si.First_Name,si.Last_Name,si.Phone 
				
		FROM dbo.billingsInfos bi INNER JOIN dbo.subscriberInfos si 

		ON si.Subscriber_ID = bi.SUBSCRIBER_ID INNER JOIN dbo.tarrifInfos ti 

		ON (ti.Tarrif_Id = bi.Tarrif_Id AND ti.STAKEHOLDER_ID = si.STAKEHOLDER_ID) 
										
		WHERE bi.SUBSCRIBER_ID = @Subscriber_ID
    END
GO
/****** Object:  StoredProcedure [dbo].[GetSubscriber_ById]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubscriber_ById]
							        @Subscriber_Id int 
                                    AS
                                    BEGIN
                                       set nocount on
                                       SELECT  SUBSCRIBER_ID,STAKEHOLDER_ID,FIRST_NAME,LAST_NAME,PHONE,GENDER,DOFB,EMAIL,ADDRESS,STATE,COUNTRY
                                       FROM dbo.SUBSCRIBER 
                                       WHERE SUBSCRIBER_ID = @Subscriber_Id
                                    END

GO
/****** Object:  StoredProcedure [dbo].[GetSubscriber_ByloginId]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubscriber_ByloginId]
							@Email varchar(200) 
AS
BEGIN
   set nocount on
  SELECT si.Subscriber_ID FROM dbo.subscriberInfos si inner join AspNetUsers an ON si.UserId = an.Id
  WHERE an.Email =  @Email
END

GO
/****** Object:  StoredProcedure [dbo].[GetTarrifs_ById]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTarrifs_ById]
							@Tarrif_Id INT 
AS
BEGIN
   set nocount on
   SELECT Tarrif_Id,STAKEHOLDER_ID,TarrifAmount,TypeOfHouse 
   FROM dbo.tarrifInfos 
   WHERE Tarrif_Id = @Tarrif_Id
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Select_AllBillings]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Select_AllBillings]	
	                            @Subscriber_ID             INT,
	                            @STAKEHOLDER_ID            VARCHAR(50),
	                            @USAGE_DURATION            INT,
	                            @MONTHLY_DURATION_PER_HR   DECIMAL(17,15),
	                            @AMOUNT_CONSUMPTION_PER_HR DECIMAL(17,15)	
                                AS
                                Begin
	                               
	                                Insert into dbo.billingsInfos(SUBSCRIBER_ID,STAKEHOLDER_ID,USAGE_DURATION,MONTHLY_DURATION_PER_HR,AMOUNT_CONSUMPTION_PER_HR)
	                                Values(@Subscriber_ID,@STAKEHOLDER_ID, @USAGE_DURATION,@MONTHLY_DURATION_PER_HR,@AMOUNT_CONSUMPTION_PER_HR )	            
                                End

GO
/****** Object:  StoredProcedure [dbo].[Insert_Select_AllDevices]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Select_AllDevices]
	--@Device_ID INT,
	@Subscriber_ID INT,
	@STAKEHOLDER_ID VARCHAR(50),
	@Imei_Number VARCHAR(50),
	@Device_Name VARCHAR(50),
	@Address Varchar(50),
	@Bus_Stop Varchar(50),
	@Verify_Address Varchar(2),
	@Delivery_Flag Varchar(3),
	@Flag_Operation Varchar(2),
	@State VARCHAR(30),
	@Lga Varchar(70),
	@Country  Varchar(50)
AS
Begin
 --if not exists(select 1 from dbo.deviceInfos where Subscriber_ID = @Subscriber_ID)
	Begin
	Insert into dbo.deviceInfos(Subscriber_ID,STAKEHOLDER_ID,Imei_Number,Device_Name,[Address],Bus_Stop,[State],Country,Lga,Verify_Address,Delivery_Flag,Flag_Operation,Transaction_Date )
	Values(@Subscriber_ID,@STAKEHOLDER_ID, @Imei_Number,@Device_Name,@Address,@Bus_Stop,@State,@Lga,@Verify_Address,@Delivery_Flag,@Flag_Operation,@Country,GetDate() )
	End
End

GO
/****** Object:  StoredProcedure [dbo].[Insert_Select_AllSubscribers]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Select_AllSubscribers]
	@STAKEHOLDER_ID VARCHAR(150),
	@First_Name VARCHAR(50),
	@Last_Name VARCHAR(50),
	@Phone Varchar(30),
	@Gender Varchar(30),
	@Dofb VARCHAR(30),
	@Email VARCHAR(50),
	@Address Varchar(50),
	@State VARCHAR(30),
	@Country Varchar(70)
AS
Begin
 if not exists(select 1 from dbo.SUBSCRIBER where EMAIL = @Email)
	Begin
	Insert into dbo.subscriberInfos(STAKEHOLDER_ID,First_Name,Last_Name,Phone,Gender,Dofb,Email,[Address],[State],Country,Date_Of_Registration)
	Values(@STAKEHOLDER_ID, @First_Name,@Last_Name,@Phone,@Gender,@Dofb,@Email,@Address,@State,@Country,GETDATE() )
	End
End

GO
/****** Object:  StoredProcedure [dbo].[Insert_Select_Tarrifs]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Select_Tarrifs]
	--@Device_ID INT,
	--@Tarrif_Id INT,
	@STAKEHOLDER_ID VARCHAR(50),
	@TarrifAmount DECIMAL(18,2),
	@TypeOfHouse VARCHAR(50)
AS
Begin
 --if not exists(select 1 from dbo.deviceInfos where Subscriber_ID = @Subscriber_ID)
	Begin
	Insert into dbo.tarrifInfos(STAKEHOLDER_ID,TarrifAmount,TypeOfHouse,DateOfTarrifTransaction)
	Values(@STAKEHOLDER_ID, @TarrifAmount,@TypeOfHouse,GetDate() )
	End
End
GO
/****** Object:  StoredProcedure [dbo].[Insert_SelectEnergyConsumed]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[Insert_SelectEnergyConsumed]
											( @DeviceId				  INT
											,@StartDate           DATETIME
											,@EndDate             DATETIME
											,@Billings_Id		  INT OUT) AS

BEGIN

  --BEGIN TRY

  DECLARE @TotalTimeOfUsageInHrs	    INT;
  DECLARE @ConnectedTimeOfUsageInHrs    INT;
  DECLARE @DisconnectedTimeOfUsageInHrs INT;
  DECLARE @Tarrif_id                    INT;
  DECLARE @TotalEnergyConsumePerHr		DECIMAL(19,15);
  DECLARE @EnergyAmountConsumedIn30days DECIMAL(19,15);
  DECLARE @Subscriber_ID                INT;
  DECLARE @STAKEHOLDER_ID               VARCHAR(50);
  DECLARE @TypeOfHousestatus            VARCHAR(30);
  DECLARE @Base_Location                VARCHAR(MAX);
  DECLARE @billingMonth                 VARCHAR(MAX);



	

	SELECT @Subscriber_ID = Subscriber_ID
	      ,@Base_Location = State
		  ,@TypeOfHousestatus = TypeOfHouseStatus
	      ,@STAKEHOLDER_ID = STAKEHOLDER_ID
 	  FROM dbo.deviceInfos WITH (NOLOCK)
	 WHERE Device_ID = @DeviceId;



	 SELECT @Tarrif_id = Tarrif_Id 
	 FROM dbo.tarrifInfos WITH (NOLOCK)
	 WHERE STAKEHOLDER_ID = @STAKEHOLDER_ID AND TypeOfHouseStatus = @TypeOfHousestatus;
	 

								--SELECT @Subscriber_ID = SUBSCRIBER_ID
								--      ,@STAKEHOLDER_ID = STAKEHOLDER_ID
							 --	  FROM dbo.DEVICEINFO WITH (NOLOCK)
								-- WHERE DEVICE_ID = @DEviceId;

	SELECT  @ConnectedTimeOfUsageInHrs = (SUM(datepart(HH,EventProcessedUtcTime )) )   from dbo.IOTDeviceSmartMeter 
    WHERE (DEviceId = @DeviceId AND ConnectionStatus = '2' AND (EventProcessedUtcTime >= @StartDate AND EventProcessedUtcTime <= @EndDate ))
    GROUP BY DEviceId

	SELECT  @DisconnectedTimeOfUsageInHrs = (SUM(datepart(HH,EventProcessedUtcTime )) )   from dbo.IOTDeviceSmartMeter 
    WHERE (DEviceId = @DeviceId AND ConnectionStatus = '1' AND (EventProcessedUtcTime >= @StartDate AND EventProcessedUtcTime <= @EndDate ))
    GROUP BY DEviceId

	 SET @TotalTimeOfUsageInHrs = (@ConnectedTimeOfUsageInHrs - @DisconnectedTimeOfUsageInHrs)

	--IF (@ConnectedTimeOfUsageInHrs > @DisconnectedTimeOfUsageInHrs) 	  	
 --   BEGIN 

	--    SET @TotalTimeOfUsageInHrs = @ConnectedTimeOfUsageInHrs - @DisconnectedTimeOfUsageInHrs
 --   END

	--IF (@DisconnectedTimeOfUsageInHrs > @ConnectedTimeOfUsageInHrs) 	  	
 --   BEGIN 

	--    SET @TotalTimeOfUsageInHrs = 0
 --   END

      SET @billingMonth = (Select(DateName(MONTH,GETDATE())))
	 
	SELECT  PartitionId, SUM(EnergyAmountkWh) AS TotalEnergyConsumePerHr,DEviceId,
	((SUM(EnergyAmountkWh))/(NULLIF(@TotalTimeOfUsageInHrs,0))) As EnergyAmountConsumedIn30days  FROM dbo.IOTDeviceSmartMeter
	WHERE (DEviceId = @DeviceId AND ConnectionStatus = '2' AND (EventProcessedUtcTime >= @StartDate AND EventProcessedUtcTime <= @EndDate ))
	GROUP BY DEviceId, PartitionId

	SELECT   @TotalEnergyConsumePerHr = SUM(EnergyAmountkWh) ,@DeviceId = DEviceId ,
	@EnergyAmountConsumedIn30days = ((SUM(EnergyAmountkWh))/(NULLIF(@TotalTimeOfUsageInHrs,0))) FROM dbo.IOTDeviceSmartMeter
	WHERE (DEviceId = @DeviceId AND ConnectionStatus = '2' AND (EventProcessedUtcTime >= @StartDate AND EventProcessedUtcTime <= @EndDate ))
	GROUP BY DEviceId, PartitionId	

	IF (@TotalTimeOfUsageInHrs < 0)	
    BEGIN 
	SET @TotalTimeOfUsageInHrs = 0

	SELECT  PartitionId, SUM(EnergyAmountkWh) AS TotalEnergyConsumePerHr,DEviceId,
	((SUM(EnergyAmountkWh))/(NULLIF(@TotalTimeOfUsageInHrs,0))) As EnergyAmountConsumedIn30days FROM dbo.IOTDeviceSmartMeter
    WHERE (DEviceId = @DeviceId AND ConnectionStatus = '2' AND (EventProcessedUtcTime >= @StartDate AND EventProcessedUtcTime <= @EndDate ))
	GROUP BY DEviceId, PartitionId

	SELECT   @TotalEnergyConsumePerHr = SUM(EnergyAmountkWh) ,@DeviceId = DEviceId ,
	@EnergyAmountConsumedIn30days = ((SUM(EnergyAmountkWh))/(NULLIF(@TotalTimeOfUsageInHrs,0))) FROM dbo.IOTDeviceSmartMeter
	WHERE (DEviceId = @DeviceId AND ConnectionStatus = '2' AND (EventProcessedUtcTime >= @StartDate AND EventProcessedUtcTime <= @EndDate ))
	GROUP BY DEviceId, PartitionId	
	END

	IF (@TotalEnergyConsumePerHr > 0) 	  	
    BEGIN 
		INSERT INTO dbo.billingsInfos(DEviceId,Tarrif_Id,STAKEHOLDER_ID,SUBSCRIBER_ID,USAGE_DURATION,AMOUNT_CONSUMPTION_PER_HR,MONTHLY_DURATION_PER_HR,Base_Location,BillingMonth)
		VALUES (@DeviceId,@Tarrif_id,@STAKEHOLDER_ID,@Subscriber_ID,@TotalTimeOfUsageInHrs,@TotalEnergyConsumePerHr,@EnergyAmountConsumedIn30days,@Base_Location,@billingMonth)
	 SET @Billings_Id = @@IDENTITY;
	END
END

GO
/****** Object:  StoredProcedure [dbo].[Select_AllBillings]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Select_AllBillings]
							@startDate date,
							@endDate date						
                           AS
                        Begin
                                  
								Select bi.Billings_Id,bi.DEviceId,bi.TIME_OF_TRANSACTION,bi.STAKEHOLDER_ID,bi.SUBSCRIBER_ID,bi.USAGE_DURATION,bi.AMOUNT_CONSUMPTION_PER_HR as EnergyConsumedFor30Days,di.TypeOfHouseStatus ,(bi.AMOUNT_CONSUMPTION_PER_HR * ti.[TarrifAmount] ) as AmountToPay ,di.Device_Name,di.Lga,di.Bus_Stop From dbo.billingsInfos bi inner join dbo.deviceInfos di 
								On di.Device_ID = bi.DEviceId inner join dbo.tarrifInfos ti On (ti.Tarrif_Id = bi.Tarrif_Id And ti.STAKEHOLDER_ID = di.STAKEHOLDER_ID) 
								Where 1 = 1
								Or(TIME_OF_TRANSACTION <= @endDate And @endDate < TIME_OF_TRANSACTION)
	                            Or(TIME_OF_TRANSACTION <= @startDate And @startDate < TIME_OF_TRANSACTION)
	                            Order by bi.TIME_OF_TRANSACTION Desc
                        End
GO
/****** Object:  StoredProcedure [dbo].[Select_AllDevices]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Select_AllDevices]
                        @startDate date,
						@endDate date
						
AS

Begin	


     Select Device_ID,Subscriber_ID,STAKEHOLDER_ID,Imei_Number,Device_Name,[Address],Bus_Stop,[State],Country,Lga,Verify_Address,Delivery_Flag,Flag_Operation,Transaction_Date 
	 From dbo.deviceInfos Where 1 = 1
	 Or(Transaction_Date <= @endDate And @endDate < Transaction_Date)
	 Or(Transaction_Date <= @startDate And @startDate < Transaction_Date)
	 Order By Device_ID  Desc
	

End

GO
/****** Object:  StoredProcedure [dbo].[Select_AllIOTDeviceSmartMeter]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Select_AllIOTDeviceSmartMeter]
                        @startDate date,
						@endDate date
						
AS

Begin	


     Select Id,DEviceId,EnergyAmountkWh,VoltageReading,PowerReading,ConnectionStatus,StartDatetime,EndDatetime 
	 From dbo.IOTDeviceSmartMeter Where 1 = 1
	 Or(EndDatetime <= @endDate And @endDate < EndDatetime)
	 Or(StartDatetime <= @startDate And @startDate < StartDatetime)
	 Order By DEviceId  Desc
	

End

GO
/****** Object:  StoredProcedure [dbo].[Select_AllSubscriber]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Select_AllSubscriber]
                        @startDate date,
						@endDate date
						
AS

	DECLARE @email varchar(50);
Begin	


     Select Subscriber_ID, STAKEHOLDER_ID,First_Name,Last_Name,Phone,Gender,Dofb,Email,[Address],[State],Country,Date_Of_Registration 
	 From dbo.subscriberInfos Where 1 = 1
	 Or(Date_Of_Registration <= @endDate And @endDate < Date_Of_Registration)
	 Or(Date_Of_Registration <= @startDate And @startDate < Date_Of_Registration)
	 Or(EMAIL = @email )
	 Order By Subscriber_ID  Desc
	

End

GO
/****** Object:  StoredProcedure [dbo].[Select_IOTMonthlySmartMeter]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Select_IOTMonthlySmartMeter]
                      @ConnectionStatus varchar(5)
						
AS

Begin	
			 SELECT iod.Id,iod.DEviceId,iod.ConnectionStatus,bi.USAGE_DURATION,iod.EventEnqueuedUtcTime,iod.EventProcessedUtcTime 
			 FROM dbo.IOTDeviceSmartMeter iod inner join dbo.billingsInfos bi ON iod.DEviceId = bi.DEviceId 
			 WHERE ConnectionStatus = @ConnectionStatus
			 ORDER BY iod.DEviceId  DESC
	

End

GO
/****** Object:  StoredProcedure [dbo].[Select_IOTUsageSmartMeter]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Select_IOTUsageSmartMeter]
                        @ConnectionStatus varchar(5)
						
AS

Begin	


     Select Id,DEviceId,EnergyAmountkWh,ConnectionStatus,EventEnqueuedUtcTime,EventProcessedUtcTime 
	 From dbo.IOTDeviceSmartMeter Where ConnectionStatus = @ConnectionStatus
	 Order By DEviceId  Desc
	

End

GO
/****** Object:  StoredProcedure [dbo].[Update_Billings]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Billings]
                                @BillingsID				   INT,
								@Device_ID				   INT,
								@Subscriber_ID			   INT,
								@STAKEHOLDER_ID			   VARCHAR(50),
								@USAGE_DURATION			   INT,
								@MONTHLY_DURATION_PERHR    DECIMAL(17,15),
								@AMOUNT_CONSUMPTION_PER_HR DECIMAL(17,15)
                            AS
	                            BEGIN
		                              set nocount on;
		                              UPDATE dbo.billingsInfos
		                              SET   SUBSCRIBER_ID					=  @Subscriber_ID
			                               ,STAKEHOLDER_ID					= @STAKEHOLDER_ID
			                               ,DEviceId						= @Device_ID
			                               ,USAGE_DURATION                  = @USAGE_DURATION
			                               ,MONTHLY_DURATION_PER_HR	        = @MONTHLY_DURATION_PERHR
			                               ,AMOUNT_CONSUMPTION_PER_HR       = @AMOUNT_CONSUMPTION_PER_HR
			                               WHERE Billings_Id   = @BillingsID
	                            END

GO
/****** Object:  StoredProcedure [dbo].[Update_DevicesDetails]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_DevicesDetails]
								@Device_ID INT,
								@Subscriber_ID INT,
								@STAKEHOLDER_ID VARCHAR(50),
								@Imei_Number VARCHAR(50),
								@Device_Name VARCHAR(50),
								@Address Varchar(50),
								@Bus_Stop Varchar(50),
								@State VARCHAR(30),
								@Lga Varchar(70),
								@Verify_Address Varchar(2),
								@Delivery_Flag Varchar(3),
								@Flag_Operation Varchar(2),								
								@Country  Varchar(50)
AS
	BEGIN
		  set nocount on;
		  UPDATE dbo.deviceInfos
		  SET   Subscriber_ID =  @Subscriber_ID
			   ,STAKEHOLDER_ID = @STAKEHOLDER_ID
			   ,Imei_Number    = @Imei_Number
			   ,Device_Name    = @Device_Name
			   ,[Address]	   = @Address
			   ,Bus_Stop       = @Bus_Stop
			   ,[State]        = @State
			   ,Lga            = @Lga
			   ,Verify_Address = @Verify_Address
			   ,Delivery_Flag  = @Delivery_Flag
			   ,Flag_Operation = @Flag_Operation			  
			   ,Country        = @Country
		  WHERE Device_ID = @Device_ID;

	END

GO
/****** Object:  StoredProcedure [dbo].[Update_TarrifDetails]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TarrifDetails]
								@Tarrif_Id INT,
								@STAKEHOLDER_ID VARCHAR(50),
								@TarrifAmount DECIMAL(18,2),
								@TypeOfHouse VARCHAR(50)
AS
	BEGIN
		  set nocount on;
		  UPDATE dbo.tarrifInfos
		  SET   STAKEHOLDER_ID			= @STAKEHOLDER_ID
			   ,TarrifAmount			= @TarrifAmount
			   ,TypeOfHouse				= @TypeOfHouse
			   ,DateOfTarrifTransaction = GetDate()
		  WHERE Tarrif_Id = @Tarrif_Id;

	END
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BILLINGS]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BILLINGS](
	[BILLINGS_ID] [int] IDENTITY(1,1) NOT NULL,
	[DEviceId] [int] NOT NULL,
	[SUBSCRIBER_ID] [int] NOT NULL,
	[STAKEHOLDER_ID] [varchar](50) NOT NULL,
	[USAGE_DURATION] [datetime] NOT NULL,
	[AMOUNT_CONSUMPTION_PER_HR] [decimal](26, 25) NULL,
	[MONTHLY_DURATION_PER_HR] [decimal](26, 25) NULL,
PRIMARY KEY CLUSTERED 
(
	[BILLINGS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[billingsInfos]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[billingsInfos](
	[Billings_Id] [int] IDENTITY(1,1) NOT NULL,
	[DEviceId] [int] NOT NULL,
	[Tarrif_Id] [int] NULL,
	[STAKEHOLDER_ID] [nvarchar](max) NULL,
	[SUBSCRIBER_ID] [int] NOT NULL,
	[USAGE_DURATION] [int] NOT NULL,
	[AMOUNT_CONSUMPTION_PER_HR] [decimal](19, 15) NOT NULL,
	[MONTHLY_DURATION_PER_HR] [decimal](19, 15) NOT NULL,
	[TIME_OF_TRANSACTION] [datetime] NOT NULL,
	[Base_Location] [varchar](max) NULL,
	[BillingMonth] [varchar](30) NULL,
 CONSTRAINT [PK_billingsInfos] PRIMARY KEY CLUSTERED 
(
	[Billings_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category5]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category5](
	[CatId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](50) NULL,
	[isselected] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[CatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DEVICEINFO]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DEVICEINFO](
	[DEVICE_ID] [int] IDENTITY(1,1) NOT NULL,
	[SUBSCRIBER_ID] [int] NULL,
	[STAKEHOLDER_ID] [nvarchar](150) NULL,
	[DEVICE_NAME] [nvarchar](50) NULL,
	[IMEI_NUMBER] [nvarchar](50) NOT NULL,
	[ADDRESS] [varchar](100) NOT NULL,
	[BUS_STOP] [varchar](50) NOT NULL,
	[LGA] [varchar](70) NOT NULL,
	[STATE] [varchar](30) NOT NULL,
	[COUNTRY] [varchar](50) NULL,
	[VERIFY_ADDRESS] [varchar](2) NOT NULL,
	[DELIVERY_FLAG] [varchar](3) NOT NULL,
	[FLAG_OPERATION] [varchar](2) NOT NULL,
	[VERIFY_ADDRESS_DATE] [datetime] NOT NULL,
	[DELIVERY_FLAG_DATE] [datetime] NOT NULL,
	[TRANSACTION_DATE] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DEVICE_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[deviceInfos]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[deviceInfos](
	[Device_ID] [int] IDENTITY(1,1) NOT NULL,
	[Subscriber_ID] [int] NOT NULL,
	[STAKEHOLDER_ID] [nvarchar](max) NULL,
	[Imei_Number] [nvarchar](max) NULL,
	[Device_Name] [nvarchar](max) NULL,
	[TypeOfHouseStatus] [varchar](30) NULL,
	[Address] [nvarchar](max) NULL,
	[Bus_Stop] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[Lga] [nvarchar](max) NULL,
	[Verify_Address] [nvarchar](5) NULL,
	[Delivery_Flag] [nvarchar](5) NULL,
	[Flag_Operation] [nvarchar](5) NULL,
	[Device_Status] [nvarchar](5) NULL,
	[Country] [nvarchar](max) NULL,
	[Transaction_Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_deviceInfos] PRIMARY KEY CLUSTERED 
(
	[Device_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IOTDeviceSmartMeter]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IOTDeviceSmartMeter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DEviceId] [int] NOT NULL,
	[messageId] [varchar](3) NULL,
	[EnergyAmountkWh] [decimal](17, 15) NULL,
	[VoltageReading] [decimal](17, 15) NULL,
	[PowerReading] [decimal](17, 15) NULL,
	[ConnectionStatus] [nvarchar](200) NULL,
	[PartitionId] [varchar](3) NULL,
	[EventProcessedUtcTime] [datetime] NULL,
	[EventEnqueuedUtcTime] [datetime] NULL,
	[StartDatetime] [datetime] NULL,
	[EndDatetime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stackholderInfoModels]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stackholderInfoModels](
	[Stackholder_ID] [int] IDENTITY(1,1) NOT NULL,
	[Sackholder_Name] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NULL,
	[Date_Of_Registration] [datetime2](7) NOT NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_stackholderInfoModels] PRIMARY KEY CLUSTERED 
(
	[Stackholder_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SUBSCRIBER]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SUBSCRIBER](
	[SUBSCRIBER_ID] [int] IDENTITY(1,1) NOT NULL,
	[FIRST_NAME] [varchar](50) NOT NULL,
	[LAST_NAME] [varchar](50) NOT NULL,
	[PHONE] [varchar](30) NOT NULL,
	[GENDER] [varchar](2) NULL,
	[DOFB] [varchar](30) NULL,
	[EMAIL] [varchar](50) NOT NULL,
	[ADDRESS] [varchar](50) NULL,
	[STATE] [varchar](30) NULL,
	[COUNTRY] [varchar](70) NULL,
	[STAKEHOLDER_ID] [varchar](150) NULL,
	[DATE_REGISTERED] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SUBSCRIBER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[subscriberInfos]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subscriberInfos](
	[Subscriber_ID] [int] IDENTITY(1,1) NOT NULL,
	[STAKEHOLDER_ID] [nvarchar](max) NULL,
	[First_Name] [nvarchar](max) NULL,
	[Last_Name] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NULL,
	[Dofb] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NULL,
	[Date_Of_Registration] [datetime2](7) NOT NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_subscriberInfos] PRIMARY KEY CLUSTERED 
(
	[Subscriber_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tarrifInfos]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tarrifInfos](
	[Tarrif_Id] [int] IDENTITY(1,1) NOT NULL,
	[STAKEHOLDER_ID] [nvarchar](max) NULL,
	[TypeOfHouse] [varchar](max) NULL,
	[TypeOfHouseStatus] [varchar](30) NULL,
	[TarrifAmount] [decimal](18, 2) NOT NULL,
	[DateOfTarrifTransaction] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_tarrifInfos] PRIMARY KEY CLUSTERED 
(
	[Tarrif_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[userInfos]    Script Date: 8/8/2020 7:43:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userInfos](
	[Id] [nvarchar](450) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[EmailAddress] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_userInfos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200602002332_identityusers', N'3.1.4')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1d288bf4-b7f1-43cf-adbe-e80b2b25f469', N'Subcriber', N'SUBCRIBER', N'ffcd8776-14f6-4f09-ba6e-eca59467d986')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'3e2c5b8f-f034-4e90-9568-b6514b36b680', N'Admin', N'ADMIN', N'd502337b-1040-4014-8e4e-2b31e22ee298')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'697cc347-7695-476c-8fe2-ebf368915a20', N'StackHolder', N'STACKHOLDER', N'accd3040-ce69-49ad-a466-a978245277a6')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fec1ceb1-0803-4df3-821d-2a07afae0f0a', N'1d288bf4-b7f1-43cf-adbe-e80b2b25f469')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b0b927e5-c505-426a-931a-57eb4e18c02f', N'3e2c5b8f-f034-4e90-9568-b6514b36b680')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1b64d55e-4d21-4f06-be16-b1af5ae791a5', N'697cc347-7695-476c-8fe2-ebf368915a20')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1b64d55e-4d21-4f06-be16-b1af5ae791a5', N'stacholder@smartmeter.com', N'STACHOLDER@SMARTMETER.COM', N'stacholder@smartmeter.com', N'STACHOLDER@SMARTMETER.COM', 1, N'AQAAAAEAACcQAAAAEAyidU7tR9y30JoeVOLNxAE6/QB0+DLroQbrTzqv3IbnGT1s6VZ4U/90/ri3+LHG0w==', N'ZLFPYNXTP65IAAJ7V7B5JYJZ44LNONFS', N'c1d64ebd-01a7-4e64-8df0-10e6770a3ff6', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b0b927e5-c505-426a-931a-57eb4e18c02f', N'admin@smartmeter.com', N'ADMIN@SMARTMETER.COM', N'admin@smartmeter.com', N'ADMIN@SMARTMETER.COM', 1, N'AQAAAAEAACcQAAAAEEpKmHFDUy1nZw24ggq4/WkOxrWzmpEpfQDU71xjAQp9ezujEEMZFIgeq271ZWBlqA==', N'R6VM2TKYY2ENCJQ3KLK4QKAT6FRUIPN5', N'b3831991-51cd-4d84-9454-c8a2e5f00680', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fec1ceb1-0803-4df3-821d-2a07afae0f0a', N'subscriber@smartmeter.com', N'SUBSCRIBER@SMARTMETER.COM', N'subscriber@smartmeter.com', N'SUBSCRIBER@SMARTMETER.COM', 1, N'AQAAAAEAACcQAAAAEPhTcCTRzcvLyb79+3QZ3a0vw/jf7nekw+JJqR4MTl8Ekn3NncDorUF2+BQ1IZNS6Q==', N'AKTPAY3SJBCLHGABO6L4F5AKC3ANNQNC', N'3625e950-8aa2-44d5-8ce7-41edefd7fb76', NULL, 0, 0, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[billingsInfos] ON 

INSERT [dbo].[billingsInfos] ([Billings_Id], [DEviceId], [Tarrif_Id], [STAKEHOLDER_ID], [SUBSCRIBER_ID], [USAGE_DURATION], [AMOUNT_CONSUMPTION_PER_HR], [MONTHLY_DURATION_PER_HR], [TIME_OF_TRANSACTION], [Base_Location], [BillingMonth]) VALUES (1024, 5, 4, N'ade.tunde@appleelectric.com', 6, 744, CAST(97.554529875600000 AS Decimal(19, 15)), CAST(0.131121679940322 AS Decimal(19, 15)), CAST(0x0000ABCE003FCAF3 AS DateTime), N'Lagos', N'May')
INSERT [dbo].[billingsInfos] ([Billings_Id], [DEviceId], [Tarrif_Id], [STAKEHOLDER_ID], [SUBSCRIBER_ID], [USAGE_DURATION], [AMOUNT_CONSUMPTION_PER_HR], [MONTHLY_DURATION_PER_HR], [TIME_OF_TRANSACTION], [Base_Location], [BillingMonth]) VALUES (1025, 5, 4, N'ade.tunde@appleelectric.com', 6, 744, CAST(97.554529875600000 AS Decimal(19, 15)), CAST(0.131121679940322 AS Decimal(19, 15)), CAST(0x0000ABCE003FCAF4 AS DateTime), N'Lagos', N'May')
INSERT [dbo].[billingsInfos] ([Billings_Id], [DEviceId], [Tarrif_Id], [STAKEHOLDER_ID], [SUBSCRIBER_ID], [USAGE_DURATION], [AMOUNT_CONSUMPTION_PER_HR], [MONTHLY_DURATION_PER_HR], [TIME_OF_TRANSACTION], [Base_Location], [BillingMonth]) VALUES (1028, 11, 4, N'ade.tunde@appleelectric.com', 4, 744, CAST(32.771849412660934 AS Decimal(19, 15)), CAST(0.044048184694436 AS Decimal(19, 15)), CAST(0x0000ABCE003FCAF5 AS DateTime), N'Lagos', N'May')
INSERT [dbo].[billingsInfos] ([Billings_Id], [DEviceId], [Tarrif_Id], [STAKEHOLDER_ID], [SUBSCRIBER_ID], [USAGE_DURATION], [AMOUNT_CONSUMPTION_PER_HR], [MONTHLY_DURATION_PER_HR], [TIME_OF_TRANSACTION], [Base_Location], [BillingMonth]) VALUES (1030, 5, 4, N'ade.tunde@appleelectric.com', 6, 744, CAST(97.554529875600000 AS Decimal(19, 15)), CAST(0.131121679940322 AS Decimal(19, 15)), CAST(0x0000ABCE0187FC41 AS DateTime), N'Lagos', N'May')
INSERT [dbo].[billingsInfos] ([Billings_Id], [DEviceId], [Tarrif_Id], [STAKEHOLDER_ID], [SUBSCRIBER_ID], [USAGE_DURATION], [AMOUNT_CONSUMPTION_PER_HR], [MONTHLY_DURATION_PER_HR], [TIME_OF_TRANSACTION], [Base_Location], [BillingMonth]) VALUES (1031, 5, 4, N'ade.tunde@appleelectric.com', 6, 744, CAST(97.554529875600000 AS Decimal(19, 15)), CAST(0.131121679940322 AS Decimal(19, 15)), CAST(0x0000ABCE0187FC41 AS DateTime), N'Lagos', N'May')
INSERT [dbo].[billingsInfos] ([Billings_Id], [DEviceId], [Tarrif_Id], [STAKEHOLDER_ID], [SUBSCRIBER_ID], [USAGE_DURATION], [AMOUNT_CONSUMPTION_PER_HR], [MONTHLY_DURATION_PER_HR], [TIME_OF_TRANSACTION], [Base_Location], [BillingMonth]) VALUES (1034, 11, 4, N'ade.tunde@appleelectric.com', 4, 744, CAST(32.771849412660934 AS Decimal(19, 15)), CAST(0.044048184694436 AS Decimal(19, 15)), CAST(0x0000ABCE0187FC42 AS DateTime), N'Lagos', N'June')
INSERT [dbo].[billingsInfos] ([Billings_Id], [DEviceId], [Tarrif_Id], [STAKEHOLDER_ID], [SUBSCRIBER_ID], [USAGE_DURATION], [AMOUNT_CONSUMPTION_PER_HR], [MONTHLY_DURATION_PER_HR], [TIME_OF_TRANSACTION], [Base_Location], [BillingMonth]) VALUES (1049, 2, 1, N'ade.tunde@appleelectric.com', 5, 33, CAST(172.453537591086409 AS Decimal(19, 15)), CAST(5.225864775487466 AS Decimal(19, 15)), CAST(0x0000ABD1001E60ED AS DateTime), N'Lagos', N'August')
SET IDENTITY_INSERT [dbo].[billingsInfos] OFF
SET IDENTITY_INSERT [dbo].[Category5] ON 

INSERT [dbo].[Category5] ([CatId], [CategoryName], [isselected]) VALUES (1, N'Nigeria', 0)
INSERT [dbo].[Category5] ([CatId], [CategoryName], [isselected]) VALUES (2, N'Niger', 0)
INSERT [dbo].[Category5] ([CatId], [CategoryName], [isselected]) VALUES (3, N'USA', 0)
INSERT [dbo].[Category5] ([CatId], [CategoryName], [isselected]) VALUES (4, N'UK', 0)
INSERT [dbo].[Category5] ([CatId], [CategoryName], [isselected]) VALUES (5, N'India', 0)
INSERT [dbo].[Category5] ([CatId], [CategoryName], [isselected]) VALUES (6, N'China', 0)
INSERT [dbo].[Category5] ([CatId], [CategoryName], [isselected]) VALUES (7, N'Canada', 1)
SET IDENTITY_INSERT [dbo].[Category5] OFF
SET IDENTITY_INSERT [dbo].[DEVICEINFO] ON 

INSERT [dbo].[DEVICEINFO] ([DEVICE_ID], [SUBSCRIBER_ID], [STAKEHOLDER_ID], [DEVICE_NAME], [IMEI_NUMBER], [ADDRESS], [BUS_STOP], [LGA], [STATE], [COUNTRY], [VERIFY_ADDRESS], [DELIVERY_FLAG], [FLAG_OPERATION], [VERIFY_ADDRESS_DATE], [DELIVERY_FLAG_DATE], [TRANSACTION_DATE]) VALUES (5, 4, N'ade.tunde@appleelectric.com', N'SmartTest', N'44ac3030', N'No30, Kaz Ventures Close', N'Owode', N'Owode', N'Ogun', N'Nigeria', N'Y', N'Y', N'Y', CAST(0x0000ABB60048953A AS DateTime), CAST(0x0000ABB60048953A AS DateTime), CAST(0x0000ABB60048953A AS DateTime))
INSERT [dbo].[DEVICEINFO] ([DEVICE_ID], [SUBSCRIBER_ID], [STAKEHOLDER_ID], [DEVICE_NAME], [IMEI_NUMBER], [ADDRESS], [BUS_STOP], [LGA], [STATE], [COUNTRY], [VERIFY_ADDRESS], [DELIVERY_FLAG], [FLAG_OPERATION], [VERIFY_ADDRESS_DATE], [DELIVERY_FLAG_DATE], [TRANSACTION_DATE]) VALUES (12, 5, N'ade.tunde@appleelectric.com', N'Smartmeter_INV', N'4458ab68', N'No13, Olambe street Ajah', N'Emmanuel', N'Ibeju Lekki', N'Lagos', N'Nigeria', N'Y', N'Y', N'Y', CAST(0x0000ABB6007F715F AS DateTime), CAST(0x0000ABB6007F715F AS DateTime), CAST(0x0000ABB6007F715F AS DateTime))
INSERT [dbo].[DEVICEINFO] ([DEVICE_ID], [SUBSCRIBER_ID], [STAKEHOLDER_ID], [DEVICE_NAME], [IMEI_NUMBER], [ADDRESS], [BUS_STOP], [LGA], [STATE], [COUNTRY], [VERIFY_ADDRESS], [DELIVERY_FLAG], [FLAG_OPERATION], [VERIFY_ADDRESS_DATE], [DELIVERY_FLAG_DATE], [TRANSACTION_DATE]) VALUES (14, 6, N'ade.tunde@appleelectric.com', N'Smartmeter_ILA', N'5573yn91', N'No,54 Animashan Street Iju-Ishaga', N'Station', N'Ifako-ijaiye', N'Lagos', N'Nigeria', N'N', N'N', N'N', CAST(0x0000ABB600F608E8 AS DateTime), CAST(0x0000ABB600F608E8 AS DateTime), CAST(0x0000ABB600F608E8 AS DateTime))
INSERT [dbo].[DEVICEINFO] ([DEVICE_ID], [SUBSCRIBER_ID], [STAKEHOLDER_ID], [DEVICE_NAME], [IMEI_NUMBER], [ADDRESS], [BUS_STOP], [LGA], [STATE], [COUNTRY], [VERIFY_ADDRESS], [DELIVERY_FLAG], [FLAG_OPERATION], [VERIFY_ADDRESS_DATE], [DELIVERY_FLAG_DATE], [TRANSACTION_DATE]) VALUES (15, 6, NULL, N'Smartmeter_IBN', N'3035bj19', N'no12 Daltank Avenue ', N'Pepsi', N'Isoko', N'Benue', N'Nigeria', N'N', N'N', N'N', CAST(0x0000ABB601095DA9 AS DateTime), CAST(0x0000ABB601095DA9 AS DateTime), CAST(0x0000ABB601095DA9 AS DateTime))
SET IDENTITY_INSERT [dbo].[DEVICEINFO] OFF
SET IDENTITY_INSERT [dbo].[deviceInfos] ON 

INSERT [dbo].[deviceInfos] ([Device_ID], [Subscriber_ID], [STAKEHOLDER_ID], [Imei_Number], [Device_Name], [TypeOfHouseStatus], [Address], [Bus_Stop], [State], [Lga], [Verify_Address], [Delivery_Flag], [Flag_Operation], [Device_Status], [Country], [Transaction_Date]) VALUES (1, 4, N'ade.tunde@appleelectric.com', N'44ac3030', N'Smartmeter_ILA', N'R2TP', N'No30, Kaz Ventures Close', N'Owode', N'Ogun', N'Owode', N'Y', N'Y', N'Y', NULL, N'Nigeria', CAST(0x07800151EA2411410B AS DateTime2))
INSERT [dbo].[deviceInfos] ([Device_ID], [Subscriber_ID], [STAKEHOLDER_ID], [Imei_Number], [Device_Name], [TypeOfHouseStatus], [Address], [Bus_Stop], [State], [Lga], [Verify_Address], [Delivery_Flag], [Flag_Operation], [Device_Status], [Country], [Transaction_Date]) VALUES (2, 5, N'ade.tunde@appleelectric.com', N'4458ab68', N'Smartmeter_INV', N'R2SP', N'No13, Olambe street Ajah', N'Emmanuel', N'Lagos', N'Ibeju Lekki', N'Y', N'Y', N'Y', NULL, N'Nigeria', CAST(0x0700E0EED14011410B AS DateTime2))
INSERT [dbo].[deviceInfos] ([Device_ID], [Subscriber_ID], [STAKEHOLDER_ID], [Imei_Number], [Device_Name], [TypeOfHouseStatus], [Address], [Bus_Stop], [State], [Lga], [Verify_Address], [Delivery_Flag], [Flag_Operation], [Device_Status], [Country], [Transaction_Date]) VALUES (5, 6, N'ade.tunde@appleelectric.com', N'5573yn91', N'Smartmeter_ILA', N'R3', N'No,54 Animashan Street Iju-Ishaga', N'Magistate', N'Lagos', N'Ogba Aguda', N'N', N'N', N'N', NULL, N'Nigeria', CAST(0x07805BB5237D11410B AS DateTime2))
INSERT [dbo].[deviceInfos] ([Device_ID], [Subscriber_ID], [STAKEHOLDER_ID], [Imei_Number], [Device_Name], [TypeOfHouseStatus], [Address], [Bus_Stop], [State], [Lga], [Verify_Address], [Delivery_Flag], [Flag_Operation], [Device_Status], [Country], [Transaction_Date]) VALUES (9, 5, N'ade.tunde@appleelectric.com', N'3035it89', N'Smartmeter_ILA', N'R3', N'no,46 Adisa haruna Str', N'Toyin', N'Lagos', N'Ifako-ijaiye', N'N', N'N', N'N', NULL, N'Nigeria', CAST(0x07205EBE60121E410B AS DateTime2))
INSERT [dbo].[deviceInfos] ([Device_ID], [Subscriber_ID], [STAKEHOLDER_ID], [Imei_Number], [Device_Name], [TypeOfHouseStatus], [Address], [Bus_Stop], [State], [Lga], [Verify_Address], [Delivery_Flag], [Flag_Operation], [Device_Status], [Country], [Transaction_Date]) VALUES (11, 4, N'ade.tunde@appleelectric.com', N'3035jj67', N'Smartmeter_ILA', N'R4', N'11,Onitiri Street, Bariga', N'Medical', N'Lagos', N'Bariga', N'Y', N'Y', N'N', NULL, N'Nigeria', CAST(0x07A242543D071F410B AS DateTime2))
SET IDENTITY_INSERT [dbo].[deviceInfos] OFF
SET IDENTITY_INSERT [dbo].[IOTDeviceSmartMeter] ON 

INSERT [dbo].[IOTDeviceSmartMeter] ([Id], [DEviceId], [messageId], [EnergyAmountkWh], [VoltageReading], [PowerReading], [ConnectionStatus], [PartitionId], [EventProcessedUtcTime], [EventEnqueuedUtcTime], [StartDatetime], [EndDatetime]) VALUES (5, 5, N'0', CAST(7.994529875600000 AS Decimal(17, 15)), CAST(9.994529875600000 AS Decimal(17, 15)), CAST(5.234452987560000 AS Decimal(17, 15)), N'2', N'0', CAST(0x0000ABB9016A5C64 AS DateTime), CAST(0x0000ABB9016932CD AS DateTime), CAST(0x0000ABC3001247ED AS DateTime), CAST(0x0000ABCD00E50038 AS DateTime))
INSERT [dbo].[IOTDeviceSmartMeter] ([Id], [DEviceId], [messageId], [EnergyAmountkWh], [VoltageReading], [PowerReading], [ConnectionStatus], [PartitionId], [EventProcessedUtcTime], [EventEnqueuedUtcTime], [StartDatetime], [EndDatetime]) VALUES (6, 11, N'0', CAST(32.771849412660934 AS Decimal(17, 15)), CAST(44.222452987560000 AS Decimal(17, 15)), CAST(71.222452987560000 AS Decimal(17, 15)), N'1', N'0', CAST(0x0000ABB9016A5C64 AS DateTime), CAST(0x0000ABB90168820A AS DateTime), CAST(0x0000AC9F000C1166 AS DateTime), CAST(0x0000AC3C00F69F62 AS DateTime))
INSERT [dbo].[IOTDeviceSmartMeter] ([Id], [DEviceId], [messageId], [EnergyAmountkWh], [VoltageReading], [PowerReading], [ConnectionStatus], [PartitionId], [EventProcessedUtcTime], [EventEnqueuedUtcTime], [StartDatetime], [EndDatetime]) VALUES (7, 11, N'0', CAST(73.452987560000000 AS Decimal(17, 15)), CAST(12.615263401100000 AS Decimal(17, 15)), CAST(24.149016987828777 AS Decimal(17, 15)), N'2', N'0', CAST(0x0000ABB9016A5C64 AS DateTime), CAST(0x0000ABB90168DA49 AS DateTime), CAST(0x0000ABD1001BF103 AS DateTime), CAST(0x0000AC9F00C91A8F AS DateTime))
INSERT [dbo].[IOTDeviceSmartMeter] ([Id], [DEviceId], [messageId], [EnergyAmountkWh], [VoltageReading], [PowerReading], [ConnectionStatus], [PartitionId], [EventProcessedUtcTime], [EventEnqueuedUtcTime], [StartDatetime], [EndDatetime]) VALUES (8, 5, N'0', CAST(97.554529875600000 AS Decimal(17, 15)), CAST(38.554529875600000 AS Decimal(17, 15)), CAST(80.554529875600000 AS Decimal(17, 15)), N'1', N'0', CAST(0x0000ABB9016A5C64 AS DateTime), CAST(0x0000ABB901682A86 AS DateTime), CAST(0x0000ABC6016A5B48 AS DateTime), CAST(0x0000ABE5016A5B48 AS DateTime))
INSERT [dbo].[IOTDeviceSmartMeter] ([Id], [DEviceId], [messageId], [EnergyAmountkWh], [VoltageReading], [PowerReading], [ConnectionStatus], [PartitionId], [EventProcessedUtcTime], [EventEnqueuedUtcTime], [StartDatetime], [EndDatetime]) VALUES (1002, 9, N'0', CAST(42.908231976571635 AS Decimal(17, 15)), CAST(14.852650376911240 AS Decimal(17, 15)), CAST(52.397932079000000 AS Decimal(17, 15)), N'2', N'0', CAST(0x0000ABC6016A5B48 AS DateTime), CAST(0x0000ABB9016A5B48 AS DateTime), CAST(0x0000ABC6016A5B48 AS DateTime), CAST(0x0000ABE5016A5B48 AS DateTime))
INSERT [dbo].[IOTDeviceSmartMeter] ([Id], [DEviceId], [messageId], [EnergyAmountkWh], [VoltageReading], [PowerReading], [ConnectionStatus], [PartitionId], [EventProcessedUtcTime], [EventEnqueuedUtcTime], [StartDatetime], [EndDatetime]) VALUES (1003, 2, N'0', CAST(62.178099657143584 AS Decimal(17, 15)), CAST(11.002237645461000 AS Decimal(17, 15)), CAST(62.178099657143584 AS Decimal(17, 15)), N'2', N'0', CAST(0x0000ABB9016A5C64 AS DateTime), CAST(0x0000ABB90167D251 AS DateTime), CAST(0x0000ABC400164C34 AS DateTime), CAST(0x0000ABCA00DC0D34 AS DateTime))
INSERT [dbo].[IOTDeviceSmartMeter] ([Id], [DEviceId], [messageId], [EnergyAmountkWh], [VoltageReading], [PowerReading], [ConnectionStatus], [PartitionId], [EventProcessedUtcTime], [EventEnqueuedUtcTime], [StartDatetime], [EndDatetime]) VALUES (1004, 2, N'0', CAST(36.822450933942825 AS Decimal(17, 15)), CAST(11.586102835872140 AS Decimal(17, 15)), CAST(64.618192799990808 AS Decimal(17, 15)), N'2', N'0', CAST(0x0000ABBA016A5B48 AS DateTime), CAST(0x0000ABB90104F190 AS DateTime), CAST(0x0000ABCA00DC0D34 AS DateTime), CAST(0x0000ABE400DC0D34 AS DateTime))
INSERT [dbo].[IOTDeviceSmartMeter] ([Id], [DEviceId], [messageId], [EnergyAmountkWh], [VoltageReading], [PowerReading], [ConnectionStatus], [PartitionId], [EventProcessedUtcTime], [EventEnqueuedUtcTime], [StartDatetime], [EndDatetime]) VALUES (1005, 2, N'0', CAST(53.452987560000000 AS Decimal(17, 15)), CAST(18.615263401100000 AS Decimal(17, 15)), CAST(44.149016987828777 AS Decimal(17, 15)), N'1', N'0', CAST(0x0000ABBB00A49A48 AS DateTime), CAST(0x0000ABB900F476D0 AS DateTime), CAST(0x0000ABCA00DC0D34 AS DateTime), CAST(0x0000ABCA00DC0D34 AS DateTime))
INSERT [dbo].[IOTDeviceSmartMeter] ([Id], [DEviceId], [messageId], [EnergyAmountkWh], [VoltageReading], [PowerReading], [ConnectionStatus], [PartitionId], [EventProcessedUtcTime], [EventEnqueuedUtcTime], [StartDatetime], [EndDatetime]) VALUES (1006, 2, N'0', CAST(73.452987000000000 AS Decimal(17, 15)), CAST(38.554529875600000 AS Decimal(17, 15)), CAST(52.397932079000000 AS Decimal(17, 15)), N'2', N'0', CAST(0x0000ABC100A49A48 AS DateTime), CAST(0x0000ABC100A49A48 AS DateTime), CAST(0x0000ABCA00DC0D34 AS DateTime), CAST(0x0000ABCA00DC0D34 AS DateTime))
INSERT [dbo].[IOTDeviceSmartMeter] ([Id], [DEviceId], [messageId], [EnergyAmountkWh], [VoltageReading], [PowerReading], [ConnectionStatus], [PartitionId], [EventProcessedUtcTime], [EventEnqueuedUtcTime], [StartDatetime], [EndDatetime]) VALUES (1007, 2, N'0', CAST(0.000000000000000 AS Decimal(17, 15)), CAST(44.222452987560000 AS Decimal(17, 15)), CAST(24.149016987828777 AS Decimal(17, 15)), N'1', N'0', CAST(0x0000ABBF00A49A48 AS DateTime), CAST(0x0000ABB901156C50 AS DateTime), CAST(0x0000ABCB00DC0D34 AS DateTime), CAST(0x0000ABCB00DC0D34 AS DateTime))
SET IDENTITY_INSERT [dbo].[IOTDeviceSmartMeter] OFF
SET IDENTITY_INSERT [dbo].[SUBSCRIBER] ON 

INSERT [dbo].[SUBSCRIBER] ([SUBSCRIBER_ID], [FIRST_NAME], [LAST_NAME], [PHONE], [GENDER], [DOFB], [EMAIL], [ADDRESS], [STATE], [COUNTRY], [STAKEHOLDER_ID], [DATE_REGISTERED]) VALUES (4, N'Ladi', N'Tiwa', N'08023045556', N'F', N'09/10/1984', N'tiwaprecious@gmail.com', N'No2,Lamidi Str Mushin', N'Lagos', N'Nigeria', N'ade.tunde@appleelectric.com', CAST(0x0000AB9800854246 AS DateTime))
INSERT [dbo].[SUBSCRIBER] ([SUBSCRIBER_ID], [FIRST_NAME], [LAST_NAME], [PHONE], [GENDER], [DOFB], [EMAIL], [ADDRESS], [STATE], [COUNTRY], [STAKEHOLDER_ID], [DATE_REGISTERED]) VALUES (5, N'Joseph', N'Lasons', N'08067041115', N'M', N'09/10/1978', N'lason@gmail.com', N'No3, Sobiye St Adebimpe Opeilu', N'Lagos', N'Nigeria', N'ade.tunde@appleelectric.com', CAST(0x0000AB9900043B3F AS DateTime))
INSERT [dbo].[SUBSCRIBER] ([SUBSCRIBER_ID], [FIRST_NAME], [LAST_NAME], [PHONE], [GENDER], [DOFB], [EMAIL], [ADDRESS], [STATE], [COUNTRY], [STAKEHOLDER_ID], [DATE_REGISTERED]) VALUES (6, N'Jimmy', N'Klestron', N'08023060678', N'M', N'02/10/1978', N'klestron@yahoo.com', N'no30,Abule Str, Apapa', N'Lagos', N'Nigeria', N'ade.tunde@appleelectric.com', CAST(0x0000AB9900293631 AS DateTime))
INSERT [dbo].[SUBSCRIBER] ([SUBSCRIBER_ID], [FIRST_NAME], [LAST_NAME], [PHONE], [GENDER], [DOFB], [EMAIL], [ADDRESS], [STATE], [COUNTRY], [STAKEHOLDER_ID], [DATE_REGISTERED]) VALUES (1002, N'Shola', N'Maupe', N'08067041115', N'F', N'08/05/1984 12:00:00 AM', N'jacty@gmail.com', N'No26, Ademola Banire Close', N'Lagos', N'Nigeria', NULL, CAST(0x0000ABB50040388B AS DateTime))
INSERT [dbo].[SUBSCRIBER] ([SUBSCRIBER_ID], [FIRST_NAME], [LAST_NAME], [PHONE], [GENDER], [DOFB], [EMAIL], [ADDRESS], [STATE], [COUNTRY], [STAKEHOLDER_ID], [DATE_REGISTERED]) VALUES (1003, N'Kemi', N'Adebanjoko', N'08067041115', N'F', N'07/08/1984 12:00:00 AM', N'jacty@gmail.com', N'No3, Adebasiru Close', N'Lagos', N'Nigeria', NULL, CAST(0x0000ABB500428FE0 AS DateTime))
INSERT [dbo].[SUBSCRIBER] ([SUBSCRIBER_ID], [FIRST_NAME], [LAST_NAME], [PHONE], [GENDER], [DOFB], [EMAIL], [ADDRESS], [STATE], [COUNTRY], [STAKEHOLDER_ID], [DATE_REGISTERED]) VALUES (1004, N'Mark', N'Bala', N'07056043679', N'M', N'20/11/1940', N'markbaly@gmail.com', N'no50 Hadeja Street Hadeja', N'Kano', N'Nigeria', N'ade.tunde@appleelectric.com', CAST(0x0000ABB600F15EE1 AS DateTime))
SET IDENTITY_INSERT [dbo].[SUBSCRIBER] OFF
SET IDENTITY_INSERT [dbo].[subscriberInfos] ON 

INSERT [dbo].[subscriberInfos] ([Subscriber_ID], [STAKEHOLDER_ID], [First_Name], [Last_Name], [Phone], [Gender], [Dofb], [Email], [Address], [State], [Country], [Date_Of_Registration], [UserId]) VALUES (2, N'ade.tunde@appleelectric.com', N'Ladi', N'Fani', N'08064083334', N'M', N'12/08/1983', N'fani23@gmail.com', N'No27,Olowu bolade, Oshodi', N'Lagos', N'Nigeria', CAST(0x07000000000029410B AS DateTime2), NULL)
INSERT [dbo].[subscriberInfos] ([Subscriber_ID], [STAKEHOLDER_ID], [First_Name], [Last_Name], [Phone], [Gender], [Dofb], [Email], [Address], [State], [Country], [Date_Of_Registration], [UserId]) VALUES (3, N'ade.tunde@appleelectric.com', N'Sintia', N'Krin', N'08123654433', N'F', N'03/23/1956', N'sinsia@yahoo.com', N'No14,Amina str,Fagba', N'Lagos', N'Nigeria', CAST(0x070010DB39AD20410B AS DateTime2), NULL)
INSERT [dbo].[subscriberInfos] ([Subscriber_ID], [STAKEHOLDER_ID], [First_Name], [Last_Name], [Phone], [Gender], [Dofb], [Email], [Address], [State], [Country], [Date_Of_Registration], [UserId]) VALUES (4, N'ade.tunde@appleelectric.com', N'Audu', N'Ade', N'08095458699', N'M', N'09/20/1968', N'ade30@yahoo.com', N'N013,KingstenJones Apapa', N'Lagos', N'Nigeria', CAST(0x0700C45FF36026410B AS DateTime2), NULL)
INSERT [dbo].[subscriberInfos] ([Subscriber_ID], [STAKEHOLDER_ID], [First_Name], [Last_Name], [Phone], [Gender], [Dofb], [Email], [Address], [State], [Country], [Date_Of_Registration], [UserId]) VALUES (5, N'ade.tunde@appleelectric.com', N'Funmi', N'Idowu', N'07034776543', N'F', N'03/10/2020', N'id101@gmail.com', N'No35,Abatior Road,Agege', N'Lagos', N'Nigeria', CAST(0x0700604C49911E410B AS DateTime2), N'fec1ceb1-0803-4df3-821d-2a07afae0f0a')
INSERT [dbo].[subscriberInfos] ([Subscriber_ID], [STAKEHOLDER_ID], [First_Name], [Last_Name], [Phone], [Gender], [Dofb], [Email], [Address], [State], [Country], [Date_Of_Registration], [UserId]) VALUES (6, N'ade.tunde@appleelectric.com', N'Beke', N'Clement', N'09056734298', N'M', N'11/20/2020', N'beke@gmail.com', N'Ibafon,Apapa', N'Lagos', N'Nigeria', CAST(0x07006E080B9C27410B AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[subscriberInfos] OFF
SET IDENTITY_INSERT [dbo].[tarrifInfos] ON 

INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (1, N'ade.tunde@appleelectric.com', N'Residential Single Phase', N'R2SP', CAST(21.30 AS Decimal(18, 2)), CAST(0x070B23D211B869410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (3, N'ade.tunde@appleelectric.com', N'Residential Tripple Phase', N'R2TP', CAST(21.80 AS Decimal(18, 2)), CAST(0x07CB54AE0C182C410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (4, N'ade.tunde@appleelectric.com', N'Residential Dedicated Transformer 500KVA', N'R3', CAST(36.49 AS Decimal(18, 2)), CAST(0x07B5AB255A182C410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (5, N'ade.tunde@appleelectric.com', N'Residential Several Transformers', N'R4', CAST(36.92 AS Decimal(18, 2)), CAST(0x07E0259970182C410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (6, N'ade.tunde@appleelectric.com', N'Commercial
 Single Phase', N'C1SP', CAST(27.20 AS Decimal(18, 2)), CAST(0x07B5AFC8B53D2D410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (7, N'ade.tunde@appleelectric.com', N'Commercial
 Tripple Phase', N'C1TP', CAST(28.47 AS Decimal(18, 2)), CAST(0x0715DF84C93D2D410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (8, N'ade.tunde@appleelectric.com', N'Commercial
', N'C2', CAST(37.74 AS Decimal(18, 2)), CAST(0x07D5F0F5DB3D2D410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (9, N'ade.tunde@appleelectric.com', N'Commercial
 Dedicated Transformer 500KVA', N'C3', CAST(38.14 AS Decimal(18, 2)), CAST(0x07A0FAA7F23D2D410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (10, N'ade.tunde@appleelectric.com', N'Industrial', N'D1', CAST(28.68 AS Decimal(18, 2)), CAST(0x07550B9B003E2D410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (11, N'ade.tunde@appleelectric.com', N'Industrial', N'D2 ', CAST(38.38 AS Decimal(18, 2)), CAST(0x07E0E937063E2D410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (12, N'ade.tunde@appleelectric.com', N'Industrial Dedicated Transformer 500KVA', N'D3', CAST(38.85 AS Decimal(18, 2)), CAST(0x07207BDD113E2D410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (13, N'ade.tunde@appleelectric.com', N'Special
', N'A1', CAST(26.82 AS Decimal(18, 2)), CAST(0x07E09A401F3E2D410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (14, N'ade.tunde@appleelectric.com', N'Special
', N'A2 ', CAST(30.20 AS Decimal(18, 2)), CAST(0x0740977F293E2D410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (15, N'ade.tunde@appleelectric.com', N'Special
 Dedicated Transformer 500KVA', N'A3 ', CAST(30.36 AS Decimal(18, 2)), CAST(0x070B0AB3323E2D410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (17, N'ade.tunde@appleelectric.com', N'Street Light ', N'S1', CAST(19.42 AS Decimal(18, 2)), CAST(0x07EB4FED403E2D410B AS DateTime2))
INSERT [dbo].[tarrifInfos] ([Tarrif_Id], [STAKEHOLDER_ID], [TypeOfHouse], [TypeOfHouseStatus], [TarrifAmount], [DateOfTarrifTransaction]) VALUES (18, N'ade.tunde@appleelectric.com', N'church Light', NULL, CAST(23.45 AS Decimal(18, 2)), CAST(0x07F53F4A1BB869410B AS DateTime2))
SET IDENTITY_INSERT [dbo].[tarrifInfos] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 8/8/2020 7:43:10 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 8/8/2020 7:43:10 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 8/8/2020 7:43:10 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 8/8/2020 7:43:10 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 8/8/2020 7:43:10 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [EmailIndex]    Script Date: 8/8/2020 7:43:10 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 8/8/2020 7:43:10 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_stackholderInfoModels_UserId]    Script Date: 8/8/2020 7:43:10 AM ******/
CREATE NONCLUSTERED INDEX [IX_stackholderInfoModels_UserId] ON [dbo].[stackholderInfoModels]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_subscriberInfos_UserId]    Script Date: 8/8/2020 7:43:10 AM ******/
CREATE NONCLUSTERED INDEX [IX_subscriberInfos_UserId] ON [dbo].[subscriberInfos]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[billingsInfos] ADD  DEFAULT (getdate()) FOR [TIME_OF_TRANSACTION]
GO
ALTER TABLE [dbo].[DEVICEINFO] ADD  DEFAULT ('F') FOR [VERIFY_ADDRESS]
GO
ALTER TABLE [dbo].[DEVICEINFO] ADD  DEFAULT ('N') FOR [DELIVERY_FLAG]
GO
ALTER TABLE [dbo].[DEVICEINFO] ADD  DEFAULT ('N') FOR [FLAG_OPERATION]
GO
ALTER TABLE [dbo].[DEVICEINFO] ADD  DEFAULT (getdate()) FOR [VERIFY_ADDRESS_DATE]
GO
ALTER TABLE [dbo].[DEVICEINFO] ADD  DEFAULT (getdate()) FOR [DELIVERY_FLAG_DATE]
GO
ALTER TABLE [dbo].[DEVICEINFO] ADD  DEFAULT (getdate()) FOR [TRANSACTION_DATE]
GO
ALTER TABLE [dbo].[SUBSCRIBER] ADD  DEFAULT (getdate()) FOR [DATE_REGISTERED]
GO
ALTER TABLE [dbo].[tarrifInfos] ADD  DEFAULT (getdate()) FOR [DateOfTarrifTransaction]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[stackholderInfoModels]  WITH CHECK ADD  CONSTRAINT [FK_stackholderInfoModels_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[stackholderInfoModels] CHECK CONSTRAINT [FK_stackholderInfoModels_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[subscriberInfos]  WITH CHECK ADD  CONSTRAINT [FK_subscriberInfos_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[subscriberInfos] CHECK CONSTRAINT [FK_subscriberInfos_AspNetUsers_UserId]
GO
USE [master]
GO
ALTER DATABASE [SmartMeterData.sql] SET  READ_WRITE 
GO
