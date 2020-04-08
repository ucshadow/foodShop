# Food Store Project by Catalin https://foodstoren.azurewebsites.net/

## If you are using the azure website to look at the project, please allow some time for it to boot since is on the free tier plan!

### Not all features were fully tested on Azure (limited bandwidth), so something might not work.

### Admin account credentials:
email: ucshadow@gmail.com

password: 123

## Libraries and Frameworks used:

#### Back-end

* **AspNet MVC** as a web framework 
* **Entity Framework** as a database ORM
* **Owin** as authentication middleware
* **AspNet Identity** as authentication library
* **Ninject** as dependency injection
* **SQL server** as a database


#### Front-end

* **Bootstrap**
* **Vanilla Javascript**

#### 3'rd parties
* **Food names and definitions** from kaggle dataset https://www.kaggle.com/plarmuseau/foods-definitions/version/2
* **Nutrition facts** from USDA National Nutrient Database for Standard Reference, Release 27 https://fdc.nal.usda.gov/
* **Live recipe of the day** from https://edamam.com
* **Product Pictures** scraped from bing image search
* **Html template** from https://colorlib.com/
* **Azure App Services** https://azure.microsoft.com/da-dk/services/app-service/
* **Azure SQL Server** https://azure.microsoft.com/da-dk/services/sql-database/

---

#### Features

* **Normal user**

	* [Real time sells tracker](#real-time-sells-tracker)
    * [Discounts of the day](#discounts-of-the-day)
    * [Categories](#categories)
    * [Public profiles](#public-profiles)
    * [Purchase history](#purchase-history)
    * [Recipe of the day](#recipe-of-the-day)
    * [Stickers](#stickers)
    * [Comments](#comments)
    * [Ratings](#ratings)
    * [Discounts](#discounts)
    * [Nutrition Data](#nutrition-data)
    * [Real time search](#real-time-search)
*  **Affiliate**
   *  [Sell FoodStore Products from your site/app](#an-affiliate-that-sells-the-products-on-this-website-from-his-website)
   *  [Sell your products on the FoodStore site](#an-affiliate-that-can-sell-his-products-using-this-website)
   *  [Track your sells](#an-affiliate-can-track-his-sells-from-the-website)
      
* **Admin**
    * [On-site edit of product](#on-site-edit-of-product)
    * [On-site *add to discounts of the day*](#on-site-add-to-discounts-of-the-day)
    * [On-site *discount all products in category*](#nn-site-discount-all-products-in-category)
    * [Cache Utilities (view and clear cache)](#cache-utilities-view-and-clear-cache)
    * [Discounts Utilitites (Add / Remove discounts)](#discounts-utilitites-add-remove-discounts))
    * [Manage Stickers](#manage-stickers)
    * [Manage affiliate products](#manage-affiliate-products)
    * [Direct Product import from 3'rd party API's ](#direct-product-import-from-3rd-party-apis )
* **Misc**
  * Auto daily discount worker with manageable properties
  * Auto recipe of the day with 3'rd party API
  * Easy search for recipe of the day ingredients on site
  * Recomandet products form the same category
  * Hide/show personal purchase history to public
    
 

#### Details
#### Real time sells tracker

Tracks in real time sells happening on the site. It also counts affiliate sells. 
Tracks latest 4 products, including items remaining

![real time tracker](https://i.imgur.com/0EfB9Au.jpg "Real time tracker")

##### Discounts of the day

Shows discounted items for that day (or for the time interval set by the Admin). If it is set to automanager,
it auto updates to a given interval. The settings can be modified by an admin in the admin pannel
for discounts.

![Discounts of the day](https://i.imgur.com/xWJWCgY.jpg "Discounts of the day")

##### Categories

Categories are auto generated for each new category attached to a product. You can click on
a category and all products from that categy are shown. If there are more than 8 products, 
a page counter/toggler is displayed at the left bottom side corner to switch between pages.

![Categories](https://i.imgur.com/VqqCkkm.png "Categories")

##### Public profiles

The public profile is what other people can see about you. It has a name, city, image and 
a toggle to show/hide your purchase history. 

![Public profile](https://i.imgur.com/ab7ezNU.jpg "Public profile")

##### Purchase history

You can opt in to show your purchase history on your public profile.

![Public profile with history](https://i.imgur.com/qm9VXNn.jpg "Public profile with history")

##### Recipe of the day

The recipe of the day is a random recipe from a 3'rd party API
The ingredients can be clicked and you will be redirected to a page with 
search results for that ingredient. Note that some of the ingredients dont work
since thay have nothing to do with foods. The API may fail sometimes so a refresh button
is present (also good if a new recipe is wanted, since the recipe is cached to limit requests 
to the external API). Keep in mind that the API requires authentication credentials which are stored 
in the system PATH, so this may not work locally (there is a hard coded query result but I did not test it)

![Recipe of the day](https://i.imgur.com/ELJ9hLH.jpg "Recipe of the day")

##### Affiliate

There are 2 types of affiliates:

##### An affiliate that sells the products on this website from his website

You can become an affiliate and be offered an id and access to a CORS API to 
sell FoodStore's products on your own site/app whatever IoT implementation.
The API provides access to all products and products are shipped using 
the shipping implementation on the website exposed as an API.
All affiliate products sold are tracked separatly, but the purchase history is saved
on the affiliate account.
An affiliate demo is available on /affiliateExample where you MUST select a product 
and then complete the delivery lines. Note that you must be an affiliate on the current loggedin account
for the example page to work, so if you are not, head over to Pages/Affiliate, pick a name and click on BECOME!.
The example is for demo only and it may fail if the data is incorect.
The products you sold using the affiliate API are shown on the affiliate page.

![Affiliate](https://i.imgur.com/JSvUrlp.jpg "Affiliate")

##### An affiliate that can sell his products using this website

Just like Amazon sells products from 3'rd parties, this type of affiliate is more like a partner, but the functionality was added after the first type
on top of the already present affiliate functionality, so the name was left the same.
This offers the posibility to register your own products on the foodShop website.
To do so, you must register yourself as an affiliate and have an affiliate name (you can do this from the affiliate
page). Now, you can click on Add Affiliate Product link, and be presented with a page where you can add a product
following the standards of the Product on the foodStore website. When you save the product, it now shows on
your affiliate products tab as *Pending approval* meaning an admin has to approve it for it to become
an official product on the site.

![Affiliate](https://i.imgur.com/44EXDpY.jpg "Pending approval")

On the main site, a Product that is sold by an affiliate has a sticker with the affiliate's name in []
for example [best sweets]

![Affiliate](https://i.imgur.com/KSWdOan.jpg "Sold by best sweets")

##### Manage affiliate products

An admin can go into the affiliate pannel on the admin side of the site and check if there are any 
affiliate products waiting approval. If there are, he can then approve the product.
Imediatly following the approval, the product is searchable and buyable in the main site and the 
selling of the affiliate product(s) is tracked on the affiliate page of each affiliate.

The admin affiliate interface offers a list of all products: approved, pending and rejected.

![Affiliate](https://i.imgur.com/iSNcWFU.jpg "Admin affiliate interface")

Affiliates are a great way for a small start-up to grow and expand.
##### An affiliate can track his sells from the website

![Affiliate](https://i.imgur.com/igmEQmf.jpg "Sell tracking")


##### Stickers

Stickers are extra data represented as small text that you can slap on a product 
(for example CHRISMAS SALE!! or FREE!! or whatever). Of course, since no styling was asked
for, right now the stickers are just extra rows of info :)
TODO: filter by sticker

![Stickers](https://i.imgur.com/c1zTruE.jpg "Stickers")

##### Comments

Comments can be added to a product. The initial plan was to split the comments
into multiple types (comments/questions/complaints etc) but as I was working on them
I realised that I am getting out of scope, since this is not a chat app, so it remained
at only 1 type, comment.
You must purchase a product to be able to comment on it. You can only comment once and
you cannot edit your comment. Your comment on a product is seen with a red border.

![Comments](https://i.imgur.com/8j80VAN.jpg "Comments")

##### Ratings

You can rate a product you bought from your purchase history. You can only rate it once 
and cannot change the rating.

![Ratings](https://i.imgur.com/jmL7fZT.jpg "Ratings")

##### Discounts

These are general discount, not related to the daily discounts and can pe repsented on 
any Product.

##### Nutrition Data

You can click on Nutrition data on any product and get a list of nutrition data available for
that product. Why a list? well for example if you pick chicken you need to select from the
list all the *features* the chicken has for ex: is it raw, is it whole, is it chicken breast,
does it have skin, is it fried, boiled, baked etc etc, so you will get a big list of choices,
a lot of them having min imum to do with the product itself, but there was no good approach 
on how to filter this data, so I just left it kinda like how it is 
(there are ~10000 entries in the nutrient data set). Also the dataset is not stored in a db
since on Azure the databse is charged by SKU or somthing like that, basically the amount you
pay is proportional to the CPU usage of the database server, so I opted to not add 10k entryes
that I need to filter.

##### Real time search

You can type in the search bar and get immediate feed-back. This is done on the client side
with a fetch call to an exposed API end-point in the Product Controller.

![Real time search](https://i.imgur.com/ShFsrIB.jpg "Real time search") 

##### On-site edit of product

As an admin you can access the edit functionality of a product right from the normal web-site.
If you are logged in as an admin all Products everywhere have a small Edit link you can click 
to edit that Product. 

##### On-site *add to discounts of the day*

As an admin you can add a product to the discounts of the day list from the website.
If you are logged as an Admin, any Product anywhere has a small green button with a D on it that
you can click and add that product to the daily discount list.
Note that it will be recycled with the other products if the AutoDailyDiscounter runs.

![Add to discount of the day and edit](https://i.imgur.com/Arxzmtw.jpg "Add to discount of the day and edit")

##### On-site *discount all products in category*

As an admin you can discount a whole category of products just by specifying an ammount and clicking
a button. If you are logged as an admin and navigate to any category of products you have an added
text field and a button where you can add a discount percentage and submit it, and all products in that 
category will be discounted.

![Category discount](https://i.imgur.com/e4Is7lV.jpg "Category discount") 

##### Cache Utilities view and clear cache

All ProductModel(s) are cached. An admin can see what producsts are cached and can clear the cache.
Only the ProductModel uses the chache. The ProductModel is used to display all the details about a 
Product, so it's like the product's page. Note that the Product in the Product cards presented on 
the site everywhere use the Product Entity (unfortunately). I was planning to move them to use a 
Model instead but the refractor would be too big to worth it right now. More details in person :)

##### Discounts Utilitites Add Remove discounts

An admin can have fine tunning access to discounts and the AutoDailyDiscounter from the admin discount 
pannel. This includes changinng the discounter parameters and adding and removing specific discounts.

![Admin discount pannel](https://i.imgur.com/eGKSy54.jpg "Admin discount pannel") 

##### Manage Stickers

As an admin you can access the sticker panel and manage stickers.
Work still in progress....

##### Direct Product import from 3rd party APIs 

As an admin you can directly import products from 3 food stores in Denmark directly from the admin panel.
You can go on the admin pannel and click on *Import from competition* link. There you will be presented with 
3 picks coop.dk, skagenfood.dk and nemlig.com 

When going on either of those, the server connects to their unofficial API's and translates the
API response into products that can be edited and saved as your product (with a better price of course).
The API functionality is for demo purpose only and the traffic is keps at a minimum, so no real direct impact
on the specified websites.

Please keep in mind that external API's may change, so this may not work in the future.

![Admin discount pannel](https://i.imgur.com/1NLpNC4.jpg "Admin import product") 

## Final thoughts

The project would have been more structured and the code would have been more efficient if I knew how the final
product would have looked from the beginning. 
The Project started with a simple shop to which I kept adding features over features and at some point I realised
that I am not satisfied with the project structure and design approach. 

I am sure things would be done differently if I am to start the project again and implement the features it has now.
Things like caching, exposing API's, 3'rd party interaction, user roles, testing and so on would have a better design
with scalability and maintainability in mind.

My main focus would be relations between entities (foreign keys) and atomicity.
A lot of the database queries can be made more efficient by extracting common attributes into separated tables,
this is the thing I know I can improve if I have the database model from the beginning.

The thing is, the whole process for me was a learning process, althought I have some experience with the whole MVC/web frameworks
technologies (mostly MERN and Django), I have never tried NET MVC before and I have learned a lot in the process of doing this project,
especially about NET MVC and Entity Framework.

There is only one thing I regret with the NET stack, and the thing is that I did not started with NET Core instead.



