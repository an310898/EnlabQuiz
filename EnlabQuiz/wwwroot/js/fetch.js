async function fetchAPI(ApiController) {

    const url = `https://localhost:7156/api/${ApiController}`
    const res = await fetch(url, { method: 'GET', headers: { "Content-Type": "application/json" } })

    const data = await res.json();

    return data
}

async function fetchAPI(ApiController, method, formData) {

    const url = `https://localhost:7156/api/${ApiController}`
    const res = await fetch(url, { method: method, headers: { "Content-Type": "application/json" }, body: JSON.stringify(formData) })

    const data = await res.json();

    return data
}

async function CallProcedure(procName) {

    const url = `https://localhost:7156/CallProcedure/${procName}`
    const res = await fetch(url, { method: 'POST', headers: { "Content-Type": "application/json" } })

    const data = await res.json();

    return data
}


