Á
uC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Domain\Repositories\IProductRepository.cs
	namespace 	
Domain
 
. 
Repositories 
{ 
public 

	interface 
IProductRepository '
{ 
Task 
< 
IEnumerable 
< 
Product  
>  !
>! "
GetAllAsync# .
(. /
)/ 0
;0 1
Task 
< 
Product 
> 
GetByIdAsync "
(" #
Guid# '
id( *
)* +
;+ ,
Task		 
<		 
Guid		 
>		 
AddAsync		 
(		 
Product		 #
product		$ +
)		+ ,
;		, -
Task

 
UpdateAsync

 
(

 
Product

  
product

! (
)

( )
;

) *
Task 
DeleteAsync 
( 
Guid 
id  
)  !
;! "
} 
} õ
oC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Domain\Entities\ShoppingCartItem.cs
	namespace 	
Domain
 
. 
Entities 
{ 
public 

class 
ShoppingCartItem !
:" #
CartListItemBase$ 4
{ 
public 
Guid 
Cart_Id 
{ 
get !
;! "
set# &
;& '
}( )
public 
int 
Quantity 
{ 
get !
;! "
set# &
;& '
}( )
} 
} è	
zC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Domain\Repositories\IWishlistItemRepository.cs
	namespace 	
Domain
 
. 
Repositories 
{ 
public 

	interface #
IWishlistItemRepository ,
{ 
Task 
< 
IEnumerable 
< 
WishlistItem %
>% &
>& '
GetAllAsync( 3
(3 4
)4 5
;5 6
Task 
< 
WishlistItem 
> 
GetByIdAsync '
(' (
Guid( ,
id- /
)/ 0
;0 1
Task		 
<		 
Guid		 
>		 
AddAsync		 
(		 
WishlistItem		 (
wishlistItem		) 5
)		5 6
;		6 7
Task

 
UpdateAsync

 
(

 
WishlistItem

 %
wishlistItem

& 2
)

2 3
;

3 4
Task 
DeleteAsync 
( 
Guid 
id  
)  !
;! "
} 
} ¨	
zC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Domain\Repositories\IShoppingCartRepository.cs
	namespace 	
Domain
 
. 
Repositories 
{ 
public 

	interface #
IShoppingCartRepository ,
{ 
Task 
< 
IEnumerable 
< 
ShoppingCartItem )
>) *
>* +
GetAllItemsAsync, <
(< =
)= >
;> ?
Task 
< 
ShoppingCartItem 
> 
GetItemByIdAsync /
(/ 0
Guid0 4
id5 7
)7 8
;8 9
Task		 
<		 
Guid		 
>		 
AddItemAsync		 
(		  
ShoppingCartItem		  0
cartItem		1 9
)		9 :
;		: ;
Task

 
UpdateItemAsync

 
(

 
ShoppingCartItem

 -
cartItem

. 6
)

6 7
;

7 8
Task 
RemoveItemAsync 
( 
Guid !
id" $
)$ %
;% &
} 
} ﬂ
tC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Domain\Repositories\IClientRepository.cs
	namespace 	
Domain
 
. 
Repositories 
{ 
public 

	interface 
IClientRepository &
{ 
Task 
< 
IEnumerable 
< 
Client 
>  
>  !
GetAllAsync" -
(- .
). /
;/ 0
Task 
< 
Client 
> 
GetByIdAsync !
(! "
Guid" &
id' )
)) *
;* +
Task		 
<		 
Guid		 
>		 
AddAsync		 
(		 
Client		 "
client		# )
)		) *
;		* +
Task

 
UpdateAsync

 
(

 
Client

 
client

  &
)

& '
;

' (
Task 
DeleteAsync 
( 
Guid 
id  
)  !
;! "
} 
} ˙
kC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Domain\Entities\WishlistItem.cs
	namespace 	
Domain
 
. 
Entities 
{ 
public 

class 
WishlistItem 
: 
CartListItemBase  0
{ 
public 
Guid 
List_Id 
{ 
get !
;! "
set# &
;& '
}( )
} 
}		  	
fC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Domain\Entities\Product.cs
	namespace 	
Domain
 
. 
Entities 
{ 
public 

class 
Product 
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Category 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Description !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 
decimal		 
Price		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public

 
bool

 
IsNegotiable

  
{

! "
get

# &
;

& '
set

( +
;

+ ,
}

- .
} 
} ©
eC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Domain\Entities\Client.cs
	namespace 	
Domain
 
. 
Entities 
{ 
public 

class 
Client 
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Username 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 
string		 
Location		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
}

 
} ù
oC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Domain\Entities\CartListItemBase.cs
	namespace 	
Domain
 
. 
Entities 
{ 
public 

class 
CartListItemBase !
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
Guid 

Product_Id 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
virtual 
Product 
Product &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
} 
}		 