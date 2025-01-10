import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient, HTTP_INTERCEPTORS, withInterceptors, withFetch } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app/app.component';
import { appRoutes } from './app/app.routes';
import { AuthInterceptor } from './app/interceptors/auth.interceptor';

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(withFetch()),
    provideRouter(appRoutes),
    BrowserAnimationsModule,
    ReactiveFormsModule,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ]
}).catch((err) => console.error(err));
