﻿using System.Collections.Generic;
using Machine.Specifications;
using app.web.application;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
	[Subject(typeof(ViewTheMainDepartmentsInTheStore))]
	public class ViewDepartmentInformationSpecs
	{
		public abstract class concern : Observes<IImplementAUseCase,
																			ViewTheMainDepartmentsInTheStore>
		{
		}

		public class when_run : concern
		{
			Establish c = () =>
			{
				request = fake.an<IContainRequestInformation>();
				main_departments = new List<Department> { new Department() };
				find_information_in_the_store = depends.on<IFindInformationInTheStore>();
				response_engine = depends.on<IDisplayReportModels>();

				find_information_in_the_store.setup(x => x.get_the_main_departments()).Return(main_departments);
			};

			Because b = () =>
				sut.process(request);

			It should_get_the_main_departments_in_the_store = () =>
				find_information_in_the_store.received(x => x.get_the_main_departments());

			It should_display_the_report_model = () =>
				response_engine.received(x => x.display(main_departments));

			static IFindInformationInTheStore find_information_in_the_store;
			static IContainRequestInformation request;
			static IDisplayReportModels response_engine;
			static IEnumerable<Department> main_departments;
		}
	}
}