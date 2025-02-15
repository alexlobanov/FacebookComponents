﻿using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;
using Photos;

namespace Facebook.ShareKit {
	// @interface FBSDKAppGroupContent : NSObject <FBSDKCopying, NSSecureCoding>
	[BaseType (typeof (NSObject), Name = "FBSDKAppGroupContent")]
	interface AppGroupContent : CoreKit.ICopying, INSSecureCoding {

		// @property (copy, nonatomic) NSString * groupDescription;
		[Export ("groupDescription", ArgumentSemantic.Copy)]
		string GroupDescription { get; set; }

		// @property (copy, nonatomic) NSString * name;
		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		// @property (assign, nonatomic) FBSDKAppGroupPrivacy privacy;
		[Export ("privacy", ArgumentSemantic.Assign)]
		AppGroupPrivacy Privacy { get; set; }

		// -(BOOL)isEqualToAppGroupContent:(FBSDKAppGroupContent *)content;
		[Export ("isEqualToAppGroupContent:")]
		bool Equals (AppGroupContent content);
	}

	// @interface FBSDKAppInviteContent : NSObject <FBSDKCopying, NSSecureCoding>
	[BaseType (typeof (NSObject), Name = "FBSDKAppInviteContent")]
	interface AppInviteContent : CoreKit.ICopying, SharingValidation, INSSecureCoding {

		// @property (copy, nonatomic) NSURL * appLinkURL;
		[NullAllowed]
		[Export ("appLinkURL", ArgumentSemantic.Copy)]
		NSUrl AppLinkUrl { get; set; }

		// @property (nonatomic, copy) NSURL *appInvitePreviewImageURL;
		[NullAllowed]
		[Export ("appInvitePreviewImageURL", ArgumentSemantic.Copy)]
		NSUrl PreviewImageUrl { get; set; }

		// @property (nonatomic, copy) NSString *promotionCode;
		[NullAllowed]
		[Export ("promotionCode")]
		string PromotionCode { get; set; }

		// @property (nonatomic, copy) NSString *promotionText;
		[NullAllowed]
		[Export ("promotionText")]
		string PromotionText { get; set; }

		// @property FBSDKAppInviteDestination destination;
		[Export ("destination", ArgumentSemantic.Assign)]
		AppInviteDestination Destination { get; set; }

		// -(BOOL)isEqualToAppInviteContent:(FBSDKAppInviteContent *)content;
		[Export ("isEqualToAppInviteContent:")]
		bool Equals (AppInviteContent content);
	}

	// @interface FBSDKCameraEffectArguments : NSObject <FBSDKCopying, NSSecureCoding>
	[BaseType (typeof (NSObject), Name = "FBSDKCameraEffectArguments")]
	interface CameraEffectArguments : CoreKit.ICopying, INSSecureCoding {
		// -(void)setString:(NSString *)string forKey:(NSString *)key;
		[Export ("setString:forKey:")]
		void SetString ([NullAllowed] string aString, string key);

		// -(NSString *)stringForKey:(NSString *)key;
		[return: NullAllowed]
		[Export ("stringForKey:")]
		string GetString (string key);

		// -(void)setArray:(NSArray<NSString *> *)array forKey:(NSString *)key;
		[Export ("setArray:forKey:")]
		void SetArray ([NullAllowed] string [] array, string key);

		// -(NSArray *)arrayForKey:(NSString *)key;
		[return: NullAllowed]
		[Export ("arrayForKey:")]
		string [] GetArray (string key);
	}

	// @interface FBSDKCameraEffectTextures : NSObject <FBSDKCopying, NSSecureCoding>
	[BaseType (typeof (NSObject), Name = "FBSDKCameraEffectTextures")]
	interface CameraEffectTextures : CoreKit.ICopying, INSSecureCoding {
		// -(void)setImage:(UIImage *)image forKey:(NSString *)key;
		[Export ("setImage:forKey:")]
		void SetImage ([NullAllowed] UIImage image, string key);

		// -(UIImage *)imageForKey:(NSString *)key;
		[return: NullAllowed]
		[Export ("imageForKey:")]
		UIImage GetImage (string key);
	}

	// @interface FBSDKGameRequestContent : NSObject <FBSDKCopying, NSSecureCoding>
	[BaseType (typeof (NSObject), Name = "FBSDKGameRequestContent")]
	interface GameRequestContent : CoreKit.ICopying, SharingValidation, INSSecureCoding {

		// @property (assign, nonatomic) FBSDKGameRequestActionType actionType;
		[Export ("actionType", ArgumentSemantic.Assign)]
		GameRequestActionType ActionType { get; set; }

		// -(BOOL)isEqualToGameRequestContent:(FBSDKGameRequestContent *)content;
		[Export ("isEqualToGameRequestContent:")]
		bool Equals (GameRequestContent content);

		// @property (copy, nonatomic) NSString * data;
		[NullAllowed]
		[Export ("data", ArgumentSemantic.Copy)]
		string Data { get; set; }

		// @property (assign, nonatomic) FBSDKGameRequestFilter filters;
		[Export ("filters", ArgumentSemantic.Assign)]
		GameRequestFilter Filters { get; set; }

		// @property (copy, nonatomic) NSString * message;
		[Export ("message", ArgumentSemantic.Copy)]
		string Message { get; set; }

		// @property (copy, nonatomic) NSString * objectID;
		[Export ("objectID", ArgumentSemantic.Copy)]
		string ObjectId { get; set; }

		// @property (nonatomic, copy) NSArray *recipients;
		[Export ("recipients", ArgumentSemantic.Copy)]
		string [] Recipients { get; set; }

		// @property (copy, nonatomic) NSArray * recipientSuggestions;
		[Export ("recipientSuggestions", ArgumentSemantic.Copy)]
		string [] RecipientSuggestions { get; set; }

		// @property (copy, nonatomic) NSString * title;
		[Export ("title", ArgumentSemantic.Copy)]
		string Title { get; set; }
	}

	// @interface FBSDKGameRequestDialog : NSObject
	[BaseType (typeof (NSObject),
		Name = "FBSDKGameRequestDialog",
		Delegates = new [] { "Delegate" },
		Events = new [] { typeof (GameRequestDialogDelegate) })]
	interface GameRequestDialog {
		// +(instancetype _Nonnull)dialogWithContent:(FBSDKGameRequestContent * _Nonnull)content delegate:(id<FBSDKGameRequestDialogDelegate> _Nullable)delegate __attribute__((swift_name("init(content:delegate:)")));
		[Static]
		[Export ("dialogWithContent:delegate:")]
		GameRequestDialog Create (GameRequestContent content, [NullAllowed] IGameRequestDialogDelegate aDelegate);

		// +(instancetype)showWithContent:(FBSDKGameRequestContent *)content delegate:(id<FBSDKGameRequestDialogDelegate>)delegate;
		[Static]
		[Export ("showWithContent:delegate:")]
		GameRequestDialog Show ([NullAllowed] GameRequestContent content, [NullAllowed] IGameRequestDialogDelegate aDelegate);

		// @property (nonatomic, weak) id<FBSDKGameRequestDialogDelegate> delegate;
		[NullAllowed]
		[Export ("delegate", ArgumentSemantic.Weak)]
		IGameRequestDialogDelegate Delegate { get; set; }

		// @property (copy, nonatomic) FBSDKGameRequestContent * content;
		[NullAllowed]
		[Export ("content", ArgumentSemantic.Copy)]
		GameRequestContent Content { get; set; }

		// @property (getter = isFrictionlessRequestsEnabled, assign, nonatomic) BOOL frictionlessRequestsEnabled;
		[Export ("frictionlessRequestsEnabled")]
		bool FrictionlessRequestsEnabled { [Bind ("isFrictionlessRequestsEnabled")] get; set; }

		// -(BOOL)canShow;
		[Export ("canShow")]
		bool CanShow { get; }

		// -(BOOL)show;
		[Export ("show")]
		bool Show ();

		// -(BOOL)validateWithError:(NSError **)errorRef;
		[Export ("validateWithError:")]
		bool Validate (out NSError error);
	}

	interface IGameRequestDialogDelegate { }

	// @protocol FBSDKGameRequestDialogDelegate <NSObject>
	[Model (AutoGeneratedName = true)]
	[Protocol]
	[BaseType (typeof (NSObject), Name = "FBSDKGameRequestDialogDelegate")]
	interface GameRequestDialogDelegate {

		// @required -(void)gameRequestDialog:(FBSDKGameRequestDialog *)gameRequestDialog didCompleteWithResults:(NSDictionary *)results;
		[Abstract]
		[EventArgs ("GameRequestDialogCompleted")]
		[EventName ("Completed")]
		[Export ("gameRequestDialog:didCompleteWithResults:")]
		void DidComplete (GameRequestDialog gameRequestDialog, NSDictionary results);

		// @required -(void)gameRequestDialog:(FBSDKGameRequestDialog *)gameRequestDialog didFailWithError:(NSError *)error;
		[Abstract]
		[EventArgs ("GameRequestDialogFailed")]
		[EventName ("Failed")]
		[Export ("gameRequestDialog:didFailWithError:")]
		void DidFail (GameRequestDialog gameRequestDialog, NSError error);

		// @required -(void)gameRequestDialogDidCancel:(FBSDKGameRequestDialog *)gameRequestDialog;
		[Abstract]
		[EventArgs ("GameRequestDialogCancelled")]
		[EventName ("Cancelled")]
		[Export ("gameRequestDialogDidCancel:")]
		void DidCancel (GameRequestDialog gameRequestDialog);
	}

	// @interface FBSDKHashtag : NSObject <FBSDKCopying, NSSecureCoding>
	[BaseType (typeof (NSObject), Name = "FBSDKHashtag")]
	interface Hashtag : CoreKit.ICopying, INSSecureCoding {
		// + (instancetype)hashtagWithString:(NSString *)hashtagString;
		[Static]
		[Export ("hashtagWithString:")]
		Hashtag Create (string hashtag);

		// @property (nonatomic, readwrite, copy) NSString *stringRepresentation;
		[Export ("stringRepresentation")]
		string StringRepresentation { get; set; }

		// @property (nonatomic, readonly, assign, getter=isValid) BOOL valid;
		[Export ("valid")]
		bool Valid { [Bind ("isValid")] get; set; }

		// - (BOOL)isEqualToHashtag:(FBSDKHashtag *)hashtag;
		[Export ("isEqualToHashtag:")]
		bool Equals (Hashtag hashtag);
	}

	interface ILiking {

	}

	// @protocol FBSDKLiking <NSObject>
	[Model (AutoGeneratedName = true)]
	[Protocol]
	[BaseType (typeof (NSObject), Name = "FBSDKLiking")]
	interface Liking {

		// @required @property (copy, nonatomic) NSString * objectID;
		[Export ("objectID")]
		string GetObjectId ();

		[Export ("setObjectID:")]
		void SetObjectId (string id);

		// @required @property (assign, nonatomic) FBSDKLikeObjectType objectType;
		[Export ("objectType")]
		LikeObjectType GetObjectType ();

		[Export ("setObjectType:")]
		void SetObjectType (LikeObjectType type);
	}

	// @interface FBSDKMessageDialog : NSObject <FBSDKSharingDialog>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "FBSDKMessageDialog")]
	interface MessageDialog : SharingDialog {
		// +(instancetype _Nonnull)dialogWithContent:(id<FBSDKSharingContent> _Nonnull)content delegate:(id<FBSDKSharingDelegate> _Nullable)delegate __attribute__((swift_name("init(content:delegate:)")));
		[Static]
		[Export ("dialogWithContent:delegate:")]
		MessageDialog Create (ISharingContent content, [NullAllowed] ISharingDelegate aDelegate);

		// +(instancetype)showWithContent:(id<FBSDKSharingContent>)content delegate:(id<FBSDKSharingDelegate>)delegate;
		[Static]
		[Export ("showWithContent:delegate:")]
		MessageDialog Show ([NullAllowed] ISharingContent content, [NullAllowed] ISharingDelegate aDelegate);
	}

	// @interface FBSDKSendButton : FBSDKButton <FBSDKSharingButton>
	[BaseType (typeof (CoreKit.Button), Name = "FBSDKSendButton")]
	interface SendButton : SharingButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);
	}

	// @interface FBSDKShareAPI : NSObject <FBSDKSharing>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "FBSDKShareAPI")]
	interface ShareAPI : Sharing {

		// extern NSString *const FBSDKShareErrorDomain;
		[Field ("FBSDKShareErrorDomain", "__Internal")]
		NSString ErrorDomain { get; }

		// +(instancetype _Nonnull)apiWithContent:(id<FBSDKSharingContent> _Nonnull)content delegate:(id<FBSDKSharingDelegate> _Nullable)delegate __attribute__((swift_name("init(content:delegate:)")));
		[Static]
		[Export ("apiWithContent:delegate:")]
		ShareAPI Create (ISharingContent content, [NullAllowed] ISharingDelegate aDelegate);

		// +(instancetype)shareWithContent:(id<FBSDKSharingContent>)content delegate:(id<FBSDKSharingDelegate>)delegate;
		[Static]
		[Export ("shareWithContent:delegate:")]
		ShareAPI Share (ISharingContent content, [NullAllowed] ISharingDelegate aDelegate);

		// @property (nonatomic, copy) NSString *message;
		[NullAllowed]
		[Export ("message", ArgumentSemantic.Copy)]
		string Message { get; set; }

		// @property (nonatomic, copy) NSString *graphNode;
		[Export ("graphNode", ArgumentSemantic.Copy)]
		string GraphNode { get; set; }

		// @property (nonatomic, strong) FBSDKAccessToken *accessToken;
		[NullAllowed]
		[Export ("accessToken", ArgumentSemantic.Strong)]
		CoreKit.AccessToken AccessToken { get; set; }

		// -(BOOL)canShare;
		[Export ("canShare")]
		bool CanShare ();

		// -(BOOL)createOpenGraphObject:(FBSDKShareOpenGraphObject *)openGraphObject;
		[Export ("createOpenGraphObject:")]
		bool CreateOpenGraphObject (ShareOpenGraphObject openGraphObject);

		// -(BOOL)share;
		[Export ("share")]
		bool Share ();
	}

	// @interface FBSDKShareButton : FBSDKButton <FBSDKSharingButton>
	[BaseType (typeof (CoreKit.Button), Name = "FBSDKShareButton")]
	interface ShareButton : SharingButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);
	}

	// @interface FBSDKShareCameraEffectContent : NSObject <FBSDKSharingContent>
	[BaseType (typeof (NSObject), Name = "FBSDKShareCameraEffectContent")]
	interface ShareCameraEffectContent : SharingContent, SharingScheme {
		// @property (copy, nonatomic) NSString * effectID;
		[Export ("effectID")]
		string EffectId { get; set; }

		// @property (copy, nonatomic) FBSDKCameraEffectArguments * effectArguments;
		[Export ("effectArguments", ArgumentSemantic.Copy)]
		CameraEffectArguments EffectArguments { get; set; }

		// @property (copy, nonatomic) FBSDKCameraEffectTextures * effectTextures;
		[Export ("effectTextures", ArgumentSemantic.Copy)]
		CameraEffectTextures EffectTextures { get; set; }

		// -(BOOL)isEqualToShareCameraEffectContent:(FBSDKShareCameraEffectContent *)content;
		[Export ("isEqualToShareCameraEffectContent:")]
		bool Equals (ShareCameraEffectContent content);
	}

	// @interface FBSDKShareDialog : NSObject <FBSDKSharingDialog>
	[BaseType (typeof (NSObject), Name = "FBSDKShareDialog")]
	interface ShareDialog : SharingDialog {
		// +(instancetype _Nonnull)dialogWithViewController:(UIViewController * _Nullable)viewController withContent:(id<FBSDKSharingContent> _Nonnull)content delegate:(id<FBSDKSharingDelegate> _Nullable)delegate __attribute__((swift_name("init(fromViewController:content:delegate:)")));
		[Static]
		[Export ("dialogWithViewController:withContent:delegate:")]
		ShareDialog Create ([NullAllowed] UIViewController viewController, ISharingContent content, [NullAllowed] ISharingDelegate aDelegate);

		// +(instancetype)showFromViewController:(UIViewController *)viewController withContent:(id<FBSDKSharingContent>)content delegate:(id<FBSDKSharingDelegate>)delegate;
		[Static]
		[Export ("showFromViewController:withContent:delegate:")]
		ShareDialog Show (UIViewController viewController, ISharingContent content, [NullAllowed] ISharingDelegate aDelegate);

		// @property (nonatomic, weak) UIViewController * fromViewController;
		[NullAllowed]
		[Export ("fromViewController", ArgumentSemantic.Weak)]
		UIViewController FromViewController { get; set; }

		// @property (assign, nonatomic) FBSDKShareDialogMode mode;
		[Export ("mode", ArgumentSemantic.Assign)]
		ShareDialogMode Mode { get; set; }
	}

	// @interface FBSDKShareLinkContent : NSObject <FBSDKSharingContent>
	[BaseType (typeof (NSObject), Name = "FBSDKShareLinkContent")]
	interface ShareLinkContent : SharingContent {
		// @property (copy, nonatomic) NSString * _Nullable quote;
		[NullAllowed]
		[Export ("quote")]
		string Quote { get; set; }

		// -(BOOL)isEqualToShareLinkContent:(FBSDKShareLinkContent *)content;
		[Export ("isEqualToShareLinkContent:")]
		bool Equals (ShareLinkContent content);
	}

	interface IShareMedia { }

	// @protocol FBSDKShareMedia <NSObject>
	[Protocol (Name = "FBSDKShareMedia")]
	interface ShareMedia { }

	// @interface FBSDKShareMediaContent : NSObject <FBSDKSharingContent>
	[BaseType (typeof (NSObject), Name = "FBSDKShareMediaContent")]
	interface ShareMediaContent {
		// @property (nonatomic, copy) NSArray *media;
		[Export ("media", ArgumentSemantic.Copy)]
		IShareMedia [] Media { get; set; }

		// - (BOOL)isEqualToShareMediaContent:(FBSDKShareMediaContent *)content;
		[Export ("isEqualToShareMediaContent:")]
		bool Equals (ShareMediaContent content);
	}

	interface IShareMessengerActionButton { }

	// @protocol FBSDKShareMessengerActionButton <FBSDKCopying, NSSecureCoding>
	[Protocol (Name = "FBSDKShareMessengerActionButton")]
	interface ShareMessengerActionButton : CoreKit.ICopying, INSSecureCoding {
		// @required @property (copy, nonatomic) NSString * title;
		[Abstract]
		[Export ("title")]
		string Title { get; set; }
	}

	// @interface FBSDKShareMessengerGenericTemplateContent : NSObject <FBSDKSharingContent>
	[BaseType (typeof (NSObject), Name = "FBSDKShareMessengerGenericTemplateContent")]
	interface ShareMessengerGenericTemplateContent : SharingContent {
		// @property (assign, nonatomic) BOOL isSharable;
		[Export ("isSharable")]
		bool IsSharable { get; set; }

		// @property (assign, nonatomic) FBSDKShareMessengerGenericTemplateImageAspectRatio imageAspectRatio;
		[Export ("imageAspectRatio", ArgumentSemantic.Assign)]
		ShareMessengerGenericTemplateImageAspectRatio ImageAspectRatio { get; set; }

		// @property (copy, nonatomic) FBSDKShareMessengerGenericTemplateElement * element;
		[Export ("element", ArgumentSemantic.Copy)]
		ShareMessengerGenericTemplateElement Element { get; set; }
	}

	// @interface FBSDKShareMessengerGenericTemplateElement : NSObject <FBSDKCopying, NSSecureCoding>
	[BaseType (typeof (NSObject), Name = "FBSDKShareMessengerGenericTemplateElement")]
	interface ShareMessengerGenericTemplateElement : CoreKit.ICopying, INSSecureCoding {
		// @property (copy, nonatomic) NSString * title;
		[Export ("title")]
		string Title { get; set; }

		// @property (copy, nonatomic) NSString * subtitle;
		[NullAllowed]
		[Export ("subtitle")]
		string Subtitle { get; set; }

		// @property (copy, nonatomic) NSURL * imageURL;
		[NullAllowed]
		[Export ("imageURL", ArgumentSemantic.Copy)]
		NSUrl ImageUrl { get; set; }

		// @property (copy, nonatomic) id<FBSDKShareMessengerActionButton> defaultAction;
		[NullAllowed]
		[Export ("defaultAction", ArgumentSemantic.Copy)]
		IShareMessengerActionButton DefaultAction { get; set; }

		// @property (copy, nonatomic) id<FBSDKShareMessengerActionButton> button;
		[NullAllowed]
		[Export ("button", ArgumentSemantic.Copy)]
		IShareMessengerActionButton Button { get; set; }
	}

	// @interface FBSDKShareMessengerMediaTemplateContent : NSObject <FBSDKSharingContent>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "FBSDKShareMessengerMediaTemplateContent")]
	interface ShareMessengerMediaTemplateContent : SharingContent {
		// @property (assign, nonatomic) FBSDKShareMessengerMediaTemplateMediaType mediaType;
		[Export ("mediaType", ArgumentSemantic.Assign)]
		ShareMessengerMediaTemplateMediaType MediaType { get; set; }

		// @property (readonly, copy, nonatomic) NSString * attachmentID;
		[NullAllowed]
		[Export ("attachmentID")]
		string AttachmentId { get; }

		// @property (readonly, copy, nonatomic) NSURL * mediaURL;
		[NullAllowed]
		[Export ("mediaURL", ArgumentSemantic.Copy)]
		NSUrl MediaUrl { get; }

		// @property (copy, nonatomic) id<FBSDKShareMessengerActionButton> button;
		[NullAllowed]
		[Export ("button", ArgumentSemantic.Copy)]
		IShareMessengerActionButton Button { get; set; }

		// -(instancetype)initWithAttachmentID:(NSString *)attachmentID;
		[Export ("initWithAttachmentID:")]
		IntPtr Constructor (string attachmentId);

		// -(instancetype)initWithMediaURL:(NSURL *)mediaURL;
		[Export ("initWithMediaURL:")]
		IntPtr Constructor (NSUrl mediaUrl);
	}

	// @interface FBSDKShareMessengerOpenGraphMusicTemplateContent : NSObject <FBSDKSharingContent>
	[BaseType (typeof (NSObject), Name = "FBSDKShareMessengerOpenGraphMusicTemplateContent")]
	interface ShareMessengerOpenGraphMusicTemplateContent : SharingContent {
		// @property (copy, nonatomic) NSURL * url;
		[Export ("url", ArgumentSemantic.Copy)]
		NSUrl Url { get; set; }

		// @property (copy, nonatomic) id<FBSDKShareMessengerActionButton> button;
		[NullAllowed]
		[Export ("button", ArgumentSemantic.Copy)]
		IShareMessengerActionButton Button { get; set; }
	}

	// @interface FBSDKShareMessengerURLActionButton : NSObject <FBSDKShareMessengerActionButton>
	[BaseType (typeof (NSObject), Name = "FBSDKShareMessengerURLActionButton")]
	interface ShareMessengerUrlActionButton : ShareMessengerActionButton {
		// @property (copy, nonatomic) NSURL * url;
		[Export ("url", ArgumentSemantic.Copy)]
		NSUrl Url { get; set; }

		// @property (assign, nonatomic) FBSDKShareMessengerURLActionButtonWebviewHeightRatio webviewHeightRatio;
		[Export ("webviewHeightRatio", ArgumentSemantic.Assign)]
		ShareMessengerURLActionButtonWebviewHeightRatio WebviewHeightRatio { get; set; }

		// @property (assign, nonatomic) BOOL isMessengerExtensionURL;
		[Export ("isMessengerExtensionURL")]
		bool IsMessengerExtensionUrl { get; set; }

		// @property (copy, nonatomic) NSURL * fallbackURL;
		[NullAllowed]
		[Export ("fallbackURL", ArgumentSemantic.Copy)]
		NSUrl FallbackUrl { get; set; }

		// @property (assign, nonatomic) BOOL shouldHideWebviewShareButton;
		[Export ("shouldHideWebviewShareButton")]
		bool ShouldHideWebviewShareButton { get; set; }
	}

	// @interface FBSDKShareOpenGraphAction : FBSDKShareOpenGraphValueContainer <FBSDKCopying, NSSecureCoding>
	[DisableDefaultCtor]
	[BaseType (typeof (ShareOpenGraphValueContainer), Name = "FBSDKShareOpenGraphAction")]
	interface ShareOpenGraphAction : CoreKit.ICopying, INSSecureCoding {
		// -(instancetype _Nonnull)initWithActionType:(NSString * _Nonnull)actionType __attribute__((swift_name("init(type:)")));
		[Export ("initWithActionType:")]
		IntPtr Constructor (string actionType);

		// +(instancetype)actionWithType:(NSString *)actionType object:(FBSDKShareOpenGraphObject *)object key:(NSString *)key;
		[Static]
		[Export ("actionWithType:object:key:")]
		ShareOpenGraphAction Create (string actionType, ShareOpenGraphObject graphObject, string key);

		[Obsolete ("Use the Create method instead. This will be removed in future versions.")]
		[Static]
		[Wrap ("Create (actionType, graphObject, key)")]
		ShareOpenGraphAction Action (string actionType, ShareOpenGraphObject graphObject, string key);

		// +(instancetype)actionWithType:(NSString *)actionType objectID:(NSString *)objectID key:(NSString *)key;
		[Static]
		[Export ("actionWithType:objectID:key:")]
		ShareOpenGraphAction Create (string actionType, string objectId, string key);

		[Obsolete ("Use the Create method instead. This will be removed in future versions.")]
		[Static]
		[Wrap ("Create (actionType, objectId, key)")]
		ShareOpenGraphAction Action (string actionType, string objectId, string key);

		// +(instancetype)actionWithType:(NSString *)actionType objectURL:(NSURL *)objectURL key:(NSString *)key;
		[Static]
		[Export ("actionWithType:objectURL:key:")]
		ShareOpenGraphAction Create (string actionType, NSUrl objectUrl, string key);

		[Obsolete ("Use the Create method instead. This will be removed in future versions.")]
		[Static]
		[Wrap ("Create (actionType, objectUrl, key)")]
		ShareOpenGraphAction Action (string actionType, NSUrl objectUrl, string key);

		// @property (copy, nonatomic) NSString * actionType;
		[Export ("actionType", ArgumentSemantic.Copy)]
		string ActionType { get; set; }

		// -(BOOL)isEqualToShareOpenGraphAction:(FBSDKShareOpenGraphAction *)action;
		[Export ("isEqualToShareOpenGraphAction:")]
		bool Equals (ShareOpenGraphAction action);
	}

	// @interface FBSDKShareOpenGraphContent : NSObject <FBSDKSharingContent>
	[BaseType (typeof (NSObject), Name = "FBSDKShareOpenGraphContent")]
	interface ShareOpenGraphContent : SharingContent {

		// @property (copy, nonatomic) FBSDKShareOpenGraphAction * action;
		[NullAllowed]
		[Export ("action", ArgumentSemantic.Copy)]
		ShareOpenGraphAction Action { get; set; }

		// @property (copy, nonatomic) NSString * previewPropertyName;
		[Export ("previewPropertyName", ArgumentSemantic.Copy)]
		string PreviewPropertyName { get; set; }

		// -(BOOL)isEqualToShareOpenGraphContent:(FBSDKShareOpenGraphContent *)content;
		[Export ("isEqualToShareOpenGraphContent:")]
		bool Equals (ShareOpenGraphContent content);
	}

	// @interface FBSDKShareOpenGraphObject : FBSDKShareOpenGraphValueContainer <FBSDKCopying, NSSecureCoding>
	[DisableDefaultCtor]
	[BaseType (typeof (ShareOpenGraphValueContainer), Name = "FBSDKShareOpenGraphObject")]
	interface ShareOpenGraphObject : CoreKit.ICopying, INSSecureCoding {

		// +(instancetype)objectWithProperties:(NSDictionary *)properties;
		[Static]
		[Export ("objectWithProperties:")]
		ShareOpenGraphObject Create ([NullAllowed] NSDictionary properties);

		// -(BOOL)isEqualToShareOpenGraphObject:(FBSDKShareOpenGraphObject *)object;
		[Export ("isEqualToShareOpenGraphObject:")]
		bool Equals (ShareOpenGraphObject aObject);
	}

	// typedef void (^FBSDKEnumerationBlock)(NSString * _Nonnull, id _Nonnull, BOOL * _Nonnull);
	delegate void EnumerationBlockHandler (string key, NSObject id, ref bool stop);

	interface IShareOpenGraphValueContaining { }

	// @protocol FBSDKShareOpenGraphValueContaining <NSObject, NSSecureCoding>
	[Protocol (Name = "FBSDKShareOpenGraphValueContaining")]
	interface ShareOpenGraphValueContaining : INSSecureCoding {
		// @required @property (readonly, nonatomic, strong) NSDictionary<NSString *,id> * _Nonnull allProperties;
		[Abstract]
		[Export ("allProperties", ArgumentSemantic.Strong)]
		NSDictionary AllProperties ();

		// @required @property (readonly, nonatomic, strong) NSEnumerator * _Nonnull keyEnumerator __attribute__((availability(swift, unavailable)));
		[Abstract]
		[Export ("keyEnumerator", ArgumentSemantic.Strong)]
		NSEnumerator KeyEnumerator ();

		// @required @property (readonly, nonatomic, strong) NSEnumerator * _Nonnull objectEnumerator __attribute__((availability(swift, unavailable)));
		[Abstract]
		[Export ("objectEnumerator", ArgumentSemantic.Strong)]
		NSEnumerator ObjectEnumerator ();

		// @required -(NSArray *)arrayForKey:(NSString *)key;
		[Abstract]
		[return: NullAllowed]
		[Export ("arrayForKey:")]
		NSObject [] GetArray (string key);

		// @required -(void)enumerateKeysAndObjectsUsingBlock:(void (^)(NSString *, id, BOOL *))block;
		[Abstract]
		[Export ("enumerateKeysAndObjectsUsingBlock:")]
		void EnumerateKeysAndObjects (EnumerationBlockHandler handle);

		// @required -(NSNumber * _Nullable)numberForKey:(NSString * _Nonnull)key;
		[Abstract]
		[return: NullAllowed]
		[Export ("numberForKey:")]
		NSNumber GetNumber (string key);

		// @required -(NSString * _Nullable)stringForKey:(NSString * _Nonnull)key;
		[Abstract]
		[return: NullAllowed]
		[Export ("stringForKey:")]
		string GetString (string key);

		// @required -(NSURL * _Nullable)URLForKey:(NSString * _Nonnull)key;
		[Abstract]
		[return: NullAllowed]
		[Export ("URLForKey:")]
		NSUrl GetUrl (string key);

		// @required -(FBSDKShareOpenGraphObject *)objectForKey:(NSString *)key;
		[Abstract]
		[return: NullAllowed]
		[Export ("objectForKey:")]
		ShareOpenGraphObject GetObject (string key);

		// @required -(id)objectForKeyedSubscript:(NSString *)key;
		[Abstract]
		[return: NullAllowed]
		[Export ("objectForKeyedSubscript:")]
		NSObject GetObjectForKeyedSubscript (string key);

		// @required -(void)parseProperties:(NSDictionary *)properties;
		[Abstract]
		[Export ("parseProperties:")]
		void ParseProperties (NSDictionary properties);

		// @required -(FBSDKSharePhoto *)photoForKey:(NSString *)key;
		[Abstract]
		[return: NullAllowed]
		[Export ("photoForKey:")]
		SharePhoto PhotoForKey (string key);

		// @required -(void)removeObjectForKey:(NSString *)key;
		[Abstract]
		[Export ("removeObjectForKey:")]
		void RemoveObjectForKey (string key);

		// @required -(void)setArray:(NSArray *)array forKey:(NSString *)key;
		[Abstract]
		[Export ("setArray:forKey:")]
		void SetArray ([NullAllowed] NSObject [] array, string key);

		// @required -(void)setNumber:(NSNumber *)number forKey:(NSString *)key;
		[Abstract]
		[Export ("setNumber:forKey:")]
		void SetNumber ([NullAllowed] NSNumber number, string key);

		// @required -(void)setObject:(FBSDKShareOpenGraphObject *)object forKey:(NSString *)key;
		[Abstract]
		[Export ("setObject:forKey:")]
		void SetObject ([NullAllowed] ShareOpenGraphObject graphObject, string key);

		// @required -(void)setPhoto:(FBSDKSharePhoto *)photo forKey:(NSString *)key;
		[Abstract]
		[Export ("setPhoto:forKey:")]
		void SetPhoto ([NullAllowed] SharePhoto photo, string key);

		// @required -(void)setString:(NSString *)string forKey:(NSString *)key;
		[Abstract]
		[Export ("setString:forKey:")]
		void SetString ([NullAllowed] string aString, string key);

		// @required -(void)setURL:(NSURL *)URL forKey:(NSString *)key;
		[Abstract]
		[Export ("setURL:forKey:")]
		void SetUrl ([NullAllowed] NSUrl Url, string key);
	}

	// @interface FBSDKShareOpenGraphValueContainer : NSObject <FBSDKShareOpenGraphValueContaining>
	[BaseType (typeof (NSObject), Name = "FBSDKShareOpenGraphValueContainer")]
	interface ShareOpenGraphValueContainer : ShareOpenGraphValueContaining {

	}

	// @interface FBSDKSharePhoto : NSObject <NSSecureCoding, FBSDKCopying, FBSDKShareMedia, FBSDKSharingValidation>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "FBSDKSharePhoto")]
	interface SharePhoto : INSSecureCoding, CoreKit.ICopying, ShareMedia, SharingValidation {

		// +(instancetype)photoWithImage:(UIImage *)image userGenerated:(BOOL)userGenerated;
		[Static]
		[Export ("photoWithImage:userGenerated:")]
		SharePhoto From ([NullAllowed] UIImage image, bool userGenerated);

		// +(instancetype)photoWithImageURL:(NSURL *)imageURL userGenerated:(BOOL)userGenerated;
		[Static]
		[Export ("photoWithImageURL:userGenerated:")]
		SharePhoto From ([NullAllowed] NSUrl imageURL, bool userGenerated);

		// +(instancetype)photoWithPhotoAsset:(PHAsset *)photoAsset userGenerated:(BOOL)userGenerated;
		[Static]
		[Export ("photoWithPhotoAsset:userGenerated:")]
		SharePhoto From (PHAsset photoAsset, bool userGenerated);

		// @property (nonatomic, strong) UIImage * image;
		[NullAllowed]
		[Export ("image", ArgumentSemantic.Strong)]
		UIImage Image { get; set; }

		// @property (copy, nonatomic) NSURL * imageURL;
		[NullAllowed]
		[Export ("imageURL", ArgumentSemantic.Copy)]
		NSUrl ImageUrl { get; set; }

		// @property (copy, nonatomic) PHAsset * photoAsset;
		[Export ("photoAsset", ArgumentSemantic.Copy)]
		PHAsset PhotoAsset { get; set; }

		// @property (getter = isUserGenerated, assign, nonatomic) BOOL userGenerated;
		[Export ("userGenerated")]
		bool UserGenerated { [Bind ("isUserGenerated")] get; set; }

		// @property (nonatomic, copy) NSString *caption;
		[NullAllowed]
		[Export ("caption", ArgumentSemantic.Copy)]
		string Caption { get; set; }

		// -(BOOL)isEqualToSharePhoto:(FBSDKSharePhoto *)photo;
		[Export ("isEqualToSharePhoto:")]
		bool Equals (SharePhoto photo);
	}

	// @interface FBSDKSharePhotoContent : NSObject <FBSDKSharingContent>
	[BaseType (typeof (NSObject), Name = "FBSDKSharePhotoContent")]
	interface SharePhotoContent : SharingContent {

		// @property (copy, nonatomic) NSArray * photos;
		[NullAllowed]
		[Export ("photos", ArgumentSemantic.Copy)]
		SharePhoto [] Photos { get; set; }

		// -(BOOL)isEqualToSharePhotoContent:(FBSDKSharePhotoContent *)content;
		[Export ("isEqualToSharePhotoContent:")]
		bool Equals (SharePhotoContent content);
	}

	// @interface FBSDKShareVideo : NSObject <NSSecureCoding, FBSDKCopying, FBSDKShareMedia, FBSDKSharingValidation>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "FBSDKShareVideo")]
	interface ShareVideo : INSSecureCoding, CoreKit.ICopying, ShareMedia, SharingValidation {
		// +(instancetype)videoWithData:(NSData *)data;
		[Static]
		[Export ("videoWithData:")]
		ShareVideo From (NSData data);

		// +(instancetype)videoWithData:(NSData *)data previewPhoto:(FBSDKSharePhoto *)previewPhoto;
		[Static]
		[Export ("videoWithData:previewPhoto:")]
		ShareVideo From (NSData data, SharePhoto previewPhoto);

		// + (instancetype)videoWithVideoAsset:(PHAsset *)videoAsset;
		[Static]
		[Export ("videoWithVideoAsset:")]
		ShareVideo From ([NullAllowed] PHAsset videoAsset);

		// + (instancetype)videoWithVideoAsset:(PHAsset *)videoAsset previewPhoto:(FBSDKSharePhoto *)previewPhoto;
		[Static]
		[Export ("videoWithVideoAsset:previewPhoto:")]
		ShareVideo From ([NullAllowed] PHAsset videoAsset, [NullAllowed] SharePhoto previewPhoto);

		// +(instancetype)videoWithVideoURL:(NSURL *)videoURL;
		[Static]
		[Export ("videoWithVideoURL:")]
		ShareVideo From ([NullAllowed] NSUrl videoURL);

		// + (instancetype)videoWithVideoURL:(NSURL *)videoURL previewPhoto:(FBSDKSharePhoto *)previewPhoto;
		[Static]
		[Export ("videoWithVideoURL:previewPhoto:")]
		ShareVideo From ([NullAllowed] NSUrl videoURL, [NullAllowed] SharePhoto previewPhoto);

		// @property (nonatomic, strong) NSData * data;
		[NullAllowed]
		[Export ("data", ArgumentSemantic.Strong)]
		NSData Data { get; set; }

		// @property (nonatomic, copy) PHAsset *videoAsset;
		[NullAllowed]
		[Export ("videoAsset", ArgumentSemantic.Copy)]
		PHAsset VideoAsset { get; set; }

		// @property (copy, nonatomic) NSURL * videoURL;
		[NullAllowed]
		[Export ("videoURL", ArgumentSemantic.Copy)]
		NSUrl VideoUrl { get; set; }

		// @property (nonatomic, copy) FBSDKSharePhoto *previewPhoto;
		[NullAllowed]
		[Export ("previewPhoto", ArgumentSemantic.Copy)]
		SharePhoto PreviewPhoto { get; set; }

		// -(BOOL)isEqualToShareVideo:(FBSDKShareVideo *)video;
		[Export ("isEqualToShareVideo:")]
		bool Equals (ShareVideo video);
	}

	// @interface FBSDKShareVideo (PHAsset)
	[Category]
	[BaseType (typeof (PHAsset))]
	interface PHAsset_FBSDKShareVideo {
		// @property (readonly, copy, nonatomic) NSURL * videoURL;
		[Export ("videoURL")]
		NSUrl GetVideoUrl ();
	}

	// @interface FBSDKShareVideoContent : NSObject <FBSDKSharingContent>
	[BaseType (typeof (NSObject), Name = "FBSDKShareVideoContent")]
	interface ShareVideoContent : SharingContent {
		// @property (copy, nonatomic) FBSDKShareVideo * video;
		[NullAllowed]
		[Export ("video", ArgumentSemantic.Copy)]
		ShareVideo Video { get; set; }

		// -(BOOL)isEqualToShareVideoContent:(FBSDKShareVideoContent *)content;
		[Export ("isEqualToShareVideoContent:")]
		bool Equals (ShareVideoContent content);
	}

	interface ISharing {

	}

	// @protocol FBSDKSharing <NSObject>
	[Protocol (Name = "FBSDKSharing")]
	interface Sharing {

		// @required @property (nonatomic, weak) id<FBSDKSharingDelegate> delegate;
		[Export ("delegate")]
		ISharingDelegate GetDelegate ();

		[Export ("setDelegate:")]
		void SetDelegate ([NullAllowed] ISharingDelegate aDelegate);

		// @required @property (copy, nonatomic) id<FBSDKSharingContent> shareContent;
		[Export ("shareContent")]
		ISharingContent GetShareContent ();

		[Export ("setShareContent:")]
		void SetShareContent ([NullAllowed] ISharingContent shareContent);

		// @required @property (assign, nonatomic) BOOL shouldFailOnDataError;
		[Export ("shouldFailOnDataError")]
		bool GetShouldFailOnDataError ();

		[Export ("setShouldFailOnDataError:")]
		void SetShouldFailOnDataError (bool shouldFail);

		// @required -(BOOL)validateWithError:(NSError **)errorRef;
		[Export ("validateWithError:")]
		bool Validate (out NSError errorRef);
	}

	interface ISharingDialog {

	}

	// @protocol FBSDKSharingDialog <FBSDKSharing>
	[Protocol (Name = "FBSDKSharingDialog")]
	interface SharingDialog : Sharing {

		// @required -(BOOL)canShow;
		[Abstract]
		[Export ("canShow")]
		bool CanShow ();

		// @required -(BOOL)show;
		[Abstract]
		[Export ("show")]
		bool Show ();
	}

	interface ISharingDelegate {

	}

	// @protocol FBSDKSharingDelegate <NSObject>
	[Model (AutoGeneratedName = true)]
	[Protocol]
	[BaseType (typeof (NSObject), Name = "FBSDKSharingDelegate")]
	interface SharingDelegate {

		// @required -(void)sharer:(id<FBSDKSharing>)sharer didCompleteWithResults:(NSDictionary *)results;
		[Abstract]
		[Export ("sharer:didCompleteWithResults:")]
		void DidComplete (ISharing sharer, NSDictionary results);

		// @required -(void)sharer:(id<FBSDKSharing>)sharer didFailWithError:(NSError *)error;
		[Abstract]
		[Export ("sharer:didFailWithError:")]
		void DidFail (ISharing sharer, NSError error);

		// @required -(void)sharerDidCancel:(id<FBSDKSharing>)sharer;
		[Abstract]
		[Export ("sharerDidCancel:")]
		void DidCancel (ISharing sharer);
	}

	interface ISharingButton {

	}

	// @protocol FBSDKSharingButton <NSObject>
	[Protocol (Name = "FBSDKSharingButton")]
	interface SharingButton {

		// @required @property (copy, nonatomic) id<FBSDKSharingContent> shareContent;
		[Abstract]
		[Export ("shareContent")]
		ISharingContent GetShareContent ();

		[Abstract]
		[Export ("setShareContent:")]
		void SetShareContent ([NullAllowed] ISharingContent shareContent);
	}

	interface ISharingContent {

	}

	// @protocol FBSDKSharingContent <FBSDKCopying, NSSecureCoding>
	[Protocol (Name = "FBSDKSharingContent")]
	interface SharingContent : CoreKit.ICopying, SharingValidation, INSSecureCoding {

		// @required @property (copy, nonatomic) NSURL * contentURL;
		[Abstract]
		[Export ("contentURL")]
		NSUrl GetContentUrl ();

		[Abstract]
		[Export ("setContentURL:")]
		void SetContentUrl ([NullAllowed] NSUrl url);

		// @property (nonatomic, copy) FBSDKHashtag *hashtag;
		[Abstract]
		[Export ("hashtag", ArgumentSemantic.Copy)]
		Hashtag Hashtag { get; set; }

		// @required @property (copy, nonatomic) NSArray * peopleIDs;
		[Abstract]
		[Export ("peopleIDs")]
		string [] GetPeopleIds ();

		[Abstract]
		[Export ("setPeopleIDs:")]
		void SetPeopleIds ([NullAllowed] string [] peolpleId);

		// @required @property (copy, nonatomic) NSString * placeID;
		[Abstract]
		[Export ("placeID")]
		string GetPlaceId ();

		[Abstract]
		[Export ("setPlaceID:")]
		void SetPlaceId (string placeId);

		// @required @property (copy, nonatomic) NSString * ref;
		[Abstract]
		[Export ("ref")]
		string GetRef ();

		[Abstract]
		[Export ("setRef:")]
		void SetRef (string aRef);

		// @required @property (copy, nonatomic) NSString * pageID;
		[Abstract]
		[Export ("pageID")]
		string PageId { get; set; }

		// @required @property (readonly, copy, nonatomic) NSString * shareUUID;
		[Abstract]
		[Export ("shareUUID")]
		string ShareUuid { get; }

		// @required -(NSDictionary<NSString *,id> *)addParameters:(NSDictionary<NSString *,id> *)existingParameters bridgeOptions:(FBSDKShareBridgeOptions)bridgeOptions;
		[Abstract]
		[Export ("addParameters:bridgeOptions:")]
		NSDictionary<NSString, NSObject> AddParameters (NSDictionary<NSString, NSObject> existingParameters, ShareBridgeOptions bridgeOptions);
	}

	interface ISharingScheme { }

	// @protocol FBSDKSharingScheme
	[Protocol (Name = "FBSDKSharingScheme")]
	interface SharingScheme {
		// @required -(NSString * _Nullable)schemeForMode:(FBSDKShareDialogMode)mode;
		[Abstract]
		[return: NullAllowed]
		[Export ("schemeForMode:")]
		string GetScheme (ShareDialogMode mode);
	}

	interface ISharingValidation { }

	// @protocol FBSDKSharingValidation
	[Protocol (Name = "FBSDKSharingValidation")]
	interface SharingValidation {
		// @required -(BOOL)validateWithOptions:(FBSDKShareBridgeOptions)bridgeOptions error:(NSError **)errorRef;
		[Abstract]
		[Export ("validateWithOptions:error:")]
		bool Validate (ShareBridgeOptions bridgeOptions, out NSError errorRef);
	}
}
