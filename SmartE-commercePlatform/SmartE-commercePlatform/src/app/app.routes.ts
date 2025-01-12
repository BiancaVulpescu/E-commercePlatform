import { Routes } from '@angular/router';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductCreateComponent } from './components/product-create/product-create.component';
import { ProductUpdateComponent } from './components/product-update/product-update.component';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ProductPricePredictionComponent } from './components/product-price-prediction/product-price-prediction.component';
import { HttpClientModule } from '@angular/common/http';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { ProfileComponent } from './components/profile/profile.component';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';
import { AuthGuard } from './guards/auth.guard';
export const appRoutes: Routes = [
    {path:'', redirectTo: '/register', pathMatch: 'full'},
    {path:'products', component: ProductListComponent},
    {path: 'products/create', component : ProductCreateComponent},
    {path: 'products/update/:id', component: ProductUpdateComponent },
    {path: 'products/detail/:id', component: ProductDetailComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'product-price-prediction', component: ProductPricePredictionComponent },
    { path: 'forgot-password', component: ForgotPasswordComponent },
    { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
    { path: 'edit-profile', component: EditProfileComponent }
];