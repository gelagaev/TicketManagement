//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.18.2.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const AUTH_BASE_URL = new InjectionToken<string>('AUTH_BASE_URL');

@Injectable({
    providedIn: 'root'
})
export class ServiceProxy {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(AUTH_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    /**
     * SignIn
     * @param body (optional)
     * @return Success
     */
    auth_SignIn(body: SignInRequest | undefined): Observable<SignInResponse> {
        let url_ = this.baseUrl + "/api/V1/SignIn";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json; x-api-version=1.0",
                "Accept": "text/plain; x-api-version=1.0"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAuth_SignIn(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAuth_SignIn(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<SignInResponse>;
                }
            } else
                return _observableThrow(response_) as any as Observable<SignInResponse>;
        }));
    }

    protected processAuth_SignIn(response: HttpResponseBase): Observable<SignInResponse> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = SignInResponse.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<SignInResponse>(null as any);
    }

    /**
     * Register
     * @param body (optional)
     * @return Success
     */
    auth_Register(body: RegisterRequest | undefined): Observable<RegisterResponse> {
        let url_ = this.baseUrl + "/api/V1/Register";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json; x-api-version=1.0",
                "Accept": "text/plain; x-api-version=1.0"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAuth_Register(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAuth_Register(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<RegisterResponse>;
                }
            } else
                return _observableThrow(response_) as any as Observable<RegisterResponse>;
        }));
    }

    protected processAuth_Register(response: HttpResponseBase): Observable<RegisterResponse> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = RegisterResponse.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<RegisterResponse>(null as any);
    }
}

export class IdentityError implements IIdentityError {
    code!: string | undefined;
    description!: string | undefined;

    constructor(data?: IIdentityError) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.code = _data["code"];
            this.description = _data["description"];
        }
    }

    static fromJS(data: any): IdentityError {
        data = typeof data === 'object' ? data : {};
        let result = new IdentityError();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["code"] = this.code;
        data["description"] = this.description;
        return data;
    }
}

export interface IIdentityError {
    code: string | undefined;
    description: string | undefined;
}

export class RegisterRequest implements IRegisterRequest {
    email!: string | undefined;
    password!: string | undefined;
    firstName!: string | undefined;
    lastName!: string | undefined;

    constructor(data?: IRegisterRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.email = _data["email"];
            this.password = _data["password"];
            this.firstName = _data["firstName"];
            this.lastName = _data["lastName"];
        }
    }

    static fromJS(data: any): RegisterRequest {
        data = typeof data === 'object' ? data : {};
        let result = new RegisterRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["email"] = this.email;
        data["password"] = this.password;
        data["firstName"] = this.firstName;
        data["lastName"] = this.lastName;
        return data;
    }
}

export interface IRegisterRequest {
    email: string | undefined;
    password: string | undefined;
    firstName: string | undefined;
    lastName: string | undefined;
}

export class RegisterResponse implements IRegisterResponse {
    success!: boolean;
    errors!: IdentityError[] | undefined;

    constructor(data?: IRegisterResponse) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.success = _data["success"];
            if (Array.isArray(_data["errors"])) {
                this.errors = [] as any;
                for (let item of _data["errors"])
                    this.errors!.push(IdentityError.fromJS(item));
            }
        }
    }

    static fromJS(data: any): RegisterResponse {
        data = typeof data === 'object' ? data : {};
        let result = new RegisterResponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["success"] = this.success;
        if (Array.isArray(this.errors)) {
            data["errors"] = [];
            for (let item of this.errors)
                data["errors"].push(item.toJSON());
        }
        return data;
    }
}

export interface IRegisterResponse {
    success: boolean;
    errors: IdentityError[] | undefined;
}

export class SignInRequest implements ISignInRequest {
    email!: string;
    password!: string;

    constructor(data?: ISignInRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.email = _data["email"];
            this.password = _data["password"];
        }
    }

    static fromJS(data: any): SignInRequest {
        data = typeof data === 'object' ? data : {};
        let result = new SignInRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["email"] = this.email;
        data["password"] = this.password;
        return data;
    }
}

export interface ISignInRequest {
    email: string;
    password: string;
}

export class SignInResponse implements ISignInResponse {
    success!: boolean;
    token!: string | undefined;
    error!: string | undefined;

    constructor(data?: ISignInResponse) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.success = _data["success"];
            this.token = _data["token"];
            this.error = _data["error"];
        }
    }

    static fromJS(data: any): SignInResponse {
        data = typeof data === 'object' ? data : {};
        let result = new SignInResponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["success"] = this.success;
        data["token"] = this.token;
        data["error"] = this.error;
        return data;
    }
}

export interface ISignInResponse {
    success: boolean;
    token: string | undefined;
    error: string | undefined;
}

export class SwaggerException extends Error {
    override message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new SwaggerException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((event.target as any).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}