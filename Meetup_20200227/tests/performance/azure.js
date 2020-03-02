import { check, group, sleep } from "k6";
import http from "k6/http";
import { Rate } from "k6/metrics";

const myFailRate = new Rate('failed requests');
const baseURl = "http://meetup2020.azurewebsites.net/api/v1";

// Test configuration
export let options = {
    // Rampup for 10s from 1 to 15, stay at 15, and then down to 0
    stages: [
        { duration: "10s", target: 15 },
        { duration: "20s", target: 15 },
        { duration: "10s", target: 0 }
    ],
    thresholds: {
        "http_req_duration": ["p(95)<250"]
    }
};

// User scenario
export default function() {
    group("Get Foods", function() {
        let url = baseURl + "/foods";
        let res = http.get(url);

        // Make sure the status code is 200 OK
        check(res, {
            "is status 200": (r) => r.status === 200
        });

        myFailRate.add(res.status !== 200);
        
        // Simulate user reading the page
        sleep(5);
    });

    group("Insert Food", function() {
        let res = http.get(baseURl + "/foods");

        // Make sure the status code is 200 OK
        check(res, {
            "is status 200": (r) => r.status === 200
        });

        myFailRate.add(res.status !== 200);
        
        // Simulate user reading the page
        sleep(5);
    });
}