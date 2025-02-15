﻿using System;

using Accounts;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Facebook.LoginKit {
	// @interface FBSDKDeviceLoginCodeInfo : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "FBSDKDeviceLoginCodeInfo")]
	interface DeviceLoginCodeInfo {
		// @property (readonly, copy, nonatomic) NSString * _Nonnull identifier;
		[Export ("identifier")]
		string Identifier { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull loginCode;
		[Export ("loginCode")]
		string LoginCode { get; }

		// @property (readonly, copy, nonatomic) NSURL * _Nonnull verificationURL;
		[Export ("verificationURL", ArgumentSemantic.Copy)]
		NSUrl VerificationUrl { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull expirationDate;
		[Export ("expirationDate", ArgumentSemantic.Copy)]
		NSDate ExpirationDate { get; }

		// @property (readonly, assign, nonatomic) NSUInteger pollingInterval;
		[Export ("pollingInterval")]
		nuint PollingInterval { get; }
	}

	interface IDeviceLoginManagerDelegate { }

	// @protocol FBSDKDeviceLoginManagerDelegate <NSObject>
	[Model (AutoGeneratedName = true)]
	[Protocol]
	[BaseType (typeof (NSObject), Name = "FBSDKDeviceLoginManagerDelegate")]
	interface DeviceLoginManagerDelegate {
		// @required -(void)deviceLoginManager:(FBSDKDeviceLoginManager * _Nonnull)loginManager startedWithCodeInfo:(FBSDKDeviceLoginCodeInfo * _Nonnull)codeInfo;
		[Abstract]
		[EventArgs ("DeviceLoginManagerStarted")]
		[Export ("deviceLoginManager:startedWithCodeInfo:")]
		void Started (DeviceLoginManager loginManager, DeviceLoginCodeInfo codeInfo);

		// @required -(void)deviceLoginManager:(FBSDKDeviceLoginManager * _Nonnull)loginManager completedWithResult:(FBSDKDeviceLoginManagerResult * _Nullable)result error:(NSError * _Nullable)error;
		[Abstract]
		[EventArgs ("DeviceLoginManagerCompleted")]
		[Export ("deviceLoginManager:completedWithResult:error:")]
		void Completed (DeviceLoginManager loginManager, [NullAllowed] DeviceLoginManagerResult result, [NullAllowed] NSError error);
	}

	// @interface FBSDKDeviceLoginManager : NSObject <NSNetServiceDelegate>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject),
		   Name = "FBSDKDeviceLoginManager",
		   Delegates = new [] { "Delegate" },
		   Events = new [] { typeof (DeviceLoginManagerDelegate) })]
	interface DeviceLoginManager : INSNetServiceDelegate {
		// -(instancetype _Nonnull)initWithPermissions:(NSArray<NSString *> * _Nullable)permissions enableSmartLogin:(BOOL)enableSmartLogin __attribute__((objc_designated_initializer));
		[Export ("initWithPermissions:enableSmartLogin:")]
		[DesignatedInitializer]
		IntPtr Constructor (string [] permissions, bool enableSmartLogin);

		// @property (nonatomic, weak) id<FBSDKDeviceLoginManagerDelegate> _Nullable delegate;
		[NullAllowed]
		[Export ("delegate", ArgumentSemantic.Weak)]
		IDeviceLoginManagerDelegate Delegate { get; set; }

		// @property (readonly, copy, nonatomic) NSArray<NSString *> * _Nullable permissions;
		[Export ("permissions", ArgumentSemantic.Copy)]
		string [] Permissions { get; }

		// @property (copy, nonatomic) NSURL * _Nullable redirectURL;
		[NullAllowed, Export ("redirectURL", ArgumentSemantic.Copy)]
		NSUrl RedirectUrl { get; set; }

		// -(void)start;
		[Export ("start")]
		void Start ();

		// -(void)cancel;
		[Export ("cancel")]
		void Cancel ();
	}

	// @interface FBSDKDeviceLoginManagerResult : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "FBSDKDeviceLoginManagerResult")]
	interface DeviceLoginManagerResult {
		// @property (readonly, nonatomic, strong) FBSDKAccessToken * _Nullable accessToken;
		[NullAllowed, Export ("accessToken", ArgumentSemantic.Strong)]
		CoreKit.AccessToken AccessToken { get; }

		// @property (readonly, getter = isCancelled, assign, nonatomic) BOOL cancelled;
		[Export ("isCancelled")]
		bool IsCancelled { get; }
	}

	// @interface FBSDKLoginButton : FBSDKButton
	[BaseType (typeof (CoreKit.Button),
		Name = "FBSDKLoginButton",
		Delegates = new [] { "Delegate" },
		Events = new [] { typeof (LoginButtonDelegate) })]
	interface LoginButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		// extern NSString *const FBSDKLoginErrorDomain;
		[Field ("FBSDKLoginErrorDomain", "__Internal")]
		NSString ErrorDomain { get; }

		// @property (assign, nonatomic) FBSDKDefaultAudience defaultAudience;
		[Export ("defaultAudience", ArgumentSemantic.Assign)]
		DefaultAudience DefaultAudience { get; set; }

		// @property (nonatomic, weak) id<FBSDKLoginButtonDelegate> delegate;
		[NullAllowed]
		[Export ("delegate", ArgumentSemantic.Weak)]
		ILoginButtonDelegate Delegate { get; set; }

		// @property (assign, nonatomic) FBSDKLoginBehavior loginBehavior;
		[Export ("loginBehavior", ArgumentSemantic.Assign)]
		LoginBehavior LoginBehavior { get; set; }

		// @property (copy, nonatomic) NSArray<NSString *> * _Nonnull permissions;
		[Export ("permissions", ArgumentSemantic.Copy)]
		string [] Permissions { get; set; }

		// @property (assign, nonatomic) FBSDKLoginButtonTooltipBehavior tooltipBehavior;
		[Export ("tooltipBehavior", ArgumentSemantic.Assign)]
		LoginButtonTooltipBehavior TooltipBehavior { get; set; }

		// @property (assign, nonatomic) FBSDKTooltipColorStyle tooltipColorStyle;
		[Export ("tooltipColorStyle", ArgumentSemantic.Assign)]
		TooltipColorStyle TooltipColorStyle { get; set; }
	}

	interface ILoginButtonDelegate {

	}

	// @protocol FBSDKLoginButtonDelegate <NSObject>
	[Model (AutoGeneratedName = true)]
	[Protocol]
	[BaseType (typeof (NSObject), Name = "FBSDKLoginButtonDelegate")]
	interface LoginButtonDelegate {

		// @required -(void)loginButton:(FBSDKLoginButton *)loginButton didCompleteWithResult:(FBSDKLoginManagerLoginResult *)result error:(NSError *)error;
		[Abstract]
		[EventArgs ("LoginButtonCompleted")]
		[EventName ("Completed")]
		[Export ("loginButton:didCompleteWithResult:error:")]
		void DidComplete (LoginButton loginButton, [NullAllowed] LoginManagerLoginResult result, [NullAllowed] NSError error);

		// @required -(void)loginButtonDidLogOut:(FBSDKLoginButton *)loginButton;
		[Abstract]
		[EventArgs ("LoginButtonLoggedOut")]
		[EventName ("LoggedOut")]
		[Export ("loginButtonDidLogOut:")]
		void DidLogOut (LoginButton loginButton);

		[DelegateName ("LoginButtonWillLogin")]
		[DefaultValue (true)]
		[Export ("loginButtonWillLogin:")]
		bool WillLogin (LoginButton loginButton);
	}

	// typedef void (^FBSDKLoginManagerLoginResultBlock)(FBSDKLoginManagerLoginResult * _Nullable, NSError * _Nullable);
	delegate void LoginManagerLoginResultBlockHandler ([NullAllowed] LoginManagerLoginResult result, [NullAllowed] NSError error);

	// @interface FBSDKLoginManager : NSObject
	[BaseType (typeof (NSObject), Name = "FBSDKLoginManager")]
	interface LoginManager {

		// @property (strong, nonatomic) NSString *authType;
		[BindAs (typeof (LoginAuthType))]
		[Export ("authType", ArgumentSemantic.Strong)]
		NSString AuthType { get; set; }

		// @property (assign, nonatomic) FBSDKDefaultAudience defaultAudience;
		[Export ("defaultAudience", ArgumentSemantic.Assign)]
		DefaultAudience DefaultAudience { get; set; }

		// @property (assign, nonatomic) FBSDKLoginBehavior loginBehavior;
		[Export ("loginBehavior", ArgumentSemantic.Assign)]
		LoginBehavior LoginBehavior { get; set; }

		// -(void)logInWithPermissions:(NSArray<NSString *> * _Nonnull)permissions fromViewController:(UIViewController * _Nullable)fromViewController handler:(FBSDKLoginManagerLoginResultBlock _Nullable)handler;
		[Export ("logInWithPermissions:fromViewController:handler:")]
		void LogIn (string [] permissions, [NullAllowed] UIViewController fromViewController, [NullAllowed] LoginManagerLoginResultBlockHandler handler);

		// -(void)reauthorizeDataAccess:(UIViewController *)fromViewController handler:(FBSDKLoginManagerLoginResultBlockHandler)handler;
		[Export ("reauthorizeDataAccess:handler:")]
		void ReauthorizeDataAccess (UIViewController fromViewController, LoginManagerLoginResultBlockHandler handler);

		// -(void)logOut;
		[Export ("logOut")]
		void LogOut ();
	}

	// @interface FBSDKLoginManagerLoginResult : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "FBSDKLoginManagerLoginResult")]
	interface LoginManagerLoginResult {

		// @property (copy, nonatomic) FBSDKAccessToken * token;
		[NullAllowed]
		[Export ("token", ArgumentSemantic.Copy)]
		CoreKit.AccessToken Token { get; set; }

		// @property (readonly, nonatomic) BOOL isCancelled;
		[Export ("isCancelled")]
		bool IsCancelled { get; }

		// @property (copy, nonatomic) NSSet * grantedPermissions;
		[NullAllowed]
		[Export ("grantedPermissions", ArgumentSemantic.Copy)]
		NSSet GrantedPermissions { get; set; }

		// @property (copy, nonatomic) NSSet * declinedPermissions;
		[NullAllowed]
		[Export ("declinedPermissions", ArgumentSemantic.Copy)]
		NSSet DeclinedPermissions { get; set; }

		// -(instancetype)initWithToken:(FBSDKAccessToken *)token isCancelled:(BOOL)isCancelled grantedPermissions:(NSSet *)grantedPermissions declinedPermissions:(NSSet *)declinedPermissions __attribute__((objc_designated_initializer));
		[DesignatedInitializer]
		[Export ("initWithToken:isCancelled:grantedPermissions:declinedPermissions:")]
		IntPtr Constructor ([NullAllowed] CoreKit.AccessToken token, bool isCancelled, [NullAllowed] NSSet grantedPermissions, [NullAllowed] NSSet declinedPermissions);
	}

	// @interface FBSDKLoginTooltipView : FBSDKTooltipView
	[BaseType (typeof (TooltipView),
		Name = "FBSDKLoginTooltipView",
		Delegates = new [] { "Delegate" },
		Events = new [] { typeof (LoginTooltipViewDelegate) })]
	interface LoginTooltipView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		// @property (assign, nonatomic) id<FBSDKLoginTooltipViewDelegate> delegate;
		[NullAllowed]
		[Export ("delegate", ArgumentSemantic.Weak)]
		ILoginTooltipViewDelegate Delegate { get; set; }

		// @property (getter = shouldForceDisplay, assign, nonatomic) BOOL forceDisplay;
		[Export ("forceDisplay")]
		bool ForceDisplay { [Bind ("shouldForceDisplay")] get; set; }
	}

	interface ILoginTooltipViewDelegate {

	}

	// @protocol FBSDKLoginTooltipViewDelegate <NSObject>
	[Model (AutoGeneratedName = true)]
	[Protocol]
	[BaseType (typeof (NSObject), Name = "FBSDKLoginTooltipViewDelegate")]
	interface LoginTooltipViewDelegate {

		// @optional -(BOOL)loginTooltipView:(FBSDKLoginTooltipView *)view shouldAppear:(BOOL)appIsEligible;
		[DelegateName ("LoginTooltipViewShouldAppear")]
		[DefaultValue (true)]
		[Export ("loginTooltipView:shouldAppear:")]
		bool ShouldAppear (LoginTooltipView view, bool appIsEligible);

		// @optional -(void)loginTooltipViewWillAppear:(FBSDKLoginTooltipView *)view;
		[EventArgs ("LoginTooltipViewWillAppear")]
		[Export ("loginTooltipViewWillAppear:")]
		void WillAppear (LoginTooltipView view);

		// @optional -(void)loginTooltipViewWillNotAppear:(FBSDKLoginTooltipView *)view;
		[EventArgs ("LoginTooltipViewWillNotAppear")]
		[Export ("loginTooltipViewWillNotAppear:")]
		void WillNotAppear (LoginTooltipView view);
	}

	// @interface FBSDKTooltipView : UIView
	[BaseType (typeof (UIView), Name = "FBSDKTooltipView")]
	interface TooltipView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		// @property (assign, nonatomic) CFTimeInterval displayDuration;
		[Export ("displayDuration")]
		double DisplayDuration { get; set; }

		// @property (assign, nonatomic) FBSDKTooltipColorStyle colorStyle;
		[Export ("colorStyle", ArgumentSemantic.Assign)]
		TooltipColorStyle ColorStyle { get; set; }

		// @property (copy, nonatomic) NSString * message;
		[NullAllowed]
		[Export ("message", ArgumentSemantic.Copy)]
		string Message { get; set; }

		// @property (copy, nonatomic) NSString * tagline;
		[NullAllowed]
		[Export ("tagline", ArgumentSemantic.Copy)]
		string Tagline { get; set; }

		// -(instancetype)initWithTagline:(NSString *)tagline message:(NSString *)message colorStyle:(FBSDKTooltipColorStyle)colorStyle;
		[Export ("initWithTagline:message:colorStyle:")]
		IntPtr Constructor ([NullAllowed] string tagline, [NullAllowed] string message, TooltipColorStyle colorStyle);

		// -(void)presentFromView:(UIView *)anchorView;
		[Export ("presentFromView:")]
		void PresentFromView (UIView anchorView);

		// -(void)presentInView:(UIView *)view withArrowPosition:(CGPoint)arrowPosition direction:(FBSDKTooltipViewArrowDirection)arrowDirection;
		[Export ("presentInView:withArrowPosition:direction:")]
		void PresentInView (UIView view, CGPoint arrowPosition, TooltipViewArrowDirection arrowDirection);

		// -(void)dismiss;
		[Export ("dismiss")]
		void Dismiss ();
	}
}
