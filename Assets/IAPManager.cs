//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Purchasing;
////using UnityEngine.Purchasing.Extension;

//public class IAPManager : MonoBehaviour, IStoreListener
//{
//    private IStoreController controller;

//    void Start()
//    {
//        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
//        // Add products here (if needed programmatically)
//         UnityPurchasing.Initialize(this, builder);
      
//    }

//    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//    {
//        this.controller = controller;

//        if (controller.products.WithID("NO_ADS").hasReceipt)
//        {
//            Debug.Log("✅ Remove Ads already purchased!");
//            // Disable ads
//        }
//        else
//        {
//            Debug.Log("No purchased Remove Ads.");
//        }
//    }

//    public void OnInitializeFailed(InitializationFailureReason error) =>
//        Debug.LogError("IAP Initialization Failed: " + error);

//    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//    {
//        Debug.Log("Product purchased: " + args.purchasedProduct.definition.id);
//        return PurchaseProcessingResult.Complete;
//    }

//    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) =>
//        Debug.LogWarning("Purchase failed: " + failureReason);

    
//}
