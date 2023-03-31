import { RouteData } from '../DataTypes/RouteData'

// Routing interface
export interface IRouter {
    // maps a set of routes
    map(route: RouteData[]): void;
}