import { check, group, sleep } from "k6";
import http from "k6/http";

const apiRandomMeal = "https://www.themealdb.com/api/json/v1/1/random.php";
const apiBaseURL = "https://meetup2020.azurewebsites.net/api/v1";

// Test Configuration
const maxUsers = 2000;
const sleepTimeSec = 1;
const randomMealLocal = true;
const isLogEnabled = true;

// Test configuration
export let options = {
    // Rampup
    stages: [
        { duration: "10s", target: maxUsers },
        { duration: "20s", target: maxUsers },
        { duration: "15s", target: (maxUsers / 2) },
        { duration: "5s", target: maxUsers },
        { duration: "10s", target: 0 }
    ],
    thresholds: {
        "http_req_duration": ["p(95)<250"]
    }
};



export function setup() {
    log("Init Random meals");
    const totalTestMeals = 20;
    let arr = [];

    if(randomMealLocal) {
        log("Using random numbers to generate meals");
        for (let index = 0; index < totalTestMeals; index++) {
            arr.push({
                strMeal: "Cibo " + Math.floor(Math.random() * 500),
                strCategory: "Generico"
            });
        }
    }
    else {
        log("Using API to generate random meals");
        for (let index = 0; index < totalTestMeals; index++) {
            let randomMeal = http.get(apiRandomMeal);
            log("Received random meal");
            arr.push(randomMeal.json().meals[0]);
        }
    }

    return { mealsTestList: arr };
}

// User scenario
export default function(data) {
    group("Add food", function() {        
        let meal = getRandomMeal(data.mealsTestList);

        let payload = 
        {
            name: meal.strMeal,
            type: meal.strCategory,
            calories: Math.floor(Math.random() * 500),
            created: new Date().toISOString()
        };

        log("Random Meal: " + payload.name);

        let headers = { "Content-Type": "application/json" };

        let res = http.post(apiBaseURL + "/foods", JSON.stringify(payload), { headers: headers });

        check(res, {
            "is created (status 201)": (r) => r.status === 201
        });

        sleep(sleepTimeSec);
    });
}

function getRandomMeal(mealsTestList) {
    let rndMealItemIndex = Math.floor(Math.random() * mealsTestList.length);
    return mealsTestList[rndMealItemIndex];
}

function printConfiguration() {
    console.log("------- Configuration -------");
    console.log("Random meal Local: " + randomMealLocal);
}

function log(s) {
    if(isLogEnabled) {
        console.log(s);
    }
}