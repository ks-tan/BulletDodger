using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

namespace PlayPhone
{
	public static class Billing
	{
		public class PurchaseDetails
		{
			public string Id { get ; set; }
			public string ItemId { get; set; }
			public bool IsDurable { get; set; }
			public bool IsSubscription { get; set; }
			public string Description { get; set; }
			public string Name { get; set; }
			public int Quantity { get; set; }
			public string IconUrl { get; set; }
			public string Receipt { get; set; } // Json string
			public string Price { get; set; }
		}

		public static event Action<PurchaseDetails> OnSuccess;
		public static event Action<string> OnError;
		public static event Action OnCancel;
		public static event Action<PurchaseDetails> OnRestore;

		public static event Action<string> OnSubscriptions;
		public static event Action<string> OnSubscriptionsError;

		public static void Purchase(string itemId)
		{
			var values = new Dictionary<string, object> () {
				{ Consts.HASH_VALUES_PURCHASE_PSGN_ITEM_ID, itemId },
			};
			Plugin.DoAction (Consts.PSGN_PURCHASE, values);
		}

		public static void GetSubscriptions()
		{
			Plugin.GetSubscriptions();
		}
		
		public static void RestorePurchases()
		{
			Plugin.DoAction (Consts.PSGN_RESTORE_PURCHASES);
		}

		internal static void RaiseOnSuccess(PurchaseDetails purchaseDetails)
		{
			if (OnSuccess != null)
			{
				OnSuccess (purchaseDetails);
			}
		}

		internal static void RaiseOnError(string error)
		{
			if (OnError != null) 
			{
				OnError (error);
			}
		}

		internal static void RaiseOnCancel()
		{
			if (OnCancel != null)
			{
				OnCancel ();
			}
		}

		internal static void RaiseOnRestore(PurchaseDetails purchaseDetails)
		{
			if (OnRestore != null)
			{
				OnRestore (purchaseDetails);
			}
		}

		internal static void RaiseSubscriptions(string subscriptions)
		{
			if (OnSubscriptions != null)
			{
				OnSubscriptions(subscriptions);
			}
		}

		internal static void RaiseSubscriptionsError(string error)
		{
			if (OnSubscriptionsError != null)
			{
				OnSubscriptionsError(error);
			}
		}
	}
}