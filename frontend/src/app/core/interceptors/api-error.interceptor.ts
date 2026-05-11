import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const apiErrorInterceptor: HttpInterceptorFn = (request, next) =>
  next(request).pipe(
    catchError((error: unknown) => {
      const message =
        error instanceof HttpErrorResponse
          ? extractApiMessage(error)
          : 'Nao foi possivel concluir a solicitacao.';

      return throwError(() => new Error(message));
    }),
  );

function extractApiMessage(error: HttpErrorResponse): string {
  if (typeof error.error?.detail === 'string') {
    return error.error.detail;
  }

  if (error.status === 0) {
    return 'API indisponivel. Verifique se o backend esta em execucao.';
  }

  return 'Nao foi possivel calcular o investimento agora.';
}
