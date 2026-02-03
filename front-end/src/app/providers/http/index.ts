import { provideHttpClient, withInterceptors } from "@angular/common/http";
import { refreshTokenInterceptor } from "./interceptors/refresh-token.interceptor";

const provideHttp = () => provideHttpClient(withInterceptors([refreshTokenInterceptor]))
export { provideHttp };