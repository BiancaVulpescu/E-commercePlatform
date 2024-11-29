import { NgModel, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";
import { provideHttpClient } from "@angular/common/http";

import { appRoutes } from "./app.routes";
import { ProductService } from "../app/services/product.service";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

@NgModule({
    declarations:[

    ],
    imports: [
        BrowserModule,
        CommonModule,
        BrowserAnimationsModule,
        ReactiveFormsModule,
        RouterModule.forRoot(appRoutes),
    ],
    providers: [provideHttpClient(), ProductService],
})
export class AppModule{}
