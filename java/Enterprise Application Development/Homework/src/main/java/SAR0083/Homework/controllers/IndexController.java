package SAR0083.Homework.controllers;

import java.security.Principal;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;

import SAR0083.Homework.models.Vehicle;
import SAR0083.Homework.StringToEmailAddressConverter;
import SAR0083.Homework.models.Car;
import SAR0083.Homework.models.Customer;
import SAR0083.Homework.models.Lease;
import SAR0083.Homework.models.Truck;
import SAR0083.Homework.services.VehicleService;
import SAR0083.Homework.services.AccountService;
import SAR0083.Homework.services.CustomerService;
import SAR0083.Homework.services.LeaseService;

@Controller
public class IndexController {
	// Properties
	@Autowired
	private AccountService accountService;
	@Autowired
	private VehicleService vehicleService;
	@Autowired
	private CustomerService customerService;
	@Autowired
	private LeaseService leaseService;
	
	private String title = "Vehicle Leasing Database";
	private String birthDateMax = DateTimeFormatter.ISO_LOCAL_DATE.format(LocalDate.now().minusYears(18));
	
	
	// Page: index
	@GetMapping(path = "/")
	public String index(Model model, Principal principal) {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", title);
		model.addAttribute("accounts", accountService.getAccounts());
		model.addAttribute("vehicles", vehicleService.getVehicles());
		model.addAttribute("cars", vehicleService.getCars());
		model.addAttribute("trucks", vehicleService.getTrucks());
		model.addAttribute("customers", customerService.getCustomers());
		model.addAttribute("leases", leaseService.getLeases());
		
		return "index";
	}
	
	
	// Page: vehicle
	@GetMapping(path = "/add/vehicle")
	public String addVehicle(Model model, Principal principal) throws Exception {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", "Add vehicle");
		model.addAttribute("formButtonValue", "Add vehicle");
		model.addAttribute("vehicle", new Vehicle());
		model.addAttribute("car", new Car());
		model.addAttribute("truck", new Truck());
		
		return "addVehicle";
	}
	
	@GetMapping(path = "/remove/vehicle/{id}")
	public String removeVehicle(@PathVariable long id, Model model) {
		vehicleService.delete(id);
		
		return "redirect:../../";
	}
	
	
	// Page: car
	@GetMapping(path = "/add/car")
	public String addCar(Model model, Principal principal) throws Exception {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", "Add car");
		model.addAttribute("formButtonValue", "Add car");
		model.addAttribute("car", new Car());
		
		return "editCar";
	}
	
	@GetMapping(path = "/edit/car/{id}")
	public String editCar(@PathVariable long id, Model model, Principal principal) {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", "Edit car");
		model.addAttribute("formButtonValue", "Edit car");
		model.addAttribute("car", vehicleService.getCar(id));
		
		return "editCar";
	}
	
	@PostMapping(path = "/edit/car")
	public String editCar(@ModelAttribute Car car, Model model) throws Exception {
		if(car.getId() != 0) {
			Vehicle vehicle = vehicleService.getVehicle(car.getId());
			
			if(vehicle.getVehicleType() != car.getVehicleType()) {
				vehicleService.delete(car.getId());
			}
		}
		
		vehicleService.setCar(car);
		
		return "redirect:../";
	}
	
	
	// Page: truck
	@GetMapping(path = "/add/truck")
	public String addTruck(Model model, Principal principal) throws Exception {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", "Add truck");
		model.addAttribute("formButtonValue", "Add truck");
		model.addAttribute("truck", new Truck());
		
		return "editTruck";
	}
	
	@GetMapping(path = "/edit/truck/{id}")
	public String editTruck(@PathVariable long id, Model model, Principal principal) {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", "Edit truck");
		model.addAttribute("formButtonValue", "Edit truck");
		model.addAttribute("truck", vehicleService.getTruck(id));
		
		return "editTruck";
	}
	
	@PostMapping(path = "/edit/truck")
	public String editTruck(@ModelAttribute Truck truck, Model model) throws Exception {
		if(truck.getId() != 0) {
			Vehicle vehicle = vehicleService.getVehicle(truck.getId());
			
			if(vehicle.getVehicleType() != truck.getVehicleType()) {
				vehicleService.delete(truck.getId());
			}
		}
		
		vehicleService.setTruck(truck);
		
		return "redirect:../";
	}
	
	
	// Page: customer
	@GetMapping(path = "/add/customer")
	public String addCustomer(Model model, Principal principal) throws Exception {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", "Add customer");
		model.addAttribute("formButtonValue", "Add customer");
		model.addAttribute("birthDateMax", birthDateMax);
		model.addAttribute("customer", new Customer());
		
		return "editCustomer";
	}
	
	@GetMapping(path = "/edit/customer/{id}")
	public String editCustomer(@PathVariable long id, Model model, Principal principal) {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", "Edit customer");
		model.addAttribute("formButtonValue", "Edit customer");
		model.addAttribute("birthDateMax", birthDateMax);
		model.addAttribute("customer", customerService.getCustomer(id));
		
		return "editCustomer";
	}

	@PostMapping(path = "/edit/customer")
	public String editCustomer(@ModelAttribute Customer customer, Model model) {
		customerService.setCustomer(customer);
		
		return "redirect:../";
	}
	
	@GetMapping(path = "/remove/customer/{id}")
	public String removeCustomer(@PathVariable long id, Model model) {
		customerService.delete(id);
		
		return "redirect:../../";
	}
	
	
	// Page: lease
	@GetMapping(path = "/add/lease")
	public String addLease(Model model, Principal principal) throws Exception {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", "Add lease");
		model.addAttribute("formButtonValue", "Add lease");
		model.addAttribute("lease", new Lease());
		model.addAttribute("vehicles", vehicleService.getVehicles());
		model.addAttribute("customers", customerService.getCustomers());
		
		return "editLease";
	}
	
	@GetMapping(path = "/edit/lease/{id}")
	public String editLease(@PathVariable long id, Model model, Principal principal) {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", "Edit lease");
		model.addAttribute("formButtonValue", "Edit lease");
		model.addAttribute("lease", leaseService.getLease(id));
		model.addAttribute("vehicles", vehicleService.getVehicles());
		model.addAttribute("customers", customerService.getCustomers());
		
		return "editLease";
	}

	@PostMapping(path = "/edit/lease")
	public String editLease(@ModelAttribute Lease lease, Model model) {
		leaseService.setLease(lease);
		
		return "redirect:../";
	}
	
	@GetMapping(path = "/remove/lease/{id}")
	public String removeLease(@PathVariable long id, Model model) {
		leaseService.delete(id);
		
		return "redirect:../../";
	}
}
