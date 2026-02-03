import { provideHttpClient, withInterceptors } from "@angular/common/http";
import { refreshTokenInterceptor } from "./interceptors/refresh-token.interceptor";
import { errorInterceptor } from "./interceptors/error.interceptor";

const provideHttp = () => provideHttpClient(
  withInterceptors([
    refreshTokenInterceptor,
    errorInterceptor
  ]));
export { provideHttp };