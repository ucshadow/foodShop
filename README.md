# Food Store Project by Catalin

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

#### 3'rd part
* **Food names and definitions** from kaggle dataset https://www.kaggle.com/plarmuseau/foods-definitions/version/2
* **Nutrition facts** from USDA National Nutrient Database for Standard Reference, Release 27 https://fdc.nal.usda.gov/
* **Live recipe of the day** from https://edamam.com
* **Product Pictures** scraped from bing image search
* **Html template** from https://colorlib.com/

---

#### Features

* **Normal user**

	* Real time sells tracker
    * Discounts of the day
    * Categories
    * Public profiles
    * Purchase history
    * Recipe of the day
    * Affiliate
    * Stickers
    * Comments
    * Ratings
    * Discounts
    * Nutrition Data
    * Real time search
* **Admin**
    * On-site edit of product
    * On-site *add to discounts of the day*
    * On-site *discount all products in category*
    * Cache Utilities (view and clear cache)
    * Discounts Utilitites (Add / Remove discounts)
    * Manage Stickers
* **Misc**
  * Auto daily discount worker with manageable properties
  * Auto recipe of the day with 3'rd party API
  * Easy search for recipe of the day ingredients on site
  * Recomandet products form the same category
  * Hide/show personal purchase history to public
    
 

#### Details
##### Real time sells tracker

Tracks in real time sells happening on the site. It also counts affiliate sells. 
Tracks latest 4 products, including items remaining
![real time tracker](https://i.imgur.com/0EfB9Au.jpg "Real time tracker")

##### Discounts of the day

Shows discounted items for that day (or for the time interval set). If it's set to automanager,
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

![Public profile with history](https://i.imgur.com/qm9VXNn.jpg "Public profile with history")

##### Recipe of the day

The recipe of the day is a random recipe from a 3'rd party API
The ingredients can be clicked and you will be redirected to a page with 
search results for that ingredient. Note that some of the ingredients dont work
since thay have nothing to do with foods. The API may fail sometimes so a refresh button
is present (also good if a new recipe is wanted, since the recipe is cached to limit requests 
to the external API)

![Recipe of the day](https://i.imgur.com/ELJ9hLH.jpg "Recipe of the day")

##### Affiliate

You can become an affiliate and be offered an id and access to a CORS API to 
use the site to sell products on your own site/app whatever IoT implementation.
The API provides access to all products and products are shipped using 
the shipping implementation on the website exposed as an API.
All affiliate products sold are tracked separatly, but the purchase history is saved
on the affiliate account.
An affiliate demo is available on /affiliateExample where you MUST add a product id (between 1 and ~800),
click GET and then complete the delivery lines and affiliate id (pasted from the top).
The example is for demo only and it may fail if the data is incorect.
The products you sold using the affiliate API are shown on the affiliate page.

![Affiliate](https://i.imgur.com/JSvUrlp.jpg "Affiliate")

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

![Ratings](https://i.imgur.com/jmL7fZT.jpg "Ratings") 

##### Nutrition Data

You can click on Nutrition data on any product and get a list of nutrition data available for
that product. Why a list? well for example if you pick chicken you need to select from the
list all the *features* the chicken has for ex: is it raw, is it whole, is it chicken breast,
does it have skin, is it fried, boiled, baked etc etc, so you will get a big list of chices,
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

##### On-site *discount all products in category*

As an admin you can discount a whole category of products just by specifying an ammount and clicking
a button. If you are logged as an admin and navigate to any category of products you have an added
text field and a button where you can add a discount percentage and submit it, and all products in that 
category will be discounted.

![Category discount](https://i.imgur.com/e4Is7lV.jpg "Category discount") 

##### Cache Utilities (view and clear cache)

All ProductModel(s) are cached. An admin can see what producsts are cached and can clear the cache.
Only the ProductModel uses the chache. The ProductModel is used to display all the details about a 
Product, so it's like the product's page. Note that the Product in the Product cards presented on 
the site everywhere use the Product Entity (unfortunately). I was planning to move them to use a 
Model instead but the refractor would be too big to worth it right now. More details in person :)

##### Discounts Utilitites (Add / Remove discounts)

An admin can have fine tunning access to discounts and the AutoDailyDiscounter from the admin discount 
pannel. This includes changinng the discounter parameters and adding and removing specific discounts.

![Admin discount pannel](https://i.imgur.com/eGKSy54.jpg "Admin discount pannel") 

##### Manage Stickers

As an admin you can access the sticker panel and manage stickers.
Work still in progress....