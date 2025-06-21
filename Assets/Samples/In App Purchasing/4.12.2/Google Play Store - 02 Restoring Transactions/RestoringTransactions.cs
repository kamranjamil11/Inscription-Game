using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Samples.Purchasing.GooglePlay.RestoringTransactions
{
    [RequireComponent(typeof(UserWarningGooglePlayStore))]
    public class RestoringTransactions : MonoBehaviour, IDetailedStoreListener
    {
        IStoreController m_StoreController;
        IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;

        public string noAdsProductId = "com.wordgame.inscription.no_ads";
        UIHandler ui_Handler;
        //  public Text hasNoAdsText;

        // public Text restoreStatusText;

        void Start()
        {
            ui_Handler = FindObjectOfType<UIHandler>();
            InitializePurchasing();
            UpdateWarningMessage();
        }

        void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.AddProduct(noAdsProductId, ProductType.NonConsumable);
            builder.AddProduct("com.wordgame.inscription.coins_5000", ProductType.Consumable);
            builder.AddProduct("com.wordgame.inscription.coins_2000", ProductType.Consumable);
            builder.AddProduct("com.wordgame.inscription.coins_500", ProductType.Consumable);

            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("In-App Purchasing successfully initialized");

            m_StoreController = controller;
          //  m_GooglePlayStoreExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();

            // UpdateUI();


            // ✅ Check if user already owns the product
            if (m_StoreController.products.WithID(noAdsProductId).hasReceipt)
            {
                Debug.Log("✅ 'Remove Ads' already purchased. Disabling ads...");
                // 👉 Disable ads or unlock features here
                UpdateUI();
            }
            else
            {
                Debug.Log("❌ 'Remove Ads' not purchased.");
            }
           
        }

        public void Restore()
        {
            m_GooglePlayStoreExtensions.RestoreTransactions(OnRestore);

        }

        void OnRestore(bool success, string error)
        {
            var restoreMessage = "";
            if (success)
            {
                // This does not mean anything was restored,
                // merely that the restoration process succeeded.
                restoreMessage = "Restore Successful";
                UpdateUI();
            }
            else
            {
                // Restoration failed.
                restoreMessage = $"Restore Failed with error: {error}";
            }

            Debug.Log(restoreMessage);
           // restoreStatusText.text = restoreMessage;
        }

        public void BuyNoAds()
        {
            m_StoreController.InitiatePurchase(noAdsProductId);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            var product = args.purchasedProduct;
            string productId = args.purchasedProduct.definition.id;
            Debug.Log($"Processing Purchase: {product.definition.id}");

            Popup coins_Popup = GameObject.FindObjectOfType<Popup>();

            switch (productId)
            {
                case "com.wordgame.inscription.coins_5000":
                    Debug.Log("✅ Purchased 5000 Coins");
                    // Add 100 coins to player balance
                    coins_Popup.AddCoins(5000);
                    break;

                case "com.wordgame.inscription.coins_2000":
                    Debug.Log("✅ Purchased 2000 Coins");
                    // Add 50 gems to player balance
                    coins_Popup.AddCoins(2000);
                    break;
                case "com.wordgame.inscription.coins_500":
                    Debug.Log("✅ Purchased 500 Coins");
                    // Add 50 gems to player balance
                    coins_Popup.AddCoins(500);
                    break;

                case "com.wordgame.inscription.no_ads":                   
                    Debug.Log("Remove Ads purchased. Disabling ads");
                   // if (m_StoreController.products.WithID(noAdsProductId).hasReceipt)
                   // {
                       // Debug.Log("✅ 'Remove Ads' already purchased. Disabling ads...");
                        // 👉 Disable ads or unlock features here
                        UpdateUI();
                   // }
                   // else
                   // {
                       // Debug.Log("❌ 'Remove Ads' not purchased.");
                   // }
                    break;
            }



            

           
            return PurchaseProcessingResult.Complete;
        }

        void UpdateUI()
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {            
                ui_Handler.RemoveAdsCompleted();
            }
            //  hasNoAdsText.text = HasNoAds() ? "No ads will be shown" : "Ads will be shown";
        }

        bool HasNoAds()
        {
            var noAdsProduct = m_StoreController.products.WithID(noAdsProductId);
            return noAdsProduct != null && noAdsProduct.hasReceipt;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            OnInitializeFailed(error, null);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

            if (message != null)
            {
                errorMessage += $" More details: {message}";
            }

            Debug.Log(errorMessage);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
           
            Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.Log($"Purchase failed - Product: '{product.definition.id}'," +
                $" Purchase failure reason: {failureDescription.reason}," +
                $" Purchase failure details: {failureDescription.message}");
            Popup coins_Popup = GameObject.FindObjectOfType<Popup>();
            if (coins_Popup != null)
            {
                coins_Popup.loading_Panel.SetActive(false);
            }
            if (product.definition.id == "com.wordgame.inscription.no_ads") 
            {
                ui_Handler.loadingPanel.SetActive(false);   
            }
        }

        void UpdateWarningMessage()
        {
            GetComponent<UserWarningGooglePlayStore>().UpdateWarningText();
        }
    }
}
