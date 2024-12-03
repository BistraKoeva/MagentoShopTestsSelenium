using MagentoDemoStoreTestsPOM.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoDemoStoreTestsPOM.Tests
{
    public class AddToCartRemoveProductsTests : BaseTest
    {
        [Test]
        public void AddProductToCartGuestUser()
        {
            HomePage homePage = new HomePage(driver);
            homePage.NavigateToPage(HomePage.Url);
            MainCategoryPage mainCategoryPage = homePage.ChooseMainCategoryFromNavbar(HomePage.WomenNavbarLinkXpath);
            ProductsPage productsPage = mainCategoryPage.ChooseCategoryFromMainSideBar("Bottoms");
            ProductObject product = productsPage.GetProductByIndex(1);
            product.SelectSizeAndColorAndAddToCart("28", "Blue");

            Assert.That(productsPage.SuccessMessage.Text, Is.EqualTo($"You added {product.Name} to your shopping cart."), "The success message for adding to cart is not as expected.");
            
        }

        [Test]
        public void VerifyMiniCartContentOneItem()
        {
            HomePage homePage = new(driver);
            homePage.NavigateToPage(HomePage.Url);

            ProductObject product = AddProductToCart(homePage, HomePage.WomenNavbarLinkXpath, "Tops", 1, "S", "Yellow");

            CartMiniViewPage cartMiniView = homePage.OpenCartMiniView();

            Assert.That(cartMiniView.CartCounterNumber.Text, Is.EqualTo("1"), "The cart counter does not show the correct number.");
            Assert.That(cartMiniView.ProductNamesElements[0].Text, Is.EqualTo(product.Name), "The name of the product is not as expected.");
            Assert.That(cartMiniView.ProductsPricesElements[0].Text, Is.EqualTo(product.Price), "The price of the product is not as expected.");
            Assert.That(cartMiniView.ItemsTotalCountElement.Text, Is.EqualTo("1"), "The total count of items in the cart is not correct.");
            Assert.That(cartMiniView.CartSubtotalElement.Text, Is.EqualTo(product.Price), "The cart subtotal is not correct.");


            cartMiniView.ExpandCollapseSeeDetails(0);

            Assert.That(cartMiniView.ProductsSizesAndColorsElements[0].Text, Is.EqualTo("S"), "The chosen size is not correct.");
            Assert.That(cartMiniView.ProductsSizesAndColorsElements[1].Text, Is.EqualTo("Yellow"), "The chosen color is not correct.");
        }

        [Test]
        public void VerifyMiniCartContentMultipleItems()
        {
            HomePage homePage = new(driver);
            homePage.NavigateToPage(HomePage.Url);

            ProductObject product1 = AddProductToCart(homePage, HomePage.WomenNavbarLinkXpath, "Tops", 1, "S", "Yellow");
            ProductObject product2 = AddProductToCart(homePage, HomePage.WomenNavbarLinkXpath, "Tops", 5, "M", "Purple");

            CartMiniViewPage cartMiniView = homePage.OpenCartMiniView();

            //The last added item always goes on top in mini cart.
            Assert.That(cartMiniView.CartCounterNumber.Text, Is.EqualTo("2"), "The cart counter does not show the correct number.");
            Assert.That(cartMiniView.ProductNamesElements[0].Text, Is.EqualTo(product2.Name), "The name of the product is not as expected.");
            Assert.That(cartMiniView.ProductNamesElements[1].Text, Is.EqualTo(product1.Name), "The name of the product is not as expected.");
            Assert.That(cartMiniView.ProductsPricesElements[0].Text, Is.EqualTo(product2.Price), "The price of the product is not as expected.");
            Assert.That(cartMiniView.ProductsPricesElements[1].Text, Is.EqualTo(product1.Price), "The price of the product is not as expected.");
            Assert.That(cartMiniView.ItemsTotalCountElement.Text, Is.EqualTo("2"), "The total count of items in the cart is not correct.");
            string price1 = product1.Price.Substring(1);
            string price2 = product2.Price.Substring(1);
            Assert.That(cartMiniView.CartSubtotalElement.Text, Is.EqualTo($"${(double.Parse(price1) + double.Parse(price2)):F2}"), "The cart subtotal is not correct.");

            cartMiniView.ExpandCollapseSeeDetails(0);
            cartMiniView.ExpandCollapseSeeDetails(1);

            Assert.That(cartMiniView.ProductsSizesAndColorsElements[0].Text, Is.EqualTo(product2.Size), "The chosen size of product 1 is not correct.");
            Assert.That(cartMiniView.ProductsSizesAndColorsElements[1].Text, Is.EqualTo(product2.Color), "The chosen color of product 1 is not correct.");
            Assert.That(cartMiniView.ProductsSizesAndColorsElements[2].Text, Is.EqualTo(product1.Size), "The chosen size of product 2 is not correct.");
            Assert.That(cartMiniView.ProductsSizesAndColorsElements[3].Text, Is.EqualTo(product1.Color), "The chosen color of product 2 is not correct.");
        }

        [Test]
        public void ChangeQuantityOfProductInMiniCart()
        {
            HomePage homePage = new(driver);
            homePage.NavigateToPage(HomePage.Url);

            ProductObject product = AddProductToCart(homePage, HomePage.WomenNavbarLinkXpath, "Tops", 1, "S", "Yellow");
           
            CartMiniViewPage cartMiniView = homePage.OpenCartMiniView();
            
            //The Clear method doesn't work most probably because the value is injected with JS, so we use this approach
            cartMiniView.ProductsQuantityInputs[0].SendKeys(OpenQA.Selenium.Keys.Control + "a");
            cartMiniView.ProductsQuantityInputs[0].SendKeys(OpenQA.Selenium.Keys.Delete);
            cartMiniView.ProductsQuantityInputs[0].SendKeys("3");
            cartMiniView.UpdateQuantityOfProduct(0);

            Assert.That(cartMiniView.ItemsTotalCountElement.Text, Is.EqualTo("3"), "The total count of items in the cart is not correct.");

            string price = product.Price.Substring(1);
            Assert.That(cartMiniView.CartSubtotalElement.Text, Is.EqualTo($"${(double.Parse(price) * 3):F2}"), "The cart subtotal is not correct.");
        }

        [Test]
        public void DeleteProductFromMiniCartMultipleItems()
        {
            HomePage homePage = new(driver);
            homePage.NavigateToPage(HomePage.Url);

            ProductObject product1 = AddProductToCart(homePage, HomePage.WomenNavbarLinkXpath, "Tops", 1, "S", "Yellow");
            ProductObject product2 = AddProductToCart(homePage, HomePage.WomenNavbarLinkXpath, "Tops", 5, "M", "Purple");

            CartMiniViewPage cartMiniView = homePage.OpenCartMiniView();

            //This will delete product2, since the last added product is always on top of the mini cart.
            cartMiniView.DeleteProduct(0);

            Assert.That(cartMiniView.CartCounterNumber.Text, Is.EqualTo("1"), "The cart counter does not show the correct number.");
            Assert.That(cartMiniView.ProductNamesElements.Count, Is.EqualTo(1), "The number of products in the cart is not as expected.");
            Assert.That(cartMiniView.ProductNamesElements[0].Text, Is.EqualTo(product1.Name), "The name of the product is not as expected.");
            Assert.That(cartMiniView.CartSubtotalElement.Text, Is.EqualTo(product1.Price), "The cart subtotal is not correct.");
        }

        [Test]
        public void DeleteLastProductFromMiniCart()
        {
            HomePage homePage = new(driver);
            homePage.NavigateToPage(HomePage.Url);

            ProductObject product = AddProductToCart(homePage, HomePage.WomenNavbarLinkXpath, "Tops", 5, "M", "Purple");

            CartMiniViewPage cartMiniView = homePage.OpenCartMiniView();

            cartMiniView.DeleteProduct(0);

            Assert.IsNotNull(cartMiniView.NoItemsInCartMessage, "The No items in cart message is not on the page.");
            Assert.That(cartMiniView.NoItemsInCartMessage.Text, Is.EqualTo("You have no items in your shopping cart."), "The No items in cart message is not as expected");
        }

        // This test must fail because there is a bug in the application - the Update cart button on the Product details page does not update cart. 
        [Test]
        public void EditProductSizeFromMiniCart()
        {
            HomePage homePage = new(driver);
            homePage.NavigateToPage(HomePage.Url);

            ProductObject product = AddProductToCart(homePage, HomePage.WomenNavbarLinkXpath, "Tops", 5, "M", "Purple");

            CartMiniViewPage cartMiniView = homePage.OpenCartMiniView();
            ProductDetailsPage productDetailsPage = cartMiniView.ClickEditProductLink(0);
            productDetailsPage.EditProductSize("S");
            cartMiniView = homePage.OpenCartMiniView();
            cartMiniView.ExpandCollapseSeeDetails(0);

            Assert.That(cartMiniView.ProductsSizesAndColorsElements[0].Text, Is.EqualTo("S"), "The size of the product was not edited.");
            Assert.That(cartMiniView.ProductsSizesAndColorsElements[1].Text, Is.EqualTo(product.Color), "The color of the product should stay the same.");
        }


        private ProductObject AddProductToCart(HomePage homePage, string mainCategory, string categoryFromSideBar, int indexOfProductOnPage, string size, string color)
        {
            MainCategoryPage mainCategoryPage = homePage.ChooseMainCategoryFromNavbar(mainCategory);
            ProductsPage productsPage = mainCategoryPage.ChooseCategoryFromMainSideBar(categoryFromSideBar);
            ProductObject product = productsPage.GetProductByIndex(indexOfProductOnPage);
            product.SelectSizeAndColorAndAddToCart(size, color);
            return product;
        }
    }
}
