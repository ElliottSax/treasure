using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace TreasureExcavator
{
    /// <summary>
    /// Manages in-game shop, IAP purchases, and item inventory.
    /// Supports multiple currencies (gold, gems) and various item types.
    /// </summary>
    public class ShopManager : Singleton<ShopManager>
    {
        [Header("Shop Inventory")]
        [SerializeField] private List<ShopItem> shopItems = new List<ShopItem>();

        [Header("Events")]
        public UnityEvent<ShopItem> onItemPurchased = new UnityEvent<ShopItem>();
        public UnityEvent<ShopItem, string> onPurchaseFailed = new UnityEvent<ShopItem, string>();
        public UnityEvent onShopInventoryUpdated = new UnityEvent();

        private Dictionary<string, ShopItem> itemDatabase;

        protected override void Awake()
        {
            base.Awake();
            InitializeShop();
        }

        /// <summary>
        /// Initializes shop inventory and database
        /// </summary>
        private void InitializeShop()
        {
            itemDatabase = new Dictionary<string, ShopItem>();

            // Create default shop items if none assigned
            if (shopItems.Count == 0)
            {
                CreateDefaultShopItems();
            }

            // Populate database
            foreach (ShopItem item in shopItems)
            {
                if (!itemDatabase.ContainsKey(item.itemId))
                {
                    itemDatabase[item.itemId] = item;
                }
            }

            Debug.Log($"[ShopManager] Initialized shop with {itemDatabase.Count} items");
        }

        /// <summary>
        /// Creates default shop inventory
        /// </summary>
        private void CreateDefaultShopItems()
        {
            shopItems = new List<ShopItem>
            {
                // Vehicles
                new ShopItem("vehicle_dual_scoop", "Dual-Scoop Loader", "Increased capacity + Auto-Collect ability",
                    ShopItemType.Vehicle, CurrencyType.Gold, 1000, "dual_scoop_icon"),

                new ShopItem("vehicle_nitro_hauler", "Nitro Hauler", "Speed boost ability",
                    ShopItemType.Vehicle, CurrencyType.Gold, 2500, "nitro_hauler_icon"),

                new ShopItem("vehicle_mega_vault", "Mega Vault Truck", "Maximum capacity + Gate Magnet",
                    ShopItemType.Vehicle, CurrencyType.Gold, 5000, "mega_vault_icon"),

                // Power-Up Bundles (Consumables)
                new ShopItem("powerup_speed_x3", "Speed Boost x3", "3 Speed Boost power-ups",
                    ShopItemType.PowerUpBundle, CurrencyType.Gold, 100, "speed_boost_icon", 3),

                new ShopItem("powerup_magnet_x3", "Magnet x3", "3 Treasure Magnet power-ups",
                    ShopItemType.PowerUpBundle, CurrencyType.Gold, 150, "magnet_icon", 3),

                new ShopItem("powerup_double_x3", "Double Points x3", "3 Double Points power-ups",
                    ShopItemType.PowerUpBundle, CurrencyType.Gold, 200, "double_points_icon", 3),

                new ShopItem("powerup_mega_bundle", "Mega Power-Up Bundle", "5 of each power-up",
                    ShopItemType.PowerUpBundle, CurrencyType.Gold, 800, "mega_bundle_icon", 15),

                // Gold Packs (IAP)
                new ShopItem("gold_small", "Small Gold Pack", "500 Gold",
                    ShopItemType.Currency, CurrencyType.RealMoney, 0.99f, "gold_small_icon", 500),

                new ShopItem("gold_medium", "Medium Gold Pack", "1,200 Gold (20% bonus!)",
                    ShopItemType.Currency, CurrencyType.RealMoney, 1.99f, "gold_medium_icon", 1200),

                new ShopItem("gold_large", "Large Gold Pack", "3,000 Gold (50% bonus!)",
                    ShopItemType.Currency, CurrencyType.RealMoney, 4.99f, "gold_large_icon", 3000),

                new ShopItem("gold_mega", "Mega Gold Pack", "10,000 Gold (100% bonus!)",
                    ShopItemType.Currency, CurrencyType.RealMoney, 9.99f, "gold_mega_icon", 10000),

                // Gems (Premium Currency)
                new ShopItem("gems_small", "Small Gem Pack", "100 Gems",
                    ShopItemType.Currency, CurrencyType.RealMoney, 0.99f, "gems_small_icon", 100),

                new ShopItem("gems_medium", "Medium Gem Pack", "250 Gems (25% bonus!)",
                    ShopItemType.Currency, CurrencyType.RealMoney, 1.99f, "gems_medium_icon", 250),

                new ShopItem("gems_large", "Large Gem Pack", "750 Gems (50% bonus!)",
                    ShopItemType.Currency, CurrencyType.RealMoney, 4.99f, "gems_large_icon", 750),

                // Special Offers
                new ShopItem("starter_pack", "Starter Pack", "1,000 Gold + Dual-Scoop Loader",
                    ShopItemType.Bundle, CurrencyType.RealMoney, 2.99f, "starter_pack_icon"),

                new ShopItem("remove_ads", "Remove Ads", "Remove all advertisements permanently",
                    ShopItemType.Permanent, CurrencyType.RealMoney, 2.99f, "no_ads_icon"),

                new ShopItem("vip_pass", "VIP Pass", "Double daily rewards + 20% more gold forever",
                    ShopItemType.Permanent, CurrencyType.RealMoney, 9.99f, "vip_icon"),

                // Cosmetics
                new ShopItem("skin_golden", "Golden Vehicle Skin", "Shiny gold vehicle skin",
                    ShopItemType.Cosmetic, CurrencyType.Gems, 500, "golden_skin_icon"),

                new ShopItem("trail_rainbow", "Rainbow Trail", "Colorful particle trail",
                    ShopItemType.Cosmetic, CurrencyType.Gems, 300, "rainbow_trail_icon"),
            };
        }

        /// <summary>
        /// Attempts to purchase an item
        /// </summary>
        public bool PurchaseItem(string itemId)
        {
            if (!itemDatabase.ContainsKey(itemId))
            {
                Debug.LogWarning($"[ShopManager] Item not found: {itemId}");
                onPurchaseFailed?.Invoke(null, "Item not found");
                return false;
            }

            ShopItem item = itemDatabase[itemId];

            // Check if already owned (for non-consumables)
            if (IsItemOwned(itemId) && item.itemType != ShopItemType.PowerUpBundle && item.itemType != ShopItemType.Currency)
            {
                Debug.LogWarning($"[ShopManager] Item already owned: {itemId}");
                onPurchaseFailed?.Invoke(item, "Already owned");
                return false;
            }

            // Process purchase based on currency type
            bool purchaseSuccess = false;

            switch (item.currencyType)
            {
                case CurrencyType.Gold:
                    purchaseSuccess = PurchaseWithGold(item);
                    break;

                case CurrencyType.Gems:
                    purchaseSuccess = PurchaseWithGems(item);
                    break;

                case CurrencyType.RealMoney:
                    purchaseSuccess = PurchaseWithRealMoney(item);
                    break;
            }

            if (purchaseSuccess)
            {
                ProcessPurchase(item);
                onItemPurchased?.Invoke(item);
                onShopInventoryUpdated?.Invoke();

                Debug.Log($"[ShopManager] Successfully purchased: {item.itemName}");
            }

            return purchaseSuccess;
        }

        /// <summary>
        /// Purchases item with gold
        /// </summary>
        private bool PurchaseWithGold(ShopItem item)
        {
            if (SaveManager.Instance == null) return false;

            int goldCost = Mathf.RoundToInt(item.price);

            if (SaveManager.Instance.SpendGold(goldCost))
            {
                // Track gold spent for achievements
                if (AchievementManager.Instance != null)
                {
                    AchievementManager.Instance.OnGoldSpent(goldCost);
                }

                return true;
            }
            else
            {
                onPurchaseFailed?.Invoke(item, "Insufficient gold");
                return false;
            }
        }

        /// <summary>
        /// Purchases item with gems
        /// </summary>
        private bool PurchaseWithGems(ShopItem item)
        {
            if (SaveManager.Instance == null) return false;

            int gemCost = Mathf.RoundToInt(item.price);
            int currentGems = SaveManager.Instance.GetSaveData().totalGems;

            if (currentGems >= gemCost)
            {
                SaveManager.Instance.GetSaveData().totalGems -= gemCost;
                SaveManager.Instance.SaveGame();
                return true;
            }
            else
            {
                onPurchaseFailed?.Invoke(item, "Insufficient gems");
                return false;
            }
        }

        /// <summary>
        /// Purchases item with real money (IAP)
        /// </summary>
        private bool PurchaseWithRealMoney(ShopItem item)
        {
            // This would integrate with Unity IAP
            // For now, simulate successful purchase in testing
            Debug.Log($"[ShopManager] Processing IAP purchase: {item.itemName} (${item.price})");

            // TODO: Integrate Unity IAP
            // UnityPurchasing.BuyProductID(item.itemId);

            return true; // Simulate success for now
        }

        /// <summary>
        /// Processes purchase and grants rewards
        /// </summary>
        private void ProcessPurchase(ShopItem item)
        {
            switch (item.itemType)
            {
                case ShopItemType.Vehicle:
                    UnlockVehicle(item.itemId);
                    break;

                case ShopItemType.PowerUpBundle:
                    GrantPowerUpBundle(item.itemId, item.quantity);
                    break;

                case ShopItemType.Currency:
                    GrantCurrency(item.itemId, item.quantity);
                    break;

                case ShopItemType.Bundle:
                    ProcessBundle(item.itemId);
                    break;

                case ShopItemType.Permanent:
                    UnlockPermanentItem(item.itemId);
                    break;

                case ShopItemType.Cosmetic:
                    UnlockCosmetic(item.itemId);
                    break;
            }

            // Mark as owned
            MarkItemAsOwned(item.itemId);
        }

        /// <summary>
        /// Unlocks a vehicle
        /// </summary>
        private void UnlockVehicle(string itemId)
        {
            if (SaveManager.Instance != null)
            {
                string vehicleName = GetVehicleNameFromItemId(itemId);
                SaveManager.Instance.UnlockVehicle(vehicleName);

                if (AchievementManager.Instance != null)
                {
                    int totalVehicles = SaveManager.Instance.GetSaveData().unlockedVehicles.Count;
                    AchievementManager.Instance.OnVehicleUnlocked(totalVehicles);
                }
            }
        }

        /// <summary>
        /// Grants power-up bundle to inventory
        /// </summary>
        private void GrantPowerUpBundle(string itemId, int quantity)
        {
            // Add to consumable inventory (would be stored in SaveManager)
            Debug.Log($"[ShopManager] Granted {quantity}x {itemId} power-ups");

            // Store in PlayerPrefs for now (should be in SaveManager)
            int currentCount = PlayerPrefs.GetInt($"PowerUp_{itemId}", 0);
            PlayerPrefs.SetInt($"PowerUp_{itemId}", currentCount + quantity);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Grants currency to player
        /// </summary>
        private void GrantCurrency(string itemId, int amount)
        {
            if (SaveManager.Instance == null) return;

            if (itemId.Contains("gold"))
            {
                SaveManager.Instance.AddGold(amount);
                Debug.Log($"[ShopManager] Granted {amount} gold");
            }
            else if (itemId.Contains("gems"))
            {
                SaveManager.Instance.GetSaveData().totalGems += amount;
                SaveManager.Instance.SaveGame();
                Debug.Log($"[ShopManager] Granted {amount} gems");
            }
        }

        /// <summary>
        /// Processes bundle purchase (multiple items)
        /// </summary>
        private void ProcessBundle(string itemId)
        {
            switch (itemId)
            {
                case "starter_pack":
                    SaveManager.Instance.AddGold(1000);
                    UnlockVehicle("vehicle_dual_scoop");
                    break;
            }
        }

        /// <summary>
        /// Unlocks permanent item (remove ads, VIP, etc.)
        /// </summary>
        private void UnlockPermanentItem(string itemId)
        {
            PlayerPrefs.SetInt($"Permanent_{itemId}", 1);
            PlayerPrefs.Save();

            Debug.Log($"[ShopManager] Unlocked permanent item: {itemId}");
        }

        /// <summary>
        /// Unlocks cosmetic item
        /// </summary>
        private void UnlockCosmetic(string itemId)
        {
            PlayerPrefs.SetInt($"Cosmetic_{itemId}", 1);
            PlayerPrefs.Save();

            Debug.Log($"[ShopManager] Unlocked cosmetic: {itemId}");
        }

        /// <summary>
        /// Marks item as owned
        /// </summary>
        private void MarkItemAsOwned(string itemId)
        {
            PlayerPrefs.SetInt($"Owned_{itemId}", 1);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Checks if item is owned
        /// </summary>
        public bool IsItemOwned(string itemId)
        {
            return PlayerPrefs.GetInt($"Owned_{itemId}", 0) == 1;
        }

        /// <summary>
        /// Gets all shop items of a specific type
        /// </summary>
        public List<ShopItem> GetItemsByType(ShopItemType type)
        {
            return shopItems.Where(item => item.itemType == type).ToList();
        }

        /// <summary>
        /// Gets all shop items
        /// </summary>
        public List<ShopItem> GetAllItems()
        {
            return shopItems;
        }

        /// <summary>
        /// Gets shop item by ID
        /// </summary>
        public ShopItem GetItem(string itemId)
        {
            return itemDatabase.ContainsKey(itemId) ? itemDatabase[itemId] : null;
        }

        /// <summary>
        /// Helper to convert item ID to vehicle name
        /// </summary>
        private string GetVehicleNameFromItemId(string itemId)
        {
            switch (itemId)
            {
                case "vehicle_dual_scoop": return "Dual-Scoop Loader";
                case "vehicle_nitro_hauler": return "Nitro Hauler";
                case "vehicle_mega_vault": return "Mega Vault Truck";
                default: return itemId;
            }
        }

        /// <summary>
        /// Checks if player can afford item
        /// </summary>
        public bool CanAffordItem(string itemId)
        {
            if (!itemDatabase.ContainsKey(itemId)) return false;

            ShopItem item = itemDatabase[itemId];

            switch (item.currencyType)
            {
                case CurrencyType.Gold:
                    return SaveManager.Instance.GetGold() >= item.price;

                case CurrencyType.Gems:
                    return SaveManager.Instance.GetSaveData().totalGems >= item.price;

                case CurrencyType.RealMoney:
                    return true; // Always available for purchase

                default:
                    return false;
            }
        }
    }

    /// <summary>
    /// Shop item data structure
    /// </summary>
    [Serializable]
    public class ShopItem
    {
        public string itemId;
        public string itemName;
        public string description;
        public ShopItemType itemType;
        public CurrencyType currencyType;
        public float price;
        public string iconName;
        public int quantity = 1; // For bundles and consumables
        public bool isLimitedTime = false;
        public DateTime expirationDate;

        public ShopItem(string id, string name, string desc, ShopItemType type,
            CurrencyType currency, float price, string icon, int qty = 1)
        {
            this.itemId = id;
            this.itemName = name;
            this.description = desc;
            this.itemType = type;
            this.currencyType = currency;
            this.price = price;
            this.iconName = icon;
            this.quantity = qty;
        }
    }

    public enum ShopItemType
    {
        Vehicle,
        PowerUpBundle,
        Currency,
        Bundle,
        Permanent,
        Cosmetic
    }

    public enum CurrencyType
    {
        Gold,
        Gems,
        RealMoney
    }
}
