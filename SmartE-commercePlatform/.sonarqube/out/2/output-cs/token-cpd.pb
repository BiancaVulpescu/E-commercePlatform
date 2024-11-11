≤
sC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Utils\ValidationBehavior.cs
	namespace 	
Application
 
. 
Utils 
{ 
public 

class 
ValidationBehavior #
<# $
TRequest$ ,
,, -
	TResponse. 7
>7 8
:9 :
IPipelineBehavior; L
<L M
TRequestM U
,U V
	TResponseW `
>` a
whereb g
TRequesth p
:q r
IRequests {
<{ |
	TResponse	| Ö
>
Ö Ü
{ 
private 
readonly 
IEnumerable $
<$ %

IValidator% /
</ 0
TRequest0 8
>8 9
>9 :

validators; E
;E F
public

 
ValidationBehavior

 !
(

! "
IEnumerable

" -
<

- .

IValidator

. 8
<

8 9
TRequest

9 A
>

A B
>

B C

validators

D N
)

N O
{ 	
this 
. 

validators 
= 

validators (
;( )
} 	
public 
async 
Task 
< 
	TResponse #
># $
Handle% +
(+ ,
TRequest, 4
request5 <
,< ="
RequestHandlerDelegate> T
<T U
	TResponseU ^
>^ _
next` d
,d e
CancellationTokenf w
cancellationToken	x â
)
â ä
{ 	
var 
context 
= 
new 
ValidationContext /
</ 0
TRequest0 8
>8 9
(9 :
request: A
)A B
;B C
var 
failures 
= 

validators %
. 
Select 
( 
v 
=> 
v 
. 
Validate '
(' (
context( /
)/ 0
)0 1
. 

SelectMany 
( 
result "
=># %
result& ,
., -
Errors- 3
)3 4
. 
Where 
( 
f 
=> 
f 
!=  
null! %
)% &
. 
ToList 
( 
) 
; 
if 
( 
failures 
. 
Count 
!= !
$num" #
)# $
{ 
throw 
new 
ValidationException -
(- .
failures. 6
)6 7
;7 8
} 
return 
await 
next 
( 
) 
;  
} 	
} 
} ◊
oC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Utils\MappingProfile.cs
	namespace 	
Application
 
. 
Utils 
{ 
public		 

class		 
MappingProfile		 
:		  !
Profile		" )
{

 
public 
MappingProfile 
( 
) 
{ 	
	CreateMap 
< 
Product 
, 

ProductDto )
>) *
(* +
)+ ,
., -

ReverseMap- 7
(7 8
)8 9
;9 :
	CreateMap 
<  
CreateProductCommand *
,* +
Product, 3
>3 4
(4 5
)5 6
.6 7

ReverseMap7 A
(A B
)B C
;C D
	CreateMap 
<  
UpdateProductCommand *
,* +
Product, 3
>3 4
(4 5
)5 6
.6 7

ReverseMap7 A
(A B
)B C
;C D
	CreateMap 
< 
WishlistItem "
," #
WishlistItemDto$ 3
>3 4
(4 5
)5 6
.6 7

ReverseMap7 A
(A B
)B C
;C D
	CreateMap 
< %
CreateWishlistItemCommand /
,/ 0
WishlistItem1 =
>= >
(> ?
)? @
.@ A

ReverseMapA K
(K L
)L M
;M N
	CreateMap 
< %
UpdateWishlistItemCommand /
,/ 0
WishlistItem1 =
>= >
(> ?
)? @
.@ A

ReverseMapA K
(K L
)L M
;M N
	CreateMap 
< 
ShoppingCartItem &
,& '
ShoppingCartItemDto( ;
>; <
(< =
)= >
.> ?

ReverseMap? I
(I J
)J K
;K L
	CreateMap 
< )
CreateShoppingCartItemCommand 3
,3 4
ShoppingCartItem5 E
>E F
(F G
)G H
.H I

ReverseMapI S
(S T
)T U
;U V
	CreateMap 
< )
UpdateShoppingCartItemCommand 3
,3 4
ShoppingCartItem5 E
>E F
(F G
)G H
.H I

ReverseMapI S
(S T
)T U
;U V
} 	
} 
} ≥
ëC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\QueryHandler\GetWishlistItemByIdQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
QueryHandlers  -
;- .
public

 
class

 +
GetWishlistItemByIdQueryHandler

 ,
:

- .
IRequestHandler

/ >
<

> ?$
GetWishlistItemByIdQuery

? W
,

W X
Result

Y _
<

_ `
WishlistItemDto

` o
>

o p
>

p q
{ 
private 
readonly #
IWishlistItemRepository ,#
_wishlistItemRepository- D
;D E
private 
readonly 
IMapper 
_mapper $
;$ %
public 
+
GetWishlistItemByIdQueryHandler *
(* +#
IWishlistItemRepository+ B"
wishlistItemRepositoryC Y
,Y Z
IMapper[ b
mapperc i
)i j
{ #
_wishlistItemRepository 
=  !"
wishlistItemRepository" 8
;8 9
_mapper 
= 
mapper 
; 
} 
public 

async 
Task 
< 
Result 
< 
WishlistItemDto ,
>, -
>- .
Handle/ 5
(5 6$
GetWishlistItemByIdQuery6 N
requestO V
,V W
CancellationTokenX i
cancellationTokenj {
){ |
{ 
try 
{ 	
var 
product 
= 
await #
_wishlistItemRepository  7
.7 8
GetByIdAsync8 D
(D E
requestE L
.L M
IdM O
)O P
;P Q
return 
product 
is 
null "
? 
Result 
< 
WishlistItemDto (
>( )
.) *
Failure* 1
(1 2
WishlistItemErrors2 D
.D E
NotFoundE M
(M N
requestN U
.U V
IdV X
)X Y
)Y Z
: 
Result 
< 
WishlistItemDto (
>( )
.) *
Success* 1
(1 2
_mapper2 9
.9 :
Map: =
<= >
WishlistItemDto> M
>M N
(N O
productO V
)V W
)W X
;X Y
} 	
catch 
( 
	Exception 
e 
) 
{ 	
return   
Result   
<   
WishlistItemDto   )
>  ) *
.  * +
Failure  + 2
(  2 3
WishlistItemErrors  3 E
.  E F!
GetWishlistItemFailed  F [
(  [ \
e  \ ]
.  ] ^
Message  ^ e
)  e f
)  f g
;  g h
}!! 	
}"" 
}## ∞
ïC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\QueryHandler\GetShoppingCartItemByIdQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
QueryHandler  ,
{		 
public

 

class

 /
#GetShoppingCartItemByIdQueryHandler

 4
:

5 6
IRequestHandler

7 F
<

F G(
GetShoppingCartItemByIdQuery

G c
,

c d
Result

e k
<

k l
ShoppingCartItemDto

l 
>	

 Ä
>


Ä Å
{ 
private 
readonly #
IShoppingCartRepository 0

repository1 ;
;; <
private 
readonly 
IMapper  
mapper! '
;' (
public /
#GetShoppingCartItemByIdQueryHandler 2
(2 3#
IShoppingCartRepository3 J

repositoryK U
,U V
IMapperW ^
mapper_ e
)e f
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
ShoppingCartItemDto! 4
>4 5
>5 6
Handle7 =
(= >(
GetShoppingCartItemByIdQuery> Z
request[ b
,b c
CancellationTokend u
cancellationToken	v á
)
á à
{ 	
try 
{ 
var 
cartItem 
= 
await $

repository% /
./ 0
GetItemByIdAsync0 @
(@ A
requestA H
.H I
IdI K
)K L
;L M
if 
( 
cartItem 
== 
null  $
)$ %
{ 
return 
Result !
<! "
ShoppingCartItemDto" 5
>5 6
.6 7
Failure7 >
(> ?"
ShoppingCartItemErrors? U
.U V
NotFoundV ^
(^ _
request_ f
.f g
Idg i
)i j
)j k
;k l
} 
var   
cartItemDto   
=    !
mapper  " (
.  ( )
Map  ) ,
<  , -
ShoppingCartItemDto  - @
>  @ A
(  A B
cartItem  B J
)  J K
;  K L
return!! 
Result!! 
<!! 
ShoppingCartItemDto!! 1
>!!1 2
.!!2 3
Success!!3 :
(!!: ;
cartItemDto!!; F
)!!F G
;!!G H
}"" 
catch## 
(## 
	Exception## 
ex## 
)##  
{$$ 
return%% 
Result%% 
<%% 
ShoppingCartItemDto%% 1
>%%1 2
.%%2 3
Failure%%3 :
(%%: ;"
ShoppingCartItemErrors%%; Q
.%%Q R
GetItemFailed%%R _
(%%_ `
ex%%` b
.%%b c
Message%%c j
)%%j k
)%%k l
;%%l m
}&& 
}'' 	
}(( 
}))  
åC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\QueryHandler\GetProductByIdQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
QueryHandlers  -
;- .
public

 
class

 &
GetProductByIdQueryHandler

 '
:

( )
IRequestHandler

* 9
<

9 :
GetProductByIdQuery

: M
,

M N
Result

O U
<

U V

ProductDto

V `
>

` a
>

a b
{ 
private 
readonly 
IProductRepository '
_productRepository( :
;: ;
private 
readonly 
IMapper 
_mapper $
;$ %
public 
&
GetProductByIdQueryHandler %
(% &
IProductRepository& 8
productRepository9 J
,J K
IMapperL S
mapperT Z
)Z [
{ 
_productRepository 
= 
productRepository .
;. /
_mapper 
= 
mapper 
; 
} 
public 

async 
Task 
< 
Result 
< 

ProductDto '
>' (
>( )
Handle* 0
(0 1
GetProductByIdQuery1 D
requestE L
,L M
CancellationTokenN _
cancellationToken` q
)q r
{ 
try 
{ 	
var 
product 
= 
await 
_productRepository  2
.2 3
GetByIdAsync3 ?
(? @
request@ G
.G H
IdH J
)J K
;K L
return 
product 
is 
null "
? 
Result 
< 

ProductDto #
># $
.$ %
Failure% ,
(, -
ProductErrors- :
.: ;
NotFound; C
(C D
requestD K
.K L
IdL N
)N O
)O P
: 
Result 
< 

ProductDto #
># $
.$ %
Success% ,
(, -
_mapper- 4
.4 5
Map5 8
<8 9

ProductDto9 C
>C D
(D E
productE L
)L M
)M N
;N O
} 	
catch 
( 
	Exception 
e 
) 
{ 	
return   
Result   
<   

ProductDto   $
>  $ %
.  % &
Failure  & -
(  - .
ProductErrors  . ;
.  ; <
GetProductFailed  < L
(  L M
e  M N
.  N O
Message  O V
)  V W
)  W X
;  X Y
}!! 	
}"" 
}## Ω
ëC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\QueryHandler\GetAllWishlistItemsQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
QueryHandler  ,
{ 
public		 

class		 +
GetAllWishlistItemsQueryHandler		 0
:		1 2
IRequestHandler		3 B
<		B C$
GetAllWishlistItemsQuery		C [
,		[ \
List		] a
<		a b
WishlistItemDto		b q
>		q r
>		r s
{

 
private 
readonly #
IWishlistItemRepository 0

repository1 ;
;; <
private 
readonly 
IMapper  
mapper! '
;' (
public +
GetAllWishlistItemsQueryHandler .
(. /#
IWishlistItemRepository/ F

repositoryG Q
,Q R
IMapperS Z
mapper[ a
)a b
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
List 
< 
WishlistItemDto .
>. /
>/ 0
Handle1 7
(7 8$
GetAllWishlistItemsQuery8 P
requestQ X
,X Y
CancellationTokenZ k
cancellationTokenl }
)} ~
{ 	
var 
wishlistItems 
= 
await  %

repository& 0
.0 1
GetAllAsync1 <
(< =
)= >
;> ?
return 
mapper 
. 
Map 
< 
List "
<" #
WishlistItemDto# 2
>2 3
>3 4
(4 5
wishlistItems5 B
)B C
;C D
} 	
} 
} ›
ïC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\QueryHandler\GetAllShoppingCartItemsQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
QueryHandler  ,
{ 
public		 

class		 /
#GetAllShoppingCartItemsQueryHandler		 4
:		5 6
IRequestHandler		7 F
<		F G(
GetAllShoppingCartItemsQuery		G c
,		c d
List		e i
<		i j
ShoppingCartItemDto		j }
>		} ~
>		~ 
{

 
private 
readonly #
IShoppingCartRepository 0

repository1 ;
;; <
private 
readonly 
IMapper  
mapper! '
;' (
public /
#GetAllShoppingCartItemsQueryHandler 2
(2 3#
IShoppingCartRepository3 J

repositoryK U
,U V
IMapperW ^
mapper_ e
)e f
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
List 
< 
ShoppingCartItemDto 2
>2 3
>3 4
Handle5 ;
(; <(
GetAllShoppingCartItemsQuery< X
requestY `
,` a
CancellationTokenb s
cancellationToken	t Ö
)
Ö Ü
{ 	
var 
	cartItems 
= 
await  %

repository& 0
.0 1
GetAllItemsAsync1 A
(A B
)B C
;C D
return 
mapper 
. 
Map !
<! "
List" &
<& '
ShoppingCartItemDto' :
>: ;
>; <
(< =
	cartItems= F
)F G
;G H
} 	
} 
} Ä
ãC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\QueryHandler\GetAllProductQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
QueryHandler  ,
{ 
public		 

class		 &
GetAllProductsQueryHandler		 +
:		, -
IRequestHandler		. =
<		= >
GetAllProductsQuery		> Q
,		Q R
List		S W
<		W X

ProductDto		X b
>		b c
>		c d
{

 
private 
readonly 
IProductRepository +

repository, 6
;6 7
private 
readonly 
IMapper  
mapper! '
;' (
public &
GetAllProductsQueryHandler )
() *
IProductRepository* <

repository= G
,G H
IMapperI P
mapperQ W
)W X
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
List 
< 

ProductDto )
>) *
>* +
Handle, 2
(2 3
GetAllProductsQuery3 F
requestG N
,N O
CancellationTokenP a
cancellationTokenb s
)s t
{ 	
var 
products 
= 
await  

repository! +
.+ ,
GetAllAsync, 7
(7 8
)8 9
;9 :
return 
mapper 
. 
Map 
< 
List "
<" #

ProductDto# -
>- .
>. /
(/ 0
products0 8
)8 9
;9 :
} 	
} 
} ¨
ÖC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Queries\GetWishlistItemByIdQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Queries  '
;' (
public 
class $
GetWishlistItemByIdQuery %
:& '
	IdCommand( 1
,1 2
IRequest3 ;
<; <
Result< B
<B C
WishlistItemDtoC R
>R S
>S T
{ 
}		 ∑
âC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Queries\GetShoppingCartItemByIdQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Queries  '
{ 
public 

class (
GetShoppingCartItemByIdQuery -
:. /
IRequest0 8
<8 9
Result9 ?
<? @
ShoppingCartItemDto@ S
>S T
>T U
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
}

 
} ù
ÄC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Queries\GetProductByIdQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Queries  '
;' (
public 
class 
GetProductByIdQuery  
:! "
	IdCommand# ,
,, -
IRequest. 6
<6 7
Result7 =
<= >

ProductDto> H
>H I
>I J
{ 
}		 ï
ÖC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Queries\GetAllWishlistItemsQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Queries  '
{ 
public 

class $
GetAllWishlistItemsQuery )
:* +
IRequest, 4
<4 5
List5 9
<9 :
WishlistItemDto: I
>I J
>J K
{ 
} 
}		 π
âC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Queries\GetAllShoppingCartItemsQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Queries  '
{ 
public 

class (
GetAllShoppingCartItemsQuery -
:. /
IRequest0 8
<8 9
List9 =
<= >
ShoppingCartItemDto> Q
>Q R
>R S
{ 
public 
Guid 
CartId 
{ 
get  
;  !
set" %
;% &
}' (
}		 
}

 Ü
ÄC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Queries\GetAllProductsQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Queries  '
{ 
public 

class 
GetAllProductsQuery $
:% &
IRequest' /
</ 0
List0 4
<4 5

ProductDto5 ?
>? @
>@ A
{ 
} 
}		 »
íC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandValidators\UpdateWishlistItemValidator.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandValidators  1
{ 
public 

class .
"UpdateWishlistItemCommandValidator 3
:4 5
AbstractValidator6 G
<G H%
UpdateWishlistItemCommandH a
>a b
{ 
public .
"UpdateWishlistItemCommandValidator 1
(1 2
)2 3
{		 	
RuleFor

 
(

 
command

 
=>

 
command

 &
.

& '
Id

' )
)

) *
. 
NotEmpty 
( 
) 
. 
WithMessage 
( 
$str F
)F G
;G H
RuleFor 
( 
command 
=> 
command &
.& '

Product_Id' 1
)1 2
. 
NotEmpty 
( 
) 
. 
WithMessage 
( 
$str <
)< =
;= >
RuleFor 
( 
command 
=> 
command &
.& '
List_Id' .
). /
. 
NotEmpty 
( 
) 
. 
WithMessage 
( 
$str 9
)9 :
;: ;
} 	
} 
} ∞
ùC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandValidators\UpdateShoppingCartItemCommandValidator.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandValidators  1
{ 
public 

class 2
&UpdateShoppingCartItemCommandValidator 7
:8 9
AbstractValidator: K
<K L)
UpdateShoppingCartItemCommandL i
>i j
{ 
public 2
&UpdateShoppingCartItemCommandValidator 5
(5 6
)6 7
{		 	
RuleFor

 
(

 
command

 
=>

 
command

 &
.

& '
Id

' )
)

) *
. 
NotEmpty 
( 
) 
. 
WithMessage 
( 
$str K
)K L
;L M
RuleFor 
( 
command 
=> 
command &
.& '

Product_Id' 1
)1 2
. 
NotEmpty 
( 
) 
. 
WithMessage 
( 
$str <
)< =
;= >
RuleFor 
( 
command 
=> 
command &
.& '
Cart_Id' .
). /
. 
NotEmpty 
( 
) 
. 
WithMessage 
( 
$str 9
)9 :
;: ;
RuleFor 
( 
command 
=> 
command &
.& '
Quantity' /
)/ 0
. 
GreaterThan 
( 
$num 
) 
. 
WithMessage 
( 
$str >
)> ?
;? @
} 	
} 
} ™
îC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandValidators\UpdateProductCommandValidator.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandValidators  1
{ 
public 

class )
UpdateProductCommandValidator .
:/ 0
AbstractValidator1 B
<B C 
UpdateProductCommandC W
>W X
{ 
public )
UpdateProductCommandValidator ,
(, -
)- .
{		 	
RuleFor

 
(

 
p

 
=>

 
p

 
.

 
Title

  
)

  !
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num "
)" #
. 
WithMessage 
( 
$str n
)n o
;o p
RuleFor 
( 
p 
=> 
p 
. 
Category #
)# $
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num "
)" #
. 
WithMessage 
( 
$str q
)q r
;r s
RuleFor 
( 
p 
=> 
p 
. 
Description &
)& '
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num "
)" #
. 
WithMessage 
( 
$str t
)t u
;u v
} 	
} 
} ô

ôC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandValidators\CreateWishlistItemCommandValidator.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandValidators  1
{ 
public 

class .
"CreateWishlistItemCommandValidator 3
:4 5
AbstractValidator6 G
<G H%
CreateWishlistItemCommandH a
>a b
{ 
public .
"CreateWishlistItemCommandValidator 1
(1 2
)2 3
{		 	
RuleFor

 
(

 
command

 
=>

 
command

 &
.

& '

Product_Id

' 1
)

1 2
. 
NotEmpty 
( 
) 
. 
WithMessage 
( 
$str <
)< =
;= >
RuleFor 
( 
command 
=> 
command &
.& '
List_Id' .
). /
. 
NotEmpty 
( 
) 
. 
WithMessage 
( 
$str 9
)9 :
;: ;
} 	
} 
} ≤
ùC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandValidators\CreateShoppingCartItemCommandValidator.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandValidators  1
{ 
public 

class 2
&CreateShoppingCartItemCommandValidator 7
:8 9
AbstractValidator: K
<K L)
CreateShoppingCartItemCommandL i
>i j
{ 
public 2
&CreateShoppingCartItemCommandValidator 5
(5 6
)6 7
{		 	
RuleFor

 
(

 
p

 
=>

 
p

 
.

 
Cart_Id

 "
)

" #
.

# $
NotEmpty

$ ,
(

, -
)

- .
;

. /
RuleFor 
( 
p 
=> 
p 
. 

Product_Id %
)% &
.& '
NotEmpty' /
(/ 0
)0 1
;1 2
RuleFor 
( 
command 
=> 
command &
.& '
Quantity' /
)/ 0
.0 1
GreaterThan1 <
(< =
$num= >
)> ?
.? @
WithMessage@ K
(K L
$strL m
)m n
;n o
} 	
} 
} ™
îC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandValidators\CreateProductCommandValidator.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandValidators  1
{ 
public 

class )
CreateProductCommandValidator .
:/ 0
AbstractValidator1 B
<B C 
CreateProductCommandC W
>W X
{ 
public )
CreateProductCommandValidator ,
(, -
)- .
{		 	
RuleFor

 
(

 
p

 
=>

 
p

 
.

 
Title

  
)

  !
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num "
)" #
. 
WithMessage 
( 
$str n
)n o
;o p
RuleFor 
( 
p 
=> 
p 
. 
Category #
)# $
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num "
)" #
. 
WithMessage 
( 
$str q
)q r
;r s
RuleFor 
( 
p 
=> 
p 
. 
Description &
)& '
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num "
)" #
. 
WithMessage 
( 
$str t
)t u
;u v
} 	
} 
} ﬁ
áC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Commands\UpdateWishlistItemCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class %
UpdateWishlistItemCommand *
:+ ,)
CreateWishlistItemBaseCommand- J
,J K
IRequestL T
<T U
ResultU [
<[ \
Unit\ `
>` a
>a b
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
} 
}		 É
ãC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Commands\UpdateShoppingCartItemCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class )
UpdateShoppingCartItemCommand .
:/ 0-
!CreateShoppingCartItemBaseCommand1 R
,R S
IRequestT \
<\ ]
Result] c
<c d
Unitd h
>h i
>i j
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
int 
Quantity 
{ 
get !
;! "
set# &
;& '
}( )
}		 
}

 œ
ÇC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Commands\UpdateProductCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class  
UpdateProductCommand %
:& '$
CreateProductCommandBase( @
,@ A
IRequestB J
<J K
ResultK Q
<Q R
UnitR V
>V W
>W X
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
} 
}		 í
wC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Commands\IdCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

abstract 
class 
	IdCommand #
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
} 
} ∂
áC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Commands\DeleteWishlistItemCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class %
DeleteWishlistItemCommand *
:+ ,
	IdCommand- 6
,6 7
IRequest8 @
<@ A
ResultA G
<G H
UnitH L
>L M
>M N
{ 
} 
} æ
ãC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Commands\DeleteShoppingCartItemCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class )
DeleteShoppingCartItemCommand .
:/ 0
	IdCommand1 :
,: ;
IRequest< D
<D E
ResultE K
<K L
UnitL P
>P Q
>Q R
{ 
} 
} ¨
ÇC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Commands\DeleteProductCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class  
DeleteProductCommand %
:& '
	IdCommand( 1
,1 2
IRequest3 ;
<; <
Result< B
<B C
UnitC G
>G H
>H I
{ 
} 
}  
áC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Commands\CreateWishlistItemCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class %
CreateWishlistItemCommand *
:+ ,)
CreateWishlistItemBaseCommand- J
,J K
IRequestL T
<T U
ResultU [
<[ \
Guid\ `
>` a
>a b
{ 
} 
} ∆
ãC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Commands\CreateWishlistItemBaseCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class )
CreateWishlistItemBaseCommand .
{ 
public 
Guid 

Product_Id 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
Guid 
List_Id 
{ 
get !
;! "
set# &
;& '
}( )
} 
} ÷
ãC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Commands\CreateShoppingCartItemCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class )
CreateShoppingCartItemCommand .
:/ 0-
!CreateShoppingCartItemBaseCommand1 R
,R S
IRequestT \
<\ ]
Result] c
<c d
Guidd h
>h i
>i j
{ 
} 
}		 Á
èC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Commands\CreateShoppingCartItemBaseCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class -
!CreateShoppingCartItemBaseCommand 2
{ 
public 
Guid 

Product_Id 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
Guid 
Cart_Id 
{ 
get !
;! "
set# &
;& '
}( )
public 
int 
Quantity 
{ 
get !
;! "
set# &
;& '
}( )
} 
}		 ì	
ÜC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Commands\CreateProductCommandBase.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class $
CreateProductCommandBase )
{ 
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Category 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Description !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
decimal 
Price 
{ 
get "
;" #
set$ '
;' (
}) *
public		 
bool		 
IsNegotiable		  
{		! "
get		# &
;		& '
set		( +
;		+ ,
}		- .
}

 
} ª
ÇC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\Commands\CreateProductCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class  
CreateProductCommand %
:& '$
CreateProductCommandBase( @
,@ A
IRequestB J
<J K
ResultK Q
<Q R
GuidR V
>V W
>W X
{ 
} 
}		 Ç
ïC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandHandlers\UpdateWishlistItemCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandHandlers  /
{		 
public

 

class

 ,
 UpdateWishlistItemCommandHandler

 1
:

2 3
IRequestHandler

4 C
<

C D%
UpdateWishlistItemCommand

D ]
,

] ^
Result

_ e
<

e f
Unit

f j
>

j k
>

k l
{ 
private 
readonly #
IWishlistItemRepository 0

repository1 ;
;; <
private 
readonly 
IMapper  
mapper! '
;' (
public ,
 UpdateWishlistItemCommandHandler /
(/ 0#
IWishlistItemRepository0 G

repositoryH R
,R S
IMapperT [
mapper\ b
)b c
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
Unit! %
>% &
>& '
Handle( .
(. /%
UpdateWishlistItemCommand/ H
requestI P
,P Q
CancellationTokenR c
cancellationTokend u
)u v
{ 	
var 
wishlistItem 
= 
mapper %
.% &
Map& )
<) *
WishlistItem* 6
>6 7
(7 8
request8 ?
)? @
;@ A
try 
{ 
if 
( 
wishlistItem  
is! #
null$ (
)( )
{ 
return 
Result !
<! "
Unit" &
>& '
.' (
Failure( /
(/ 0
WishlistItemErrors0 B
.B C
ValidationFailedC S
(S T
$strT i
)i j
)j k
;k l
} 
await 

repository  
.  !
UpdateAsync! ,
(, -
wishlistItem- 9
)9 :
;: ;
return 
Result 
< 
Unit "
>" #
.# $
Success$ +
(+ ,
Unit, 0
.0 1
Value1 6
)6 7
;7 8
}   
catch!! 
(!! 
	Exception!! 
ex!! 
)!!  
{"" 
return## 
Result## 
<## 
Unit## "
>##" #
.### $
Failure##$ +
(##+ ,
WishlistItemErrors##, >
.##> ?$
UpdateWishlistItemFailed##? W
(##W X
ex##X Z
.##Z [
Message##[ b
)##b c
)##c d
;##d e
}$$ 
}%% 	
}&& 
}'' ñ
ôC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandHandlers\UpdateShoppingCartItemCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandHandlers  /
{ 
public		 

class		 0
$UpdateShoppingCartItemCommandHandler		 5
:		6 7
IRequestHandler		8 G
<		G H)
UpdateShoppingCartItemCommand		H e
,		e f
Result		g m
<		m n
Unit		n r
>		r s
>		s t
{

 
private 
readonly #
IShoppingCartRepository 0"
shoppingCartRepository1 G
;G H
private 
readonly 
IMapper  
mapper! '
;' (
public 0
$UpdateShoppingCartItemCommandHandler 3
(3 4#
IShoppingCartRepository4 K"
shoppingCartRepositoryL b
,b c
IMapperd k
mapperl r
)r s
{ 	
this 
. "
shoppingCartRepository '
=( )"
shoppingCartRepository* @
;@ A
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
Unit! %
>% &
>& '
Handle( .
(. /)
UpdateShoppingCartItemCommand/ L
requestM T
,T U
CancellationTokenV g
cancellationTokenh y
)y z
{ 	
try 
{ 
var 
cartItem 
= 
await $"
shoppingCartRepository% ;
.; <
GetItemByIdAsync< L
(L M
requestM T
.T U
IdU W
)W X
;X Y
if 
( 
cartItem 
is 
null  $
)$ %
{ 
return 
Result !
<! "
Unit" &
>& '
.' (
Failure( /
(/ 0"
ShoppingCartItemErrors0 F
.F G
NotFoundG O
(O P
requestP W
.W X
IdX Z
)Z [
)[ \
;\ ]
} 
mapper 
. 
Map 
( 
request "
," #
cartItem$ ,
), -
;- .
await   "
shoppingCartRepository   ,
.  , -
UpdateItemAsync  - <
(  < =
cartItem  = E
)  E F
;  F G
return!! 
Result!! 
<!! 
Unit!! "
>!!" #
.!!# $
Success!!$ +
(!!+ ,
Unit!!, 0
.!!0 1
Value!!1 6
)!!6 7
;!!7 8
}"" 
catch## 
(## 
	Exception## 
ex## 
)##  
{$$ 
return%% 
Result%% 
<%% 
Unit%% "
>%%" #
.%%# $
Failure%%$ +
(%%+ ,"
ShoppingCartItemErrors%%, B
.%%B C
UpdateItemFailed%%C S
(%%S T
ex%%T V
.%%V W
Message%%W ^
)%%^ _
)%%_ `
;%%` a
}&& 
}'' 	
}(( 
})) º
êC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandHandlers\UpdateProductCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandHandlers  /
{		 
public

 

class

 '
UpdateProductCommandHandler

 ,
:

- .
IRequestHandler

/ >
<

> ? 
UpdateProductCommand

? S
,

S T
Result

U [
<

[ \
Unit

\ `
>

` a
>

a b
{ 
private 
readonly 
IProductRepository +

repository, 6
;6 7
private 
readonly 
IMapper  
mapper! '
;' (
public '
UpdateProductCommandHandler *
(* +
IProductRepository+ =

repository> H
,H I
IMapperJ Q
mapperR X
)X Y
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
Unit! %
>% &
>& '
Handle( .
(. / 
UpdateProductCommand/ C
requestD K
,K L
CancellationTokenM ^
cancellationToken_ p
)p q
{ 	
var 
product 
= 
mapper  
.  !
Map! $
<$ %
Product% ,
>, -
(- .
request. 5
)5 6
;6 7
try 
{ 
if 
( 
product 
is 
null  $
)$ %
{ 
return 
Result !
<! "
Unit" &
>& '
.' (
Failure( /
(/ 0
ProductErrors0 =
.= >
ValidationFailed> N
(N O
$strO d
)d e
)e f
;f g
} 
await 

repository  
.  !
UpdateAsync! ,
(, -
product- 4
)4 5
;5 6
return 
Result 
< 
Unit "
>" #
.# $
Success$ +
(+ ,
Unit, 0
.0 1
Value1 6
)6 7
;7 8
}   
catch!! 
(!! 
	Exception!! 
ex!! 
)!!  
{"" 
return## 
Result## 
<## 
Unit## "
>##" #
.### $
Failure##$ +
(##+ ,
ProductErrors##, 9
.##9 :
UpdateProductFailed##: M
(##M N
ex##N P
.##P Q
Message##Q X
)##X Y
)##Y Z
;##Z [
}$$ 
}%% 	
}&& 
}'' Ü
ïC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandHandlers\DeleteWishlistItemCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandHandlers  /
{ 
public 

class ,
 DeleteWishlistItemCommandHandler 1
:2 3
IRequestHandler4 C
<C D%
DeleteWishlistItemCommandD ]
,] ^
Result_ e
<e f
Unitf j
>j k
>k l
{		 
private

 
readonly

 #
IWishlistItemRepository

 0"
wishlistItemRepository

1 G
;

G H
public ,
 DeleteWishlistItemCommandHandler /
(/ 0#
IWishlistItemRepository0 G"
wishlistItemRepositoryH ^
)^ _
{ 	
this 
. "
wishlistItemRepository '
=( )"
wishlistItemRepository* @
;@ A
} 	
public 
async 
Task 
< 
Result  
<  !
Unit! %
>% &
>& '
Handle( .
(. /%
DeleteWishlistItemCommand/ H
requestI P
,P Q
CancellationTokenR c
cancellationTokend u
)u v
{ 	
try 
{ 
var 
wishlistItem  
=! "
await# ("
wishlistItemRepository) ?
.? @
GetByIdAsync@ L
(L M
requestM T
.T U
IdU W
)W X
;X Y
if 
( 
wishlistItem  
is! #
null$ (
)( )
{ 
return 
Result !
<! "
Unit" &
>& '
.' (
Failure( /
(/ 0
WishlistItemErrors0 B
.B C
NotFoundC K
(K L
requestL S
.S T
IdT V
)V W
)W X
;X Y
} 
await "
wishlistItemRepository ,
., -
DeleteAsync- 8
(8 9
wishlistItem9 E
.E F
IdF H
)H I
;I J
return 
Result 
< 
Unit "
>" #
.# $
Success$ +
(+ ,
Unit, 0
.0 1
Value1 6
)6 7
;7 8
} 
catch 
( 
	Exception 
e 
) 
{ 
return 
Result 
< 
Unit "
>" #
.# $
Failure$ +
(+ ,
WishlistItemErrors, >
.> ?$
DeleteWishlistItemFailed? W
(W X
eX Y
.Y Z
MessageZ a
)a b
)b c
;c d
} 
} 	
}   
}!! ñ
ôC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandHandlers\DeleteShoppingCartItemCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandHandlers  /
{ 
public 

class 0
$DeleteShoppingCartItemCommandHandler 5
:6 7
IRequestHandler8 G
<G H)
DeleteShoppingCartItemCommandH e
,e f
Resultg m
<m n
Unitn r
>r s
>s t
{		 
private

 
readonly

 #
IShoppingCartRepository

 0"
shoppingCartRepository

1 G
;

G H
public 0
$DeleteShoppingCartItemCommandHandler 3
(3 4#
IShoppingCartRepository4 K"
shoppingCartRepositoryL b
)b c
{ 	
this 
. "
shoppingCartRepository '
=( )"
shoppingCartRepository* @
;@ A
} 	
public 
async 
Task 
< 
Result  
<  !
Unit! %
>% &
>& '
Handle( .
(. /)
DeleteShoppingCartItemCommand/ L
requestM T
,T U
CancellationTokenV g
cancellationTokenh y
)y z
{ 	
try 
{ 
var 
cartItem 
= 
await $"
shoppingCartRepository% ;
.; <
GetItemByIdAsync< L
(L M
requestM T
.T U
IdU W
)W X
;X Y
if 
( 
cartItem 
is 
null  $
)$ %
{ 
return 
Result !
<! "
Unit" &
>& '
.' (
Failure( /
(/ 0"
ShoppingCartItemErrors0 F
.F G
NotFoundG O
(O P
requestP W
.W X
IdX Z
)Z [
)[ \
;\ ]
} 
await "
shoppingCartRepository ,
., -
RemoveItemAsync- <
(< =
cartItem= E
.E F
IdF H
)H I
;I J
return 
Result 
< 
Unit "
>" #
.# $
Success$ +
(+ ,
Unit, 0
.0 1
Value1 6
)6 7
;7 8
} 
catch 
( 
	Exception 
e 
) 
{ 
return 
Result 
< 
Unit "
>" #
.# $
Failure$ +
(+ ,"
ShoppingCartItemErrors, B
.B C
DeleteItemFailedC S
(S T
eT U
.U V
MessageV ]
)] ^
)^ _
;_ `
}   
}!! 	
}"" 
}## ß
êC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandHandlers\DeleteProductCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandHandlers  /
{ 
public 

class '
DeleteProductCommandHandler ,
:- .
IRequestHandler/ >
<> ? 
DeleteProductCommand? S
,S T
ResultU [
<[ \
Unit\ `
>` a
>a b
{		 
private

 
readonly

 
IProductRepository

 +
productRepository

, =
;

= >
public '
DeleteProductCommandHandler *
(* +
IProductRepository+ =
productRepository> O
)O P
{ 	
this 
. 
productRepository "
=# $
productRepository% 6
;6 7
} 	
public 
async 
Task 
< 
Result  
<  !
Unit! %
>% &
>& '
Handle( .
(. / 
DeleteProductCommand/ C
requestD K
,K L
CancellationTokenM ^
cancellationToken_ p
)p q
{ 	
try 
{ 
var 
product 
= 
await #
productRepository$ 5
.5 6
GetByIdAsync6 B
(B C
requestC J
.J K
IdK M
)M N
;N O
if 
( 
product 
is 
null #
)# $
{ 
return 
Result !
<! "
Unit" &
>& '
.' (
Failure( /
(/ 0
ProductErrors0 =
.= >
NotFound> F
(F G
requestG N
.N O
IdO Q
)Q R
)R S
;S T
} 
await 
productRepository '
.' (
DeleteAsync( 3
(3 4
product4 ;
.; <
Id< >
)> ?
;? @
return 
Result 
< 
Unit "
>" #
.# $
Success$ +
(+ ,
Unit, 0
.0 1
Value1 6
)6 7
;7 8
} 
catch 
( 
	Exception 
e 
) 
{ 
return 
Result 
< 
Unit "
>" #
.# $
Failure$ +
(+ ,
ProductErrors, 9
.9 :
DeleteProductFailed: M
(M N
eN O
.O P
MessageP W
)W X
)X Y
;Y Z
} 
} 	
}   
}!! ˚
ïC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandHandlers\CreateWishlistItemCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandHandlers  /
{		 
public

 

class

 ,
 CreateWishlistItemCommandHandler

 1
:

2 3
IRequestHandler

4 C
<

C D%
CreateWishlistItemCommand

D ]
,

] ^
Result

_ e
<

e f
Guid

f j
>

j k
>

k l
{ 
private 
readonly #
IWishlistItemRepository 0

repository1 ;
;; <
private 
readonly 
IMapper  
_mapper! (
;( )
public ,
 CreateWishlistItemCommandHandler /
(/ 0#
IWishlistItemRepository0 G

repositoryH R
,R S
IMapperT [
mapper\ b
)b c
{ 	
this 
. 

repository 
= 

repository (
;( )
_mapper 
= 
mapper 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /%
CreateWishlistItemCommand/ H
requestI P
,P Q
CancellationTokenR c
cancellationTokend u
)u v
{ 	
var 
wishlistItem 
= 
_mapper &
.& '
Map' *
<* +
WishlistItem+ 7
>7 8
(8 9
request9 @
)@ A
;A B
if 
( 
wishlistItem 
is 
null  $
)$ %
{ 
return 
Result 
< 
Guid "
>" #
.# $
Failure$ +
(+ ,
WishlistItemErrors, >
.> ?
ValidationFailed? O
(O P
$strP k
)k l
)l m
;m n
} 
try 
{ 
var 

returnedId 
=  
await! &

repository' 1
.1 2
AddAsync2 :
(: ;
wishlistItem; G
)G H
;H I
return 
Result 
< 
Guid "
>" #
.# $
Success$ +
(+ ,

returnedId, 6
)6 7
;7 8
}   
catch!! 
(!! 
	Exception!! 
e!! 
)!! 
{"" 
return## 
Result## 
<## 
Guid## "
>##" #
.### $
Failure##$ +
(##+ ,
WishlistItemErrors##, >
.##> ?$
CreateWishlistItemFailed##? W
(##W X
e##X Y
.##Y Z
Message##Z a
)##a b
)##b c
;##c d
}$$ 
}%% 	
}&& 
}'' ã
ôC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandHandlers\CreateShoppingCartItemCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandHandlers  /
{		 
public

 

class

 0
$CreateShoppingCartItemCommandHandler

 5
:

6 7
IRequestHandler

8 G
<

G H)
CreateShoppingCartItemCommand

H e
,

e f
Result

g m
<

m n
Guid

n r
>

r s
>

s t
{ 
private 
readonly #
IShoppingCartRepository 0

repository1 ;
;; <
private 
readonly 
IMapper  
_mapper! (
;( )
public 0
$CreateShoppingCartItemCommandHandler 3
(3 4#
IShoppingCartRepository4 K

repositoryL V
,V W
IMapperX _
mapper` f
)f g
{ 	
this 
. 

repository 
= 

repository (
;( )
_mapper 
= 
mapper 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /)
CreateShoppingCartItemCommand/ L
requestM T
,T U
CancellationTokenV g
cancellationTokenh y
)y z
{ 	
var 
cartItem 
= 
_mapper "
." #
Map# &
<& '
ShoppingCartItem' 7
>7 8
(8 9
request9 @
)@ A
;A B
if 
( 
cartItem 
is 
null  
)  !
{ 
return 
Result 
< 
Guid "
>" #
.# $
Failure$ +
(+ ,"
ShoppingCartItemErrors, B
.B C
ValidationFailedC S
(S T
$strT k
)k l
)l m
;m n
} 
try 
{ 
var 

returnedId 
=  
await! &

repository' 1
.1 2
AddItemAsync2 >
(> ?
cartItem? G
)G H
;H I
return 
Result 
< 
Guid "
>" #
.# $
Success$ +
(+ ,

returnedId, 6
)6 7
;7 8
}   
catch!! 
(!! 
	Exception!! 
e!! 
)!! 
{"" 
return## 
Result## 
<## 
Guid## "
>##" #
.### $
Failure##$ +
(##+ ,"
ShoppingCartItemErrors##, B
.##B C
CreateItemFailed##C S
(##S T
e##T U
.##U V
Message##V ]
)##] ^
)##^ _
;##_ `
}$$ 
}%% 	
}&& 
}'' µ
êC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Use Cases\CommandHandlers\CreateProductCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandHandlers  /
{		 
public

 

class

 '
CreateProductCommandHandler

 ,
:

- .
IRequestHandler

/ >
<

> ? 
CreateProductCommand

? S
,

S T
Result

U [
<

[ \
Guid

\ `
>

` a
>

a b
{ 
private 
readonly 
IProductRepository +

repository, 6
;6 7
private 
readonly 
IMapper  
_mapper! (
;( )
public '
CreateProductCommandHandler *
(* +
IProductRepository+ =

repository> H
,H I
IMapperJ Q
mapperR X
)X Y
{ 	
this 
. 

repository 
= 

repository (
;( )
_mapper 
= 
mapper 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. / 
CreateProductCommand/ C
requestD K
,K L
CancellationTokenM ^
cancellationToken_ p
)p q
{ 	
var 
product 
= 
_mapper !
.! "
Map" %
<% &
Product& -
>- .
(. /
request/ 6
)6 7
;7 8
if 
( 
product 
is 
null 
)  
{ 
return 
Result 
< 
Guid "
>" #
.# $
Failure$ +
(+ ,
ProductErrors, 9
.9 :
ValidationFailed: J
(J K
$strK `
)` a
)a b
;b c
} 
try 
{ 
var 

returnedId 
=  
await! &

repository' 1
.1 2
AddAsync2 :
(: ;
product; B
)B C
;C D
return   
Result   
<   
Guid   "
>  " #
.  # $
Success  $ +
(  + ,

returnedId  , 6
)  6 7
;  7 8
}!! 
catch"" 
("" 
	Exception"" 
e"" 
)"" 
{## 
return$$ 
Result$$ 
<$$ 
Guid$$ "
>$$" #
.$$# $
Failure$$$ +
($$+ ,
ProductErrors$$, 9
.$$9 :
CreateProductFailed$$: M
($$M N
e$$N O
.$$O P
Message$$P W
)$$W X
)$$X Y
;$$Y Z
}%% 
}&& 	
}'' 
}(( ˙
aC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Result.cs
	namespace 	
Application
 
; 
public 
class 
Result 
< 
T 
> 
{ 
private 
Result 
( 
T 
value 
) 
{ 
Value 
= 
value 
; 
Error 
= 
null 
; 
} 
private

 
Result

 
(

 
Error

 
error

 
)

 
{ 
Error 
= 
error 
; 
Value 
= 
default 
; 
} 
public 

T 
? 
Value 
; 
public 

Error 
? 
Error 
; 
public 

bool 
	IsSuccess 
=> 
Error "
is# %
null& *
;* +
public 

static 
Result 
< 
T 
> 
Success #
(# $
T$ %
value& +
)+ ,
=>- /
new0 3
(3 4
value4 9
)9 :
;: ;
public 

static 
Result 
< 
T 
> 
Failure #
(# $
Error$ )
error* /
)/ 0
=>1 3
new4 7
(7 8
error8 =
)= >
;> ?
public 

TResult 
Match 
< 
TResult  
>  !
(! "
Func" &
<& '
T' (
,( )
TResult* 1
>1 2
	onSuccess3 <
,< =
Func> B
<B C
ErrorC H
,H I
TResultJ Q
>Q R
	onFailureS \
)\ ]
{ 
return 
	IsSuccess 
? 
	onSuccess $
($ %
Value% *
!* +
)+ ,
:- .
	onFailure/ 8
(8 9
Error9 >
!> ?
)? @
;@ A
} 
} ª
tC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Errors\WishlistItemErrors.cs
	namespace 	
Application
 
. 
Errors 
; 
public 
static 
class 
WishlistItemErrors &
{ 
public 

static 
Error 
ValidationFailed (
(( )
string) /
Description0 ;
); <
=>= ?
new@ C
ErrorD I
(I J
$strJ i
,i j
Descriptionk v
)v w
;w x
public 

static 
Error 
NotFound  
(  !
Guid! %
guid& *
)* +
=>, .
new 
Error 
( 
$str )
,) *
$"+ -
$str- H
{H I
guidI M
}M N
$strN ]
"] ^
)^ _
;_ `
public

 

static

 
Error

 
WishlistItemExists

 *
(

* +
Guid

+ /
guid

0 4
)

4 5
=>

6 8
new 
Error 
( 
$str 3
,3 4
$"5 7
$str7 R
{R S
guidS W
}W X
$strX h
"h i
)i j
;j k
public 

static 
Error $
CreateWishlistItemFailed 0
(0 1
string1 7
description8 C
)C D
=>E G
new 
Error 
( 
$str 9
,9 :
description; F
)F G
;G H
public 

static 
Error !
GetWishlistItemFailed -
(- .
string. 4
description5 @
)@ A
=>B D
new 
Error 
( 
$str 6
,6 7
description8 C
)C D
;D E
public 

static 
Error $
DeleteWishlistItemFailed 0
(0 1
string1 7
description8 C
)C D
=>E G
new 
Error 
( 
$str 9
,9 :
description; F
)F G
;G H
public 

static 
Error $
UpdateWishlistItemFailed 0
(0 1
string1 7
description8 C
)C D
=>E G
new 
Error 
( 
$str 9
,9 :
description; F
)F G
;G H
} Ä
xC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Errors\ShoppingCartItemErrors.cs
	namespace 	
Application
 
. 
Errors 
; 
public 

static 
class "
ShoppingCartItemErrors .
{ 
public 
static 
Error 
ValidationFailed ,
(, -
string- 3
Description4 ?
)? @
=>A C
newD G
ErrorH M
(M N
$strN q
,q r
Descriptions ~
)~ 
;	 Ä
public 
static 
Error 
NotFound $
($ %
Guid% )
id* ,
), -
{ 	
return 
new 
Error 
( 
$str 7
,7 8
$"9 ;
$str; V
{V W
idW Y
}Y Z
$strZ e
"e f
)f g
;g h
}		 	
public 
static 
Error 
DeleteItemFailed ,
(, -
string- 3
message4 ;
); <
{ 	
return 
new 
Error 
( 
$str /
,/ 0
$"1 3
$str3 X
{X Y
messageY `
}` a
"a b
)b c
;c d
} 	
public 
static 
Error 
UpdateItemFailed ,
(, -
string- 3
message4 ;
); <
{ 	
return 
new 
Error 
( 
$str /
,/ 0
$"1 3
$str3 X
{X Y
messageY `
}` a
"a b
)b c
;c d
} 	
public 
static 
Error 
CreateItemFailed ,
(, -
string- 3
message4 ;
); <
{ 	
return 
new 
Error 
( 
$str /
,/ 0
$"1 3
$str3 X
{X Y
messageY `
}` a
"a b
)b c
;c d
} 	
public 

static 
Error 
GetItemsFailed &
(& '
string' -
message. 5
)5 6
{ 
return 
new 
Error 
( 
$str )
,) *
$"+ -
$str- U
{U V
messageV ]
}] ^
"^ _
)_ `
;` a
} 
public 

static 
Error 
GetItemFailed %
(% &
string& ,
message- 4
)4 5
{ 	
return 
new 
Error 
( 
$str ,
,, -
$". 0
$str0 W
{W X
messageX _
}_ `
"` a
)a b
;b c
} 	
}   ò
oC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Errors\ProductErrors.cs
	namespace 	
Application
 
. 
Errors 
; 
public 
static 
class 
ProductErrors !
{ 
public 

static 
Error 
ValidationFailed (
(( )
string) /
Description0 ;
); <
=>= ?
new@ C
ErrorD I
(I J
$strJ d
,d e
Descriptionf q
)q r
;r s
public 

static 
Error 
NotFound  
(  !
Guid! %
guid& *
)* +
=>, .
new 
Error 
( 
$str $
,$ %
$"& (
$str( =
{= >
guid> B
}B C
$strC R
"R S
)S T
;T U
public		 

static		 
Error		 
ProductExists		 %
(		% &
Guid		& *
guid		+ /
)		/ 0
=>		1 3
new

 
Error

 
(

 
$str

 )
,

) *
$"

+ -
$str

- B
{

B C
guid

C G
}

G H
$str

H X
"

X Y
)

Y Z
;

Z [
public 

static 
Error 
CreateProductFailed +
(+ ,
string, 2
Description3 >
)> ?
=>@ B
new 
Error 
( 
$str /
,/ 0
Description1 <
)< =
;= >
public 

static 
Error 
GetProductFailed (
(( )
string) /
Description0 ;
); <
=>= ?
new 
Error 
( 
$str ,
,, -
Description. 9
)9 :
;: ;
public 

static 
Error 
DeleteProductFailed +
(+ ,
string, 2
Description3 >
)> ?
=>@ B
new 
Error 
( 
$str /
,/ 0
Description1 <
)< =
;= >
public 

static 
Error 
UpdateProductFailed +
(+ ,
string, 2
Description3 >
)> ?
=>@ B
new 
Error 
( 
$str /
,/ 0
Description1 <
)< =
;= >
} ˘
`C:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\Error.cs
	namespace 	
Application
 
; 
public 
sealed 
record 
Error 
( 
string !
Code" &
,& '
string( .
?. /
Description0 ;
=< =
null> B
)B C
{ 
public 

static 
readonly 
Error  
None! %
=& '
new( +
(+ ,
string, 2
.2 3
Empty3 8
)8 9
;9 :
public 

override 
string 
ToString #
(# $
)$ %
{ 
return 
$" 
$str 
{ 
Code 
} 
$str 0
{0 1
Description1 <
}< =
"= >
;> ?
}		 
}

 Ö
oC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\DTOs\WishlistItemDTO.cs
	namespace 	
Application
 
. 
DTOs 
{ 
public 

class 
WishlistItemDto  
:! "
CartListItemBaseDto# 6
{ 
public 
Guid 
List_Id 
{ 
get !
;! "
set# &
;& '
}( )
} 
} ¶
sC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\DTOs\ShoppingCartItemDto.cs
	namespace 	
Application
 
. 
DTOs 
{ 
public 

class 
ShoppingCartItemDto $
:% &
CartListItemBaseDto' :
{ 
public 
Guid 
Cart_Id 
{ 
get !
;! "
set# &
;& '
}( )
public 
int 
Quantity 
{ 
get !
;! "
set# &
;& '
}( )
} 
}		 “	
jC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\DTOs\ProductDto.cs
	namespace 	
Application
 
. 
DTOs 
{ 
public 

class 

ProductDto 
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
} 
} ï
iC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\DTOs\ClientDto.cs
	namespace 	
Application
 
. 
DTOs 
{ 
public 

class 
	ClientDto 
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
}+ ,
}		 
}

 Ù
sC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\DTOs\CartListItemBaseDto.cs
	namespace 	
Application
 
. 
DTOs 
{ 
public 

class 
CartListItemBaseDto $
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
}+ ,
} 
}		 º
nC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\Application\DependencyInjection.cs
	namespace 	
Application
 
{		 
public

 

static

 
class

 
DependencyInjection

 +
{ 
public 
static 
IServiceCollection (
AddApplication) 7
(7 8
this8 <
IServiceCollection= O
servicesP X
)X Y
{ 	
services 
. 

AddMediatR 
(  
cfg  #
=>$ &
cfg' *
.* +(
RegisterServicesFromAssembly+ G
(G H
AssemblyH P
.P Q 
GetExecutingAssemblyQ e
(e f
)f g
)g h
)h i
;i j
services 
. 
AddAutoMapper "
(" #
typeof# )
() *
MappingProfile* 8
)8 9
)9 :
;: ;
services 
. %
AddValidatorsFromAssembly .
(. /
Assembly/ 7
.7 8 
GetExecutingAssembly8 L
(L M
)M N
)N O
;O P
services 
. 
AddTransient !
(! "
typeof" (
(( )
IPipelineBehavior) :
<: ;
,; <
>< =
)= >
,> ?
typeof@ F
(F G
ValidationBehaviorG Y
<Y Z
,Z [
>[ \
)\ ]
)] ^
;^ _
return 
services 
; 
} 	
} 
} 