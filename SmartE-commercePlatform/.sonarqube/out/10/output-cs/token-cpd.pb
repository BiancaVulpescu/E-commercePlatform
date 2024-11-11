Ç
nC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\SmartE-commercePlatform\Program.cs
	namespace 	#
SmartE_commercePlatform
 !
{ 
public 

class 
Program 
{ 
static 
void 
Main 
( 
string 
[  
]  !
args" &
)& '
{		 	
var

 
builder

 
=

 
WebApplication

 (
.

( )
CreateBuilder

) 6
(

6 7
args

7 ;
)

; <
;

< =
builder 
. 
Services 
. 
AddApplication +
(+ ,
), -
;- .
builder 
. 
Services 
. 
AddInfrastructure .
(. /
builder/ 6
.6 7
Configuration7 D
)D E
;E F
builder 
. 
Services 
. 
AddControllers +
(+ ,
), -
;- .
builder 
. 
Services 
. #
AddEndpointsApiExplorer 4
(4 5
)5 6
;6 7
builder 
. 
Services 
. 
AddSwaggerGen *
(* +
)+ ,
;, -
var 
app 
= 
builder 
. 
Build #
(# $
)$ %
;% &
if 
( 
app 
. 
Environment 
.  
IsDevelopment  -
(- .
). /
)/ 0
{ 
app 
. 

UseSwagger 
( 
)  
;  !
app 
. 
UseSwaggerUI  
(  !
)! "
;" #
} 
app 
. 
UseHttpsRedirection #
(# $
)$ %
;% &
app!! 
.!! 
UseAuthorization!!  
(!!  !
)!!! "
;!!" #
app## 
.## 
MapControllers## 
(## 
)##  
;##  !
app%% 
.%% 
Run%% 
(%% 
)%% 
;%% 
}&& 	
}'' 
}(( Û3
äC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\SmartE-commercePlatform\Controllers\WishlistItemsController.cs
	namespace 	#
SmartE_commercePlatform
 !
.! "
Controllers" -
{ 
[ 
Route 

(
 
$str  
)  !
]! "
[		 
ApiController		 
]		 
public

 

class

 #
WishlistItemsController

 (
:

) *
ControllerBase

+ 9
{ 
private 
readonly 
	IMediator "
mediator# +
;+ ,
public #
WishlistItemsController &
(& '
	IMediator' 0
mediator1 9
)9 :
{ 	
this 
. 
mediator 
= 
mediator $
;$ %
} 	
[ 	
HttpPost	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
CreateWishlistItem) ;
(; <%
CreateWishlistItemCommand< U%
createWishlistItemCommandV o
)o p
{ 	
var 
resultObject 
= 
await $
mediator% -
.- .
Send. 2
(2 3%
createWishlistItemCommand3 L
)L M
;M N
return 
resultObject 
.  
Match  %
<% &
IActionResult& 3
>3 4
(4 5
	onSuccess 
: 
result !
=>" $
CreatedAtAction% 4
(4 5
nameof5 ;
(; <
GetWishlistItemById< O
)O P
,P Q
newR U
{V W
idX Z
=[ \
result] c
}d e
,e f
resultg m
)m n
,n o
	onFailure 
: 
error  
=>! #

BadRequest$ .
(. /
error/ 4
)4 5
) 
; 
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetWishlistItemById) <
(< =
[= >
	FromRoute> G
]G H
GuidI M
idN P
)P Q
{ 	
var 
resultObject 
= 
await $
mediator% -
.- .
Send. 2
(2 3
new3 6$
GetWishlistItemByIdQuery7 O
{P Q
IdR T
=U V
idW Y
}Z [
)[ \
;\ ]
return   
resultObject   
.    
Match    %
<  % &
IActionResult  & 3
>  3 4
(  4 5
	onSuccess!! 
:!! 
result!! !
=>!!" $
Ok!!% '
(!!' (
result!!( .
)!!. /
,!!/ 0
	onFailure"" 
:"" 
error""  
=>""! #

BadRequest""$ .
("". /
error""/ 4
)""4 5
)## 
;## 
}$$ 	
[&& 	
HttpGet&&	 
]&& 
public'' 
async'' 
Task'' 
<'' 
IActionResult'' '
>''' (
GetAllWishlistItems'') <
(''< =
)''= >
{(( 	
var)) 
result)) 
=)) 
await)) 
mediator)) '
.))' (
Send))( ,
()), -
new))- 0$
GetAllWishlistItemsQuery))1 I
())I J
)))J K
)))K L
;))L M
return** 
Ok** 
(** 
result** 
)** 
;** 
}++ 	
[-- 	
HttpPut--	 
(-- 
$str-- 
)-- 
]-- 
public.. 
async.. 
Task.. 
<.. 
IActionResult.. '
>..' (
UpdateWishlistItem..) ;
(..; <
[..< =
FromBody..= E
]..E F%
UpdateWishlistItemCommand..G `%
updateWishlistItemCommand..a z
,..z {
[..| }
	FromRoute	..} Ü
]
..Ü á
Guid
..à å
id
..ç è
)
..è ê
{// 	
if00 
(00 %
updateWishlistItemCommand00 )
.00) *
Id00* ,
!=00- /
id000 2
)002 3
return11 

BadRequest11 !
(11! "
)11" #
;11# $
var22 
resultObject22 
=22 
await22 $
mediator22% -
.22- .
Send22. 2
(222 3%
updateWishlistItemCommand223 L
)22L M
;22M N
return33 
resultObject33 
.33  
Match33  %
<33% &
IActionResult33& 3
>333 4
(334 5
	onSuccess44 
:44 
result44 !
=>44" $
	NoContent44% .
(44. /
)44/ 0
,440 1
	onFailure55 
:55 
error55  
=>55! #

BadRequest55$ .
(55. /
error55/ 4
)554 5
)66 
;66 
}77 	
[99 	

HttpDelete99	 
(99 
$str99 
)99 
]99 
public:: 
async:: 
Task:: 
<:: 
IActionResult:: '
>::' (
DeleteWishlistItem::) ;
(::; <
[::< =
	FromRoute::= F
]::F G
Guid::H L
id::M O
)::O P
{;; 	
var<< 
resultObject<< 
=<< 
await<< $
mediator<<% -
.<<- .
Send<<. 2
(<<2 3
new<<3 6%
DeleteWishlistItemCommand<<7 P
{<<Q R
Id<<S U
=<<V W
id<<X Z
}<<[ \
)<<\ ]
;<<] ^
return== 
resultObject== 
.==  
Match==  %
<==% &
IActionResult==& 3
>==3 4
(==4 5
	onSuccess>> 
:>> 
result>> !
=>>>" $
	NoContent>>% .
(>>. /
)>>/ 0
,>>0 1
	onFailure?? 
:?? 
error??  
=>??! #

BadRequest??$ .
(??. /
error??/ 4
)??4 5
)@@ 
;@@ 
}AA 	
}BB 
}CC é5
éC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\SmartE-commercePlatform\Controllers\ShoppingCartItemsController.cs
	namespace 	#
SmartE_commercePlatform
 !
.! "
Controllers" -
{ 
[ 
Route 

(
 
$str  
)  !
]! "
[		 
ApiController		 
]		 
public

 

class

 '
ShoppingCartItemsController

 ,
:

- .
ControllerBase

/ =
{ 
private 
readonly 
	IMediator "
mediator# +
;+ ,
public '
ShoppingCartItemsController *
(* +
	IMediator+ 4
mediator5 =
)= >
{ 	
this 
. 
mediator 
= 
mediator $
;$ %
} 	
[ 	
HttpPost	 
] 
public 
async 
Task 
< 
IActionResult '
>' ("
CreateShoppingCartItem) ?
(? @
[@ A
FromBodyA I
]I J)
CreateShoppingCartItemCommandK h*
createShoppingCartItemCommand	i Ü
)
Ü á
{ 	
var 
resultObject 
= 
await $
mediator% -
.- .
Send. 2
(2 3)
createShoppingCartItemCommand3 P
)P Q
;Q R
return 
resultObject 
.  
Match  %
<% &
IActionResult& 3
>3 4
(4 5
	onSuccess 
: 
result !
=>" $
CreatedAtAction% 4
(4 5
nameof5 ;
(; <#
GetShoppingCartItemById< S
)S T
,T U
newV Y
{Z [
id\ ^
=_ `
resulta g
}h i
,i j
resultk q
)q r
,r s
	onFailure 
: 
error  
=>! #

BadRequest$ .
(. /
error/ 4
)4 5
) 
; 
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (#
GetShoppingCartItemById) @
(@ A
[A B
	FromRouteB K
]K L
GuidM Q
idR T
)T U
{   	
var!! 
resultObject!! 
=!! 
await!! $
mediator!!% -
.!!- .
Send!!. 2
(!!2 3
new!!3 6(
GetShoppingCartItemByIdQuery!!7 S
{!!T U
Id!!V X
=!!Y Z
id!![ ]
}!!^ _
)!!_ `
;!!` a
return"" 
resultObject"" 
.""  
Match""  %
<""% &
IActionResult""& 3
>""3 4
(""4 5
	onSuccess## 
:## 
result## !
=>##" $
Ok##% '
(##' (
result##( .
)##. /
,##/ 0
	onFailure$$ 
:$$ 
error$$  
=>$$! #

BadRequest$$$ .
($$. /
error$$/ 4
)$$4 5
)%% 
;%% 
}&& 	
[(( 	
HttpGet((	 
](( 
public)) 
async)) 
Task)) 
<)) 
IActionResult)) '
>))' (#
GetAllShoppingCartItems))) @
())@ A
)))A B
{** 	
var++ 
result++ 
=++ 
await++ 
mediator++ '
.++' (
Send++( ,
(++, -
new++- 0(
GetAllShoppingCartItemsQuery++1 M
(++M N
)++N O
)++O P
;++P Q
return,, 
Ok,, 
(,, 
result,, 
),, 
;,, 
}-- 	
[00 	
HttpPut00	 
(00 
$str00 
)00 
]00 
public11 
async11 
Task11 
<11 
IActionResult11 '
>11' ("
UpdateShoppingCartItem11) ?
(11? @
[11@ A
FromBody11A I
]11I J)
UpdateShoppingCartItemCommand11K h*
updateShoppingCartItemCommand	11i Ü
,
11Ü á
[
11à â
	FromRoute
11â í
]
11í ì
Guid
11î ò
id
11ô õ
)
11õ ú
{22 	
if33 
(33 )
updateShoppingCartItemCommand33 -
.33- .
Id33. 0
!=331 3
id334 6
)336 7
return44 

BadRequest44 !
(44! "
$str44" 0
)440 1
;441 2
var66 
resultObject66 
=66 
await66 $
mediator66% -
.66- .
Send66. 2
(662 3)
updateShoppingCartItemCommand663 P
)66P Q
;66Q R
return77 
resultObject77 
.77  
Match77  %
<77% &
IActionResult77& 3
>773 4
(774 5
	onSuccess88 
:88 
result88 !
=>88" $
	NoContent88% .
(88. /
)88/ 0
,880 1
	onFailure99 
:99 
error99  
=>99! #

BadRequest99$ .
(99. /
error99/ 4
)994 5
):: 
;:: 
};; 	
[== 	

HttpDelete==	 
(== 
$str== 
)== 
]== 
public>> 
async>> 
Task>> 
<>> 
IActionResult>> '
>>>' ("
DeleteShoppingCartItem>>) ?
(>>? @
[>>@ A
	FromRoute>>A J
]>>J K
Guid>>L P
id>>Q S
)>>S T
{?? 	
var@@ 
resultObject@@ 
=@@ 
await@@ $
mediator@@% -
.@@- .
Send@@. 2
(@@2 3
new@@3 6)
DeleteShoppingCartItemCommand@@7 T
{@@U V
Id@@W Y
=@@Z [
id@@\ ^
}@@_ `
)@@` a
;@@a b
returnAA 
resultObjectAA 
.AA  
MatchAA  %
<AA% &
IActionResultAA& 3
>AA3 4
(AA4 5
	onSuccessBB 
:BB 
resultBB !
=>BB" $
	NoContentBB% .
(BB. /
)BB/ 0
,BB0 1
	onFailureCC 
:CC 
errorCC  
=>CC! #

BadRequestCC$ .
(CC. /
errorCC/ 4
)CC4 5
)DD 
;DD 
}EE 	
}FF 
}GG Ú7
ÖC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\SmartE-commercePlatform\Controllers\ProductsController.cs
	namespace 	#
SmartE_commercePlatform
 !
.! "
Controllers" -
{ 
[ 
Route 

(
 
$str  
)  !
]! "
[		 
ApiController		 
]		 
public

 

class

 
ProductsController

 #
:

$ %
ControllerBase

& 4
{ 
private 
readonly 
	IMediator "
mediator# +
;+ ,
public 
ProductsController !
(! "
	IMediator" +
mediator, 4
)4 5
{ 	
this 
. 
mediator 
= 
mediator $
;$ %
} 	
[ 	
HttpPost	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
CreateProduct) 6
(6 7 
CreateProductCommand7 K 
createProductCommandL `
)` a
{ 	
try 
{ 
var 
resultObject  
=! "
await# (
mediator) 1
.1 2
Send2 6
(6 7 
createProductCommand7 K
)K L
;L M
return 
resultObject #
.# $
Match$ )
<) *
IActionResult* 7
>7 8
(8 9
	onSuccess 
: 
result %
=>& (
CreatedAtAction) 8
(8 9
nameof9 ?
(? @
GetProductById@ N
)N O
,O P
newQ T
{U V
idW Y
=Z [
result\ b
}c d
,d e
resultf l
)l m
,m n
	onFailure 
: 
error $
=>% '

BadRequest( 2
(2 3
error3 8
)8 9
) 
; 
} 
catch 
( 
	Exception 
ex 
)  
{ 
return 

BadRequest !
(! "
ex" $
.$ %
Message% ,
), -
;- .
}   
}!! 	
[## 	
HttpGet##	 
(## 
$str## 
)## 
]## 
public$$ 
async$$ 
Task$$ 
<$$ 
IActionResult$$ '
>$$' (
GetProductById$$) 7
($$7 8
[$$8 9
	FromRoute$$9 B
]$$B C
Guid$$D H
id$$I K
)$$K L
{%% 	
var&& 
resultObject&& 
=&& 
await&& $
mediator&&% -
.&&- .
Send&&. 2
(&&2 3
new&&3 6
GetProductByIdQuery&&7 J
{&&K L
Id&&M O
=&&P Q
id&&R T
}&&U V
)&&V W
;&&W X
return'' 
resultObject'' 
.''  
Match''  %
<''% &
IActionResult''& 3
>''3 4
(''4 5
	onSuccess(( 
:(( 
result(( !
=>((" $
Ok((% '
(((' (
result((( .
)((. /
,((/ 0
	onFailure)) 
:)) 
error))  
=>))! #

BadRequest))$ .
()). /
error))/ 4
)))4 5
)** 
;** 
}++ 	
[-- 	
HttpGet--	 
]-- 
public.. 
async.. 
Task.. 
<.. 
IActionResult.. '
>..' (
GetAllProducts..) 7
(..7 8
)..8 9
{// 	
var00 
result00 
=00 
await00 
mediator00 '
.00' (
Send00( ,
(00, -
new00- 0
GetAllProductsQuery001 D
(00D E
)00E F
)00F G
;00G H
return11 
Ok11 
(11 
result11 
)11 
;11 
}22 	
[44 	
HttpPut44	 
(44 
$str44 
)44 
]44 
public55 
async55 
Task55 
<55 
IActionResult55 '
>55' (
UpdateProduct55) 6
(556 7
[557 8
FromBody558 @
]55@ A 
UpdateProductCommand55B V 
updateProductCommand55W k
,55k l
[55m n
	FromRoute55n w
]55w x
Guid55y }
id	55~ Ä
)
55Ä Å
{66 	
if77 
(77  
updateProductCommand77 $
.77$ %
Id77% '
!=77( *
id77+ -
)77- .
return88 

BadRequest88 !
(88! "
)88" #
;88# $
try99 
{:: 
var;; 
resultObject;;  
=;;! "
await;;# (
mediator;;) 1
.;;1 2
Send;;2 6
(;;6 7 
updateProductCommand;;7 K
);;K L
;;;L M
return<< 
resultObject<< #
.<<# $
Match<<$ )
<<<) *
IActionResult<<* 7
><<7 8
(<<8 9
	onSuccess== 
:== 
result== %
=>==& (
	NoContent==) 2
(==2 3
)==3 4
,==4 5
	onFailure>> 
:>> 
error>> $
=>>>% '

BadRequest>>( 2
(>>2 3
error>>3 8
)>>8 9
)?? 
;?? 
}@@ 
catchAA 
(AA 
	ExceptionAA 
exAA 
)AA  
{BB 
returnCC 

BadRequestCC !
(CC! "
exCC" $
.CC$ %
MessageCC% ,
)CC, -
;CC- .
}DD 
}EE 	
[GG 	

HttpDeleteGG	 
(GG 
$strGG 
)GG 
]GG 
publicHH 
asyncHH 
TaskHH 
<HH 
IActionResultHH '
>HH' (
DeleteProductHH) 6
(HH6 7
[HH7 8
	FromRouteHH8 A
]HHA B
GuidHHC G
idHHH J
)HHJ K
{II 	
varJJ 
resultObjectJJ 
=JJ 
awaitJJ $
mediatorJJ% -
.JJ- .
SendJJ. 2
(JJ2 3
newJJ3 6 
DeleteProductCommandJJ7 K
{JJL M
IdJJN P
=JJQ R
idJJS U
}JJV W
)JJW X
;JJX Y
returnKK 
resultObjectKK 
.KK  
MatchKK  %
<KK% &
IActionResultKK& 3
>KK3 4
(KK4 5
	onSuccessLL 
:LL 
resultLL !
=>LL" $
	NoContentLL% .
(LL. /
)LL/ 0
,LL0 1
	onFailureMM 
:MM 
errorMM  
=>MM! #

BadRequestMM$ .
(MM. /
errorMM/ 4
)MM4 5
)NN 
;NN 
}OO 	
}PP 
}QQ ö
ÑC:\Users\Admin\Desktop\facultate\E-commercePlatform\SmartE-commercePlatform\SmartE-commercePlatform\Controllers\ClientsController.cs
	namespace 	#
SmartE_commercePlatform
 !
.! "
Controllers" -
{ 
[ 
Route 

(
 
$str  
)  !
]! "
[ 
ApiController 
] 
public 

class 
ClientsController "
:# $
ControllerBase% 3
{		 
private

 
readonly

 
	IMediator

 "
mediator

# +
;

+ ,
public 
ClientsController  
(  !
	IMediator! *
mediator+ 3
)3 4
{ 	
this 
. 
mediator 
= 
mediator $
;$ %
} 	
} 
} 